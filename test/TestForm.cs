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
using System.Linq;
using System.Windows.Forms;

using OBS;

using test.Objects;
using test.Utility;

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
				SceneListBox.DataBindings.Add(new Binding("SelectedItem", _presentation, "SelectedScene", false, DataSourceUpdateMode.OnPropertyChanged));
				SceneListBox.DataSource = _presentation.Scenes;

				// Item
				ItemListBox.DisplayMember = "Name";
				ItemListBox.DataBindings.Add(new Binding("SelectedItem", _presentation, "SelectedItem", false, DataSourceUpdateMode.OnPropertyChanged));

				// Source
				SourceListBox.DisplayMember = "Name";
				SourceListBox.DataBindings.Add(new Binding("SelectedItem", _presentation, "SelectedSource", false, DataSourceUpdateMode.OnPropertyChanged));
				SourceListBox.DataSource = _presentation.Sources;


				_presentation.AddScene();

				SetItemBind();

				var source = _presentation.AddSource("random", "some random source");
				_presentation.AddItem(source);

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
		}

		private void DelSceneButton_Click(object sender, EventArgs e)
		{
			_presentation.DelScene();
		}

		private void SceneListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_presentation.SelectedScene == null)
				return;

			SetItemBind();
		}

		#endregion

		#region ItemControls

		private void AddItemButton_Click(object sender, EventArgs e)
		{
			DisplaySourceMenu();
		}

		private void ItemListBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (_presentation.SelectedScene == null || _presentation.SelectedItem == null || e.Button != MouseButtons.Right)
				return;

			ShowItemContextMenu();
		}

		private void ShowItemContextMenu()
		{
			var top = new ToolStripMenuItem("Move to &Top");
			top.Click += (o, args) =>
			{
				_presentation.SelectedItem.SetOrder(obs_order_movement.OBS_ORDER_MOVE_TOP);
				ItemListBox.SelectedIndex = _presentation.SelectedScene.MoveItem(_presentation.SelectedItem, obs_order_movement.OBS_ORDER_MOVE_TOP);

			};

			var up = new ToolStripMenuItem("Move &Up");
			up.Click += (o, args) =>
			{
				_presentation.SelectedItem.SetOrder(obs_order_movement.OBS_ORDER_MOVE_UP);
				ItemListBox.SelectedIndex = _presentation.SelectedScene.MoveItem(_presentation.SelectedItem, obs_order_movement.OBS_ORDER_MOVE_UP);
			};

			var down = new ToolStripMenuItem("Move &Down");
			down.Click += (o, args) =>
			{
				_presentation.SelectedItem.SetOrder(obs_order_movement.OBS_ORDER_MOVE_DOWN);
				ItemListBox.SelectedIndex = _presentation.SelectedScene.MoveItem(_presentation.SelectedItem, obs_order_movement.OBS_ORDER_MOVE_DOWN);
			};

			var bottom = new ToolStripMenuItem("Move to &Bottom");
			bottom.Click += (o, args) =>
			{
				_presentation.SelectedItem.SetOrder(obs_order_movement.OBS_ORDER_MOVE_BOTTOM);
				ItemListBox.SelectedIndex = _presentation.SelectedScene.MoveItem(_presentation.SelectedItem, obs_order_movement.OBS_ORDER_MOVE_BOTTOM);
			};

			var transform = new ToolStripMenuItem("&Edit Transform Options...");
			transform.Click += (o, args) =>
			{
				var transformfrm = new TestTransform(_presentation.SelectedItem);
				transformfrm.ShowDialog(this);
			};

			var visible = new ToolStripBindableMenuItem
						  {
							  Text = "&Visible",
							  CheckOnClick = true
						  };
			visible.DataBindings.Add(new Binding("Checked", _presentation.SelectedItem, "Visible", false, DataSourceUpdateMode.OnPropertyChanged));


			var ordermenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };

			ordermenu.Items.AddRange(new ToolStripItem[]
			                         {
				                         top, 
				                         up, 
				                         down, 
				                         bottom, 
				                         new ToolStripSeparator(),
										 visible,
										 new ToolStripSeparator(), 
				                         transform
			                         });

			ordermenu.Show(this, PointToClient(Cursor.Position));
		}

		private void DelItemButton_Click(object sender, EventArgs e)
		{
			_presentation.DelItem();
		}

		private void ItemListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
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

		#endregion

		#region SourceControls

		private void DisplaySourceMenu(bool deleteaftercomplete = false)
		{
			// TODO: there's a dirty hack in place here
			/* 
			 * adds an item then removes it once its complete because it wont render unless the source is visible on the scene
			 * fix it so sources are displayed even if not being shown on canvas
			 */

			// create source context menu
			var inputmenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };

			// create a context menu item for each source type
			foreach (string inputType in _inputTypes)
			{
				// The variable dissapears when the loop ends so it needs to be copied
				string type = inputType;

				// create display name
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Input, inputType);

				// create menu item
				var menuitem = new ToolStripMenuItem(Text = displayname + " (" + type + ")");

				// attach menu item click event
				menuitem.Click += (sender, args) =>
				{
					// create a source based off the menu item name
					var source = _presentation.AddSource(type, displayname + (_presentation.Sources.Count + 1));

					// add scene item made from source
					_presentation.AddItem(source);

					// create property dialog
					var prop = new TestProperties(source);

					// this check is here for the addsource in the sourcelistbox
					if (deleteaftercomplete)
					{
						// remove the item after the source has been configured
						prop.Disposed += (o, eventArgs) =>
						{
							Item item = _presentation.SelectedScene.Items.Last();
							item.Remove();
							item.Dispose();
							_presentation.SelectedScene.Items.Remove(item);
						};
					}
					// show property dialog
					prop.Show();
				};

				inputmenu.Items.Add(menuitem);
			}

			inputmenu.Show(this, PointToClient(Cursor.Position));
		}

		private void DisplayFilterSourceMenu()
		{
			//TODO: actually use this somewhere :p
			var filtermenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };

			foreach (var filterType in _filterTypes)
			{
				string type = filterType;
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Filter, filterType);
				int index = _presentation.Sources.Count + 1;

				var menuitem = new ToolStripMenuItem(Text = displayname + " (" + filterType + ")");

				menuitem.Click += (sender, args) =>
				{
					ObsSource filter = new ObsSource(ObsSourceType.Filter, type, displayname + index);
					_presentation.SelectedSource.AddFilter(filter);
				};

				filtermenu.Items.Add(menuitem);
			}
			filtermenu.Items.Add("-");
			var properties = new ToolStripMenuItem(Text = "Edit Source Properties...");
			properties.Click += (sender, args) =>
			{
				var propfrm = new TestProperties(_presentation.SelectedSource);
				propfrm.ShowDialog(this);
			};
			filtermenu.Items.Add(properties);

			filtermenu.Show(this, PointToClient(Cursor.Position));
		}

		private void SourceListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
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

		private void AddSourceButton_Click(object sender, EventArgs e)
		{
			DisplaySourceMenu(true);
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

		private void SourceListBox_MouseDown(object sender, MouseEventArgs e)
		{
			// display the filter menu when rightclicking on a source in the sourcelistbox
			if (e.Button != MouseButtons.Right || _presentation.SelectedSource == null) return;

			DisplayFilterSourceMenu();
		}

		#endregion
	}
}
