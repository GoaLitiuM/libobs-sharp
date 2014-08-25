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
		private libobs.draw_callback _RenderWindow = RenderWindow;

		private List<ObsScene> _scenes = new List<ObsScene>();
		private int _selectedScene = 0;

		private List<List<ObsSource>> _sceneSources = new List<List<ObsSource>>();
		private int _selectedSource = 0;

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
			Obs.Shutdown();
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
			try
			{
				Rectangle rc = new Rectangle(0, 0, panel1.Width, panel1.Height);

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
						hwnd = panel1.Handle
					}
				};

				if (!Obs.Startup("en-US"))
					throw new ApplicationException("Startup failed.");

				if (Obs.ResetVideo(ovi) != 0)
					throw new ApplicationException("ResetVideo failed.");

				Obs.LoadAllModules();

				// Populate Source Types
				_inputTypes = Obs.GetSourceInputTypes();
				_filterTypes = Obs.GetSourceFilterTypes();
				_transitionTypes = Obs.GetSourceTransitionTypes();

				AddScene();
				AddSource();

				libobs.obs_add_draw_callback(_RenderWindow, IntPtr.Zero);
			}
			catch (Exception exp)
			{
				MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK);
				Close();
			}
		}

		private void AddScene()
		{
			ObsScene scene = new ObsScene("test scene");
			Obs.SetOutputSource(0, scene.GetSource());

			_sceneSources.Add(new List<ObsSource>());
			_sceneItems.Add(new List<ObsSceneItem>());
			_scenes.Add(scene);
			listBox1.Items.Add(scene.Name);

			listBox1.SelectedIndex = listBox1.Items.Count - 1;
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
			listBox2.Items.Add(source.Name);
		}

		private void AddInputSource(string id, string name)
		{
			ObsSource source = new ObsSource(ObsSourceType.Input, id, name);

			ObsSceneItem item = _scenes[_selectedScene].Add(source);
			item.SetScale(new libobs.vec2(20.0f, 20.0f));
			item.SetPosition(new libobs.vec2(10 * _sceneSources[_selectedScene].Count, 10 * _sceneSources[_selectedScene].Count));

			_sceneSources[_selectedScene].Add(source);
			_sceneItems[_selectedScene].Add(item);
			listBox2.Items.Add(source.Name);
		}

		private void DisplayFilterSourceMenu()
		{
			ContextMenu filtermenu = new ContextMenu();

			foreach (var filterType in _filterTypes)
			{
				MenuItem menuitem = new MenuItem
				{
					Name = Obs.GetSourceTypeDisplayName(ObsSourceType.Filter, filterType),
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
				MenuItem menuitem = new MenuItem
				{
					Text = Obs.GetSourceTypeDisplayName(ObsSourceType.Input, inputType), 
					Tag = inputType
				};
				menuitem.Click += OnInputSourceMenuClick;
				
				inputmenu.MenuItems.Add(menuitem);
			}

			inputmenu.Show(this, PointToClient(Cursor.Position));
		}

		private void OnInputSourceMenuClick(object sender, EventArgs eventArgs)
		{
			MenuItem send = (MenuItem)sender;
			string id = send.Tag.ToString();
			string name = send.Text;

			AddInputSource(id, name + (_sceneSources[_selectedScene].Count + 1));
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

			listBox1.Items.RemoveAt(index);
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
			listBox2.Items.RemoveAt(index);

			listBox2.SelectedIndex = listBox2.Items.Count - 1;
		}

		private static void RenderWindow(IntPtr data, UInt32 cx, UInt32 cy)
		{
			Obs.RenderMainView();
		}

		private void panel1_SizeChanged(object sender, EventArgs e)
		{
			Obs.ResizeMainView(panel1.Width, panel1.Height);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// AddSource();
			DisplayInputSourceMenu();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex < 0)
			{
				listBox1.SelectedIndex = _selectedScene;
				return;
			}

			_selectedScene = listBox1.SelectedIndex;
			Obs.SetOutputSource(0, _scenes[_selectedScene].GetSource());

			listBox2.Items.Clear();
			foreach (ObsSource source in _sceneSources[_selectedScene])
			{
				listBox2.Items.Add(source.Name);
			}
		}

		private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			_selectedSource = listBox2.SelectedIndex;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			DelScene(_selectedScene);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DelSource(_selectedSource);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			AddScene();
		}

		private void listBox2_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && _selectedScene != -1 && _selectedSource != -1)
			{
				DisplayFilterSourceMenu();
			}
		}
	}
}