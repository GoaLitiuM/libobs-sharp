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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using test.Utility;

using OBS;

namespace test
{
	public partial class TestForm
	{
		public readonly int MainWidth = 1280;
		public readonly int MainHeight = 720;
		private libobs.draw_callback _renderMain;

		private libobs.sceneitem_enum_callback _enumSceneItem;

		private int _renderSceneIndex;

		private readonly BindingList<ObsScene> _scenes = new BindingList<ObsScene>();

		private readonly BindingList<ObsSource> _sources = new BindingList<ObsSource>();

		private readonly List<BindingList<ObsSceneItem>> _sceneItems = new List<BindingList<ObsSceneItem>>();

		private int SceneIndex
		{
			get { return SceneListBox.SelectedIndex; }
		}

		private int ItemIndex
		{
			get { return ItemListBox.SelectedIndex; }
		}

		private int SourceIndex
		{
			get { return SourceListBox.SelectedIndex; }
		}

		private ObsScene SelectedScene
		{
			get { return (ObsScene)SceneListBox.SelectedItem; }
		}

		private BindingList<ObsSceneItem> SelectedSceneItems
		{
			get { return _sceneItems[SceneIndex]; }
		}

		private ObsSceneItem SelectedItem
		{
			get { return (ObsSceneItem)ItemListBox.SelectedItem; }
		}

		private ObsSource SelectedSource
		{
			get { return (ObsSource)SourceListBox.SelectedItem; }
		}

		public void SetItemBinding(BindingList<ObsSceneItem> items)
		{
			ItemListBox.DataSource = items;
			ItemListBox.DisplayMember = "Name";
		}

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

				SceneListBox.DataSource = _scenes;
				SceneListBox.DisplayMember = "ListName";

				AddScene();

				SetItemBinding(SelectedSceneItems);

				SourceListBox.DataSource = _sources;
				SourceListBox.DisplayMember = "Name";

				var source = AddSource("random", "some random source");
				AddItem(source);

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
			foreach (var scene in _sceneItems)
				foreach (var item in scene)
					item.Dispose();

			// dispose all sources
			foreach (var source in _sources)
				source.Dispose();

			// dispose all scenes
			foreach (var scene in _scenes)
				scene.Dispose();

			// clear all lists
			_sources.Clear();
			_sceneItems.Clear();
			_scenes.Clear();
		}

		private void MainViewPanel_SizeChanged(object sender, EventArgs e)
		{
			Obs.ResizeMainView(MainViewPanel.Width, MainViewPanel.Height);
		}

		#region Methods

		private ObsScene AddScene()
		{
			// Create new scene
			ObsScene scene = new ObsScene("test scene (" + (_scenes.Count + 1) + ")");

			// Show the scene in the viewport
			Obs.SetOutputScene(0, scene);

			// Add scene to scenelist
			_scenes.Add(scene);

			// create list for scene items
			_sceneItems.Add(new BindingList<ObsSceneItem>());

			// select the new scene
			SceneListBox.SelectedIndex = _scenes.IndexOf(_scenes.Last());

			return scene;
		}

		private void DelScene()
		{
			// dont delete if only scene or no scene selected
			if (SceneListBox.SelectedItem == null || SceneListBox.Items.Count == 1) return;

			// Dispose of all items
			foreach (ObsSceneItem item in SelectedSceneItems)
				item.Dispose();

			// Dispose of scene
			SelectedScene.Dispose();

			// store old index
			int oldindex = SceneIndex;

			// remove scene from list
			_scenes.RemoveAt(SceneIndex);

			// Select the next scene
			SceneListBox.SelectedIndex = oldindex >= _scenes.Count ? _scenes.IndexOf(_scenes.Last()) : oldindex;
		}

		private ObsSceneItem AddItem(ObsSource source)
		{
			// generate an item from soruce
			ObsSceneItem item = SelectedScene.Add(source);

			// set its proportions
			item.Position = new Vector2(0f, 0f);
			item.Scale = new Vector2(1.0f, 1.0f);
			item.SetBounds(new Vector2(MainWidth, MainHeight), ObsBoundsType.ScaleInner, ObsAlignment.Center);

			// add item to the item list
			SelectedSceneItems.Add(item);

			// select new item
			ItemListBox.SelectedIndex = SelectedSceneItems.IndexOf(SelectedSceneItems.Last());

			return item;
		}

