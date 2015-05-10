/***************************************************************************
	Copyright (C) 2014-2015 by Ari Vuollet <ari.vuollet@kapsi.fi>

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

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using OBS;

using test.Objects;

namespace test
{
	public partial class TestForm
	{
		public readonly int MainWidth = 1280;
		public readonly int MainHeight = 720;
		private libobs.draw_callback _renderMain;

		private libobs.sceneitem_enum_callback _enumSceneItem;

		private Presentation _presentation;

		private string[] _inputTypes;
		private string[] _filterTypes;
		private string[] _transitionTypes;

		public TestForm()
		{
			InitializeComponent();
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
			if (Environment.Is64BitProcess)
				Text += " (64-bit)";

			try
			{
				//callbacks
				_renderMain = RenderMain;
				_enumSceneItem = EnumSceneItem;

				Debug.WriteLine("libobs version: " + Obs.GetVersion());

				Obs.SetLogHandler((lvl, msg, p) =>
				{
					Debug.WriteLine(msg);
				});

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
															 hwnd = MainViewPanel.Handle
														 },
											};

				libobs.obs_audio_info avi = new libobs.obs_audio_info
											{
												samples_per_sec = 44100,
												speakers = libobs.speaker_layout.SPEAKERS_STEREO,
												buffer_ms = 1000
											};

				if (!Obs.Startup("en-US"))
					throw new ApplicationException("Startup failed.");

				if (!Obs.ResetVideo(ovi))
					throw new ApplicationException("ResetVideo failed.");

				if (!Obs.ResetAudio(avi))
					throw new ApplicationException("ResetAudio failed.");

				Obs.LoadAllModules();

				InitPrimitives();

				// Populate Source Types
				_inputTypes = Obs.GetSourceInputTypes();
				_filterTypes = Obs.GetSourceFilterTypes();
				_transitionTypes = Obs.GetSourceTransitionTypes();

				Console.Error.WriteLine(_transitionTypes);

				_presentation = new Presentation();

				// Bindings
				// Scene
				SceneListBox.DisplayMember = "Name";
				SceneListBox.ValueMember = "Items";
				SceneListBox.DataSource = _presentation.Scenes;

				// Item
				ItemListBox.DisplayMember = "Name";

				// Source
				SourceListBox.DisplayMember = "Name";
				SourceListBox.DataSource = _presentation.Sources;


				_presentation.AddScene();

				SetItemBind();

				var source = _presentation.AddSource("random", "some random source");
				_presentation.AddItem(source);

				_presentation.SceneIndex = SceneListBox.SelectedIndex;
				_presentation.ItemIndex = ItemListBox.SelectedIndex;
				_presentation.SourceIndex = SourceListBox.SelectedIndex;

				HideItemCheckBox.DataBindings.Add(
					new Binding("Checked", _presentation.SelectedItem, "Visible", false, DataSourceUpdateMode.OnPropertyChanged));

				EnableSourceCheckBox.DataBindings.Add(
					new Binding("Checked", _presentation.SelectedSource, "Enabled", false, DataSourceUpdateMode.OnPropertyChanged));

				MuteSourceCheckBox.DataBindings.Add(
					new Binding("Checked", _presentation.SelectedSource, "Muted", false, DataSourceUpdateMode.OnPropertyChanged));

				Obs.AddDrawCallback(_renderMain, Handle);

				Obs.ResizeMainView(MainViewPanel.Width, MainViewPanel.Height);
			}
			catch (BadImageFormatException exp)
			{
				MessageBox.Show("Platform target mismatch: "
								+ (Environment.Is64BitProcess
									? "Loading 32-bit OBS with 64-bit executable is not supported."
									: "Loading 64-bit OBS with 32-bit executable is not supported.")
								+ "\n\n" + exp.Message,
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}
		}

		private void SetItemBind()
		{
			ItemListBox.DataSource = SceneListBox.SelectedValue;
		}

		private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Obs.RemoveDrawCallback(_renderMain, Handle);
			RemoveScenesAndSources();

			if (_boxPrimitive != null)
				_boxPrimitive.Dispose();
			if (_circlePrimitive != null)
				_circlePrimitive.Dispose();

			Obs.Shutdown();
		}

		private void RemoveScenesAndSources()
		{
			// dispose of all items from scenes
			foreach (var scene in _presentation.Scenes)
				scene.ClearItems();

			// dispose all sources
			foreach (var source in _presentation.Sources)
			{
				source.Remove();
				source.Dispose();
			}

			// dispose all scenes
			foreach (var scene in _presentation.Scenes)
				scene.Dispose();
		}

		private void MainViewPanel_SizeChanged(object sender, EventArgs e)
		{
			Obs.ResizeMainView(MainViewPanel.Width, MainViewPanel.Height);
		}

		#region SceneControls

		private void AddSceneButton_Click(object sender, EventArgs e)
		{
			//AddScene();
			_presentation.AddScene();
			SceneListBox.SelectedIndex = _presentation.SceneIndex;
		}

		private void DelSceneButton_Click(object sender, EventArgs e)
		{
			_presentation.DelScene();
		}

		private void SceneListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_presentation.SceneIndex = SceneListBox.SelectedIndex;

			if (_presentation.SelectedScene == null)
				return;

			SetItemBind();
		}

		#endregion

		#region ItemControls



		private void AddItemButton_Click(object sender, EventArgs e)
		{
			_presentation.ShowAddSourceContextMenu(this);
		}

		private void DelItemButton_Click(object sender, EventArgs e)
		{
			_presentation.DelItem();
		}

		private void ItemListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_presentation.ItemIndex = ItemListBox.SelectedIndex;

			HideItemCheckBox.Enabled = _presentation.SelectedItem != null;

			if (_presentation.SelectedItem != null)
			{
				foreach (Item item in _presentation.SelectedScene.Items)
				{
					item.Selected = false;
				}

				_presentation.SelectedItem.Selected = true;
			}
		}

		private void ItemListBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (_presentation.SelectedScene == null || _presentation.SelectedItem == null || e.Button != MouseButtons.Right)
				return;

			_presentation.ShowItemContextMenu(this);
		}

		#endregion

		#region SourceControls

		private void AddSourceButton_Click(object sender, EventArgs e)
		{
			_presentation.ShowAddSourceContextMenu(this, true);
		}

		private void DelSourceButton_Click(object sender, EventArgs e)
		{
			_presentation.DelSource();
		}

		private void AddSourceToSceneButton_Click(object sender, EventArgs e)
		{
			// add currently selected source to the currently selected scene
			if (_presentation.SelectedSource == null) return;

			_presentation.AddItem(_presentation.SelectedSource);
		}

		private void SourceListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_presentation.SourceIndex = SourceListBox.SelectedIndex;

			// enable/disable the enable/mute checkboxes and set their value
			if (_presentation.SelectedSource == null)
			{
				EnableSourceCheckBox.Enabled = false;
				MuteSourceCheckBox.Enabled = false;
				AddSourceToSceneButton.Enabled = false;
				return;
			}

			EnableSourceCheckBox.Enabled = true;
			MuteSourceCheckBox.Enabled = true;
			AddSourceToSceneButton.Enabled = true;
		}

		private void SourceListBox_MouseDown(object sender, MouseEventArgs e)
		{
			// display the filter menu when rightclicking on a source in the sourcelistbox
			if (e.Button != MouseButtons.Right || _presentation.SelectedSource == null) return;

			_presentation.ShowSourceContextMenu(this);
		}

		#endregion
	}
}
