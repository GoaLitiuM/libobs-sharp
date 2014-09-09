/***************************************************************************
	Copyright (C) 2014 by Ari Vuollet <ari.vuollet@kapsi.fi>
	
	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, see <http://www.gnu.org/licenses/>.
***************************************************************************/

using OBS;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace test
{
	public partial class TestForm : Form
	{
		public readonly int MainWidth = 1280;
		public readonly int MainHeight = 720;
		private libobs.draw_callback _RenderMain;
		private libobs.sceneitem_enum_callback _EnumSceneItem;

		private List<ObsScene> _scenes = new List<ObsScene>();
		private int _selectedScene = 0;

		private List<List<ObsSource>> _sceneSources = new List<List<ObsSource>>();
		private int _selectedSource = -1;

		private List<List<ObsSceneItem>> _sceneItems = new List<List<ObsSceneItem>>();

		private string[] _inputTypes;
		private string[] _filterTypes;
		private string[] _transitionTypes;
		
		public TestForm()
		{
			InitializeComponent();
		}

		~TestForm()
		{
			Obs.Shutdown();	//FIXME: finalizer is never called
		}

		private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Obs.RemoveDrawCallback(_RenderMain, this.Handle);
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
			try
			{
				//callbacks
				_RenderMain = RenderMain;
				_EnumSceneItem = EnumSceneItem;

				Rectangle rc = new Rectangle(0, 0, MainWidth, MainHeight);
				libobs.obs_video_info ovi = new libobs.obs_video_info
				{
					adapter = 0,
					base_width = (uint)rc.Right,
					base_height = (uint)rc.Bottom,
					fps_num = 30000,
					fps_den = 1001,
					graphics_module = "libobs-d3d11",
					window_width = (uint)rc.Right,
					window_height = (uint)rc.Bottom,
					output_format = libobs.video_format.VIDEO_FORMAT_RGBA,
					output_width = (uint)rc.Right,
					output_height = (uint)rc.Bottom,
					window = new libobs.gs_window
					{
						hwnd = mainViewPanel.Handle
					}
				};

				libobs.audio_output_info ai = new libobs.audio_output_info
				{
					name = "Audio Output",
					format = libobs.audio_format.AUDIO_FORMAT_FLOAT,
					samples_per_sec = 44100,
					speakers = libobs.speaker_layout.SPEAKERS_STEREO,
					buffer_ms = 1000
				};

				if (!Obs.Startup("en-US"))
					throw new ApplicationException("Startup failed.");

				if (Obs.ResetVideo(ovi) != 0)
					throw new ApplicationException("ResetVideo failed.");

				if (!Obs.ResetAudio(ai))
					throw new ApplicationException("ResetAudio failed.");

				Obs.LoadAllModules();

				InitPrimitives();

				// Populate Source Types
				_inputTypes = Obs.GetSourceInputTypes();
				_filterTypes = Obs.GetSourceFilterTypes();
				_transitionTypes = Obs.GetSourceTransitionTypes();

				AddScene();
				AddSource();

				Obs.AddDrawCallback(_RenderMain, this.Handle);

				Obs.ResizeMainView(mainViewPanel.Width, mainViewPanel.Height);

				//select the first item
				_sceneItems[0][0].Selected = true;
			}
			catch (Exception exp)
			{
				MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK);
				Close();
			}
		}

		private void AddScene()
		{
			ObsScene scene = new ObsScene("test scene (" + (_scenes.Count + 1) + ")");
			Obs.SetOutputSource(0, scene.GetSource());

			_sceneSources.Add(new List<ObsSource>());
			_sceneItems.Add(new List<ObsSceneItem>());
			_scenes.Add(scene);
			sceneListBox.Items.Add(scene.Name);

			sceneListBox.SelectedIndex = sceneListBox.Items.Count - 1;
		}

		private void AddSource()
		{
			if (_selectedScene < 0)
				return;

			ObsSource source = new ObsSource(ObsSourceType.Input, "random", "some randon source" + (_sceneSources[_selectedScene].Count + 1));

			ObsSource filter = new ObsSource(ObsSourceType.Filter, "test_filter", "a nice green filter" + (_sceneSources[_selectedScene].Count + 1));
			source.AddFilter(filter);

			ObsSceneItem item = _scenes[_selectedScene].Add(source);
			item.SetScale(new libobs.vec2(20.0f, 20.0f));
			item.SetPosition(new libobs.vec2(10 * _sceneSources[_selectedScene].Count, 10 * _sceneSources[_selectedScene].Count));


			_sceneSources[_selectedScene].Add(source);
			_sceneItems[_selectedScene].Add(item);
			sourceListBox.Items.Add(source.Name);
		}

		private void AddInputSource(string id, string name)
		{
			ObsSource source = new ObsSource(ObsSourceType.Input, id, name);

			ObsSceneItem item = _scenes[_selectedScene].Add(source);
			
			item.SetScale(new libobs.vec2(1f, 1f));
			item.SetPosition(new libobs.vec2(0f, 0f));
			
			_sceneSources[_selectedScene].Add(source);
			_sceneItems[_selectedScene].Add(item);
			sourceListBox.Items.Add(source.Name);
		}

		private void DisplayFilterSourceMenu()
		{
			ContextMenu filtermenu = new ContextMenu();

			foreach (var filterType in _filterTypes)
			{
				MenuItem menuitem = new MenuItem
				{
					Text = Obs.GetSourceTypeDisplayName(ObsSourceType.Filter, filterType) + " (" + filterType + ")",
					Tag = filterType
				};
				menuitem.Click += OnFilterSourceMenuClick;

				filtermenu.MenuItems.Add(menuitem);
			}

			filtermenu.Show(this, PointToClient(Cursor.Position));
		}

		private void OnFilterSourceMenuClick(object sender, EventArgs eventArgs)
		{
			MenuItem send = (MenuItem)sender;
			string id = send.Tag.ToString();
			string name = send.Text;

			ObsSource filter = new ObsSource(ObsSourceType.Filter, id, name + (_sceneSources[_selectedScene].Count + 1));
			_sceneSources[_selectedScene][_selectedSource].AddFilter(filter);
		}

		private void DisplayInputSourceMenu()
		{
			ContextMenu inputmenu = new ContextMenu();

			foreach (string inputType in _inputTypes)
			{
				// The variable dissapears when the loop ends so it needs to be copied
				string type = inputType; 
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Input, inputType);
				
				MenuItem menuitem = new MenuItem
				{
					Text = displayname + " (" + type + ")"					
				};
				
				menuitem.Click += delegate
				{
					AddInputSource(type, displayname + (_sceneSources[_selectedScene].Count + 1));
					var prop = new TestProperties(ObsSourceType.Input, type);
					prop.Show();
				};

				inputmenu.MenuItems.Add(menuitem);
			}

			inputmenu.Show(this, PointToClient(Cursor.Position));
		}
		
		private void DelScene(int index)
		{
			if (_scenes.Count <= 1)
				return;


			foreach (ObsSceneItem item in _sceneItems[index])
				item.Release();

			foreach (ObsSource source in _sceneSources[index])
				source.Release();

			ObsScene scene = _scenes[index];
			scene.Release();

			_sceneSources.RemoveAt(index);
			_scenes.RemoveAt(index);

			_selectedScene = _scenes.Count - 1;

			sceneListBox.Items.RemoveAt(index);
		}

		private void DelSource(int index)
		{
			if (_selectedScene < 0)
				return;

			if (index >= _sceneSources[_selectedScene].Count || index < 0)
				return;

			ObsSource source = _sceneSources[_selectedScene][index];
			source.Release();

			ObsSceneItem item = _sceneItems[_selectedScene][index];
			item.Release();

			_sceneSources[_selectedScene].RemoveAt(index);
			_sceneItems[_selectedScene].RemoveAt(index);
			sourceListBox.Items.RemoveAt(index);

			sourceListBox.SelectedIndex = sourceListBox.Items.Count - 1;
		}


		private void mainViewPanel_SizeChanged(object sender, EventArgs e)
		{
			Obs.ResizeMainView(mainViewPanel.Width, mainViewPanel.Height);
		}

		private void addSourceButton_Click(object sender, EventArgs e)
		{
			DisplayInputSourceMenu();
		}

		private void sceneListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (sceneListBox.SelectedIndex < 0)
			{
				sceneListBox.SelectedIndex = _selectedScene;
				return;
			}

			_selectedScene = sceneListBox.SelectedIndex;
			Obs.SetOutputSource(0, _scenes[_selectedScene].GetSource());

			sourceListBox.Items.Clear();
			foreach (ObsSource source in _sceneSources[_selectedScene])
			{
				sourceListBox.Items.Add(source.Name);
			}
		}

		private void sourceListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_selectedSource = sourceListBox.SelectedIndex;
		}

		private void delSceneButton_Click(object sender, EventArgs e)
		{
			DelScene(_selectedScene);
		}

		private void delSourceButton_Click(object sender, EventArgs e)
		{
			DelSource(_selectedSource);
		}

		private void addSceneButton_Click(object sender, EventArgs e)
		{
			AddScene();
		}

		private void sourceListBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && _selectedScene != -1 && _selectedSource != -1)
			{
				DisplayFilterSourceMenu();
			}
		}

		
	}
}