		private void DelItem()
		{
			// dont delete if no scene is deleted
			if (SelectedItem == null) return;

			// dispose of scen
			SelectedItem.Remove();
			SelectedItem.Dispose();

			// store old index
			int oldindex = ItemIndex;

			// remove disposed item from list
			SelectedSceneItems.Remove(SelectedItem);

			// select next item
			if (SelectedSceneItems.Any())
			{
				ItemListBox.SelectedIndex = oldindex >= SelectedSceneItems.Count
					? SelectedSceneItems.IndexOf(SelectedSceneItems.Last())
					: oldindex;
			}
		}

		#endregion

		#region SceneControls

		private void AddSceneButton_Click(object sender, EventArgs e)
		{
			AddScene();
		}

		private ObsSource AddSource(string id, string name)
		{
			// Create a new source
			ObsSource source = new ObsSource(ObsSourceType.Input, id, name);

			// Add the source to the source list
			_sources.Add(source);

			// Select new item
			SourceListBox.SelectedIndex = _sources.IndexOf(_sources.Last());

			return source;
		}

		private void DelSceneButton_Click(object sender, EventArgs e)
		{
			DelScene();
		}

		private void DelSource()
		{
			if (SelectedSource == null) return;

			// duplicate pointer for REASONS
			var pointer = SelectedSource.GetPointer();

			// remove all scene items that use the same pointer as the selected source
			foreach (BindingList<ObsSceneItem> scene in _sceneItems)
			{
				scene.RemoveAll(x =>
				{
					using (var source = x.GetSource())
					{
						if (source.GetPointer() != pointer) return false;
					}
					x.Remove();
					x.Dispose();
					return true;
				});
			}

			// dispose of the source
			SelectedSource.Dispose();

			// store index because a remove resets index to -1
			var oldindex = SourceIndex;

			// remove the source from the source list
			_sources.Remove(SelectedSource);

			// select the next source
			if (_sources.Any())
			{
				SourceListBox.SelectedIndex = oldindex >= _sources.Count ? _sources.IndexOf(_sources.Last()) : oldindex;
			}
		}

		#endregion

		#region ItemControls

		private void AddItemButton_Click(object sender, EventArgs e)
		{
			DisplaySourceMenu();
		}

		private void SceneListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_renderSceneIndex = SceneIndex;

			if (SelectedScene == null)
				return;

			// set the viewport to the currently selected scene
			Obs.SetOutputScene(0, SelectedScene);

			SetItemBinding(SelectedSceneItems);
		}

		private void DelItemButton_Click(object sender, EventArgs e)
		{
			DelItem();
		}

		private void ItemListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// enable/disable the hide checkbox and set its value
			if (SelectedItem != null)
			{
				HideItemCheckBox.Enabled = true;

				HideItemCheckBox.Checked = SelectedItem.Visible;

				foreach (ObsSceneItem item in SelectedSceneItems)
				{
					item.Selected = false;
				}

				SelectedItem.Selected = true;
			}
			else
			{
				HideItemCheckBox.Enabled = false;
			}
		}

		private void DisplaySourceMenu(bool deleteaftercomplete = false)
		{
			// TODO: there's a dirty hack in place here
			/* 
			 * adds an item then removes it once its complete because it wont render unless the source is visible on the scene
			 * fix it so sources are displayed even if not being shown on canvas
			 */

			// create source context menu
			ContextMenu inputmenu = new ContextMenu();

			// create a context menu item for each source type
			foreach (string inputType in _inputTypes)
			{
				// The variable dissapears when the loop ends so it needs to be copied
				string type = inputType;

				// create display name
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Input, inputType);

				// create menu item
				MenuItem menuitem = new MenuItem
									{
										Text = displayname + " (" + type + ")"
									};
				// attach menu item click event
				menuitem.Click += (sender, args) =>
				{
					// create a source based off the menu item name
					var source = AddSource(type, displayname + (_sources.Count + 1));

					// add scene item made from source
					AddItem(source);

					// create property dialog
					var prop = new TestProperties(source);

					// this check is here for the addsource in the sourcelistbox
					if (deleteaftercomplete)
					{
						// remove the item after the source has been configured
						prop.Disposed += (o, eventArgs) =>
						{
							ObsSceneItem item = SelectedSceneItems.Last();
							item.Dispose();
							SelectedSceneItems.Remove(item);
						};
					}
					// show property dialog
					prop.Show();
				};

				inputmenu.MenuItems.Add(menuitem);
			}

			inputmenu.Show(this, PointToClient(Cursor.Position));
		}

		private void ItemCheckBox_Click(object sender, EventArgs e)
		{
			// only try to hide if a scene item is selected
			if (SelectedItem == null) return;

			// Invert the checkbox value
			HideItemCheckBox.Checked = !HideItemCheckBox.Checked;

			// set the value on the scene item
			SelectedItem.Visible = HideItemCheckBox.Checked;
		}

		#endregion

		#region SourceControls

		private void AddSourceButton_Click(object sender, EventArgs e)
		{
			DisplaySourceMenu(true);
		}

		private void DelSourceButton_Click(object sender, EventArgs e)
		{
			DelSource();
		}

		private void AddSourceToSceneButton_Click(object sender, EventArgs e)
		{
			// add currently selected source to the currently selected scene
			if (SelectedSource == null) return;

			AddItem(SelectedSource);
		}

		private void EnableSourceCheckBox_Click(object sender, EventArgs e)
		{
			if (SelectedSource == null) return;

			// invert the checkbox
			EnableSourceCheckBox.Checked = !EnableSourceCheckBox.Checked;

			// set enabled to the checkbox value
			SelectedSource.Enabled = EnableSourceCheckBox.Checked;
		}

		private void MuteSourceCheckBox_Click(object sender, EventArgs e)
		{
			if (SelectedSource == null) return;

			// invert the checkbox
			MuteSourceCheckBox.Checked = !MuteSourceCheckBox.Checked;

			// set muted to checkbox value
			SelectedSource.Muted = MuteSourceCheckBox.Checked;
		}

		private void DisplayFilterSourceMenu()
		{
			//TODO: actually use this somewhere :p
			ContextMenu filtermenu = new ContextMenu();

			foreach (var filterType in _filterTypes)
			{
				string type = filterType;
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Filter, filterType);
				int index = _sources.Count + 1;

				MenuItem menuitem = new MenuItem
									{
										Text = displayname + " (" + filterType + ")"
									};
				menuitem.Click += (sender, args) =>
				{
					ObsSource filter = new ObsSource(ObsSourceType.Filter, type, displayname + index);
					_sources[SourceIndex].AddFilter(filter);
				};

				filtermenu.MenuItems.Add(menuitem);
			}
			filtermenu.MenuItems.Add("-");
			MenuItem transform = new MenuItem
								 {
									 Text = "Edit Tranform Options..."
								 };
			transform.Click += (sender, args) =>
			{
				var transformfrm = new TestTransform(SelectedSceneItems[ItemIndex]);
				transformfrm.ShowDialog(this);
			};
			filtermenu.MenuItems.Add(transform);

			filtermenu.Show(this, PointToClient(Cursor.Position));
		}

		private void SourceListBox_MouseDown(object sender, MouseEventArgs e)
		{
			// display the filter menu when rightclicking on a source in the sourcelistbox
			if (e.Button != MouseButtons.Right || SelectedSource == null) return;

			DisplayFilterSourceMenu();
		}

		private void SourceListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// enable/disable the enable/mute checkboxes and set their value
			if (SelectedSource == null)
			{
				EnableSourceCheckBox.Enabled = false;
				MuteSourceCheckBox.Enabled = false;
				return;
			}

			EnableSourceCheckBox.Enabled = true;
			MuteSourceCheckBox.Enabled = true;

			EnableSourceCheckBox.Checked = SelectedSource.Enabled;
			MuteSourceCheckBox.Checked = SelectedSource.Muted;
		}

		#endregion
	}
}
