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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using OBS;

namespace test
{
	public partial class TestForm
	{
		public readonly int MainWidth = 1280;
		public readonly int MainHeight = 720;
		private libobs.draw_callback _renderMain;
		private libobs.sceneitem_enum_callback _enumSceneItem;

		private readonly List<ObsScene> _scenes = new List<ObsScene>();

		private readonly List<ObsSource> _sources = new List<ObsSource>();

		private readonly List<List<ObsSceneItem>> _sceneItems = new List<List<ObsSceneItem>>();

		private int _sceneIndex;
		private int _itemIndex;
		private int _sourceIndex;


		private ObsScene _selectedScene()
		{
			return _sceneIndex != -1 ? _scenes[_sceneIndex] : null;
		}

		private ObsSceneItem _selectedSceneItem()
		{
			return _sceneIndex != -1 && _itemIndex != -1
				? _sceneItems[_sceneIndex][_itemIndex]
				: null;
		}

		private ObsSource _selectedSource()
		{
			return _sourceIndex != -1 ? _sources[_sourceIndex] : null;
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

				Debug.WriteLine("libobs version: " + Obs.GetVersion().ToString());

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

				AddScene();
				var source = AddSource("random", "some random source");
				AddSceneItem(source);

				Obs.AddDrawCallback(_renderMain, Handle);

				Obs.ResizeMainView(MainViewPanel.Width, MainViewPanel.Height);

				//select the first item
				//_sceneItems[0][0].Selected = true;
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
			/*catch (Exception exp)
			{
				MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}*/
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

			// clear all listboxes
			SceneListBox.Items.Clear();
			SceneItemListBox.Items.Clear();
			SourceListBox.Items.Clear();
		}

		private void MainViewPanel_SizeChanged(object sender, EventArgs e)
		{
			Obs.ResizeMainView(MainViewPanel.Width, MainViewPanel.Height);
		}

		#region Scene

		private void AddScene()
		{
			// Create new scene
			ObsScene scene = new ObsScene("test scene (" + (_scenes.Count + 1) + ")");

			// Show the scene in the viewport
			Obs.SetOutputScene(0, scene);

			// Add scene to scenelist
			_scenes.Add(scene);

			// create list for scene items
			_sceneItems.Add(new List<ObsSceneItem>());

			// add scene to scenelistbox
			SceneListBox.Items.Add(scene.GetName());

			// set sceneindex to new scene
			_sceneIndex = SceneListBox.Items.Count - 1;

			// select scene in scenelistbox
			SceneListBox.SelectedIndex = _sceneIndex;
		}

		private void DelScene()
		{
			// dont delete if only scene or no scene selected
			if (_selectedScene() == null || SceneListBox.Items.Count == 1)
				return;

			// Dispose of all items
			foreach (ObsSceneItem item in _sceneItems[_sceneIndex])
				item.Dispose();

			// Dispose of scene
			_selectedScene().Dispose();

			// remove scene from list
			_scenes.RemoveAt(_sceneIndex);

			// remove scene from listbox
			SceneListBox.Items.RemoveAt(_sceneIndex);

			// select the scene directly after it
			_sceneIndex = _sceneIndex >= SceneListBox.Items.Count
				? SceneListBox.Items.Count - 1
				: _sceneIndex;
		}

		private void AddSceneButton_Click(object sender, EventArgs e)
		{
			AddScene();
		}

		private void DelSceneButton_Click(object sender, EventArgs e)
		{
			DelScene();
		}

		private void SceneListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// if the selected index isnt valid use the _sceneIndex to select the right one
			if (SceneListBox.SelectedIndex == -1)
			{
				SceneListBox.SelectedIndex = _sceneIndex > SceneListBox.Items.Count ? _sceneIndex : SceneListBox.Items.Count - 1;
				return;
			}

			_sceneIndex = SceneListBox.SelectedIndex;

			// set the viewport to the currently selected scene
			Obs.SetOutputScene(0, _selectedScene());

			// repopulate the itemlistbox with the apropriate items
			SceneItemListBox.Items.Clear();
			foreach (var item in _sceneItems[_sceneIndex])
			{
				using (ObsSource source = item.GetSource())
					SceneItemListBox.Items.Add(source.Name);
			}
		}

		#endregion

		#region SceneItem

		private void AddSceneItem(ObsSource source)
		{
			// generate an item from soruce
			ObsSceneItem item = _selectedScene().Add(source);

			// set its proportions
			item.Position = new Vector2(0f, 0f);
			item.Scale = new Vector2(1.0f, 1.0f);
			item.SetBounds(new Vector2(MainWidth, MainHeight), ObsBoundsType.ScaleInner, ObsAlignment.Center);

			// add item to the item list
			_sceneItems[_sceneIndex].Add(item);

			// add item to the item listbox
			SceneItemListBox.Items.Add(source.Name);
		}

		private void AddItemButton_Click(object sender, EventArgs e)
		{
			DisplaySourceMenu();
		}

		private void DelItemButton_Click(object sender, EventArgs e)
		{
			DelSceneItem();
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
					AddSceneItem(source);

					// create property dialog
					var prop = new TestProperties(source);

					// this check is here for the addsource in the sourcelistbox
					if (deleteaftercomplete)
					{
						// remove the item after the source has been configured
						prop.Disposed += (o, eventArgs) =>
						{
							var itemindex = SceneItemListBox.Items.Count - 1;
							_sceneItems[_sceneIndex][itemindex].Remove();
							_sceneItems[_sceneIndex][itemindex].Dispose();

							_sceneItems[_sceneIndex].RemoveAt(itemindex);

							SceneItemListBox.Items.RemoveAt(itemindex);
						};
					}
					// show property dialog
					prop.Show();
				};

				inputmenu.MenuItems.Add(menuitem);
			}

			inputmenu.Show(this, PointToClient(Cursor.Position));
		}

		private void DelSceneItem()
		{
			// only delete item if one is selected
			if (_selectedSceneItem() == null) return;

			// remove sceneitem from scene, and dispose it
			_selectedSceneItem().Remove();
			_selectedSceneItem().Dispose();

			// store item index before deleting tiem because that will reset item index to -1
			var olditemindex = _itemIndex;

			// remove item from item list
			_sceneItems[_sceneIndex].RemoveAt(_itemIndex);

			// remove item from item listbox
			SceneItemListBox.Items.RemoveAt(_itemIndex);

			// select the scene item directly after it
			if (SceneItemListBox.Items.Count > 0)
			{
				_itemIndex = olditemindex >= SceneItemListBox.Items.Count ? SceneItemListBox.Items.Count - 1 : olditemindex;
			}
			else
			{
				_itemIndex = -1;
			}
			// select item in listbox
			SceneItemListBox.SelectedIndex = _itemIndex;
		}

		private void HideSceneItemCheckBox_Click(object sender, EventArgs e)
		{
			// only try to hide if a scene item is selected
			if (_selectedSceneItem() == null) return;

			// Invert the checkbox value
			HideSceneItemCheckBox.Checked = !HideSceneItemCheckBox.Checked;

			// set the value on the scene item
			_selectedSceneItem().Visible = HideSceneItemCheckBox.Checked;
		}

		private void SceneItemListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// set item index
			_itemIndex = SceneItemListBox.SelectedIndex;

			// enable/disable the hide checkbox and set its value
			if (_selectedSceneItem() != null)
			{
				HideSceneItemCheckBox.Enabled = true;

				HideSceneItemCheckBox.Checked = _selectedSceneItem().Visible;
			}
			else
			{
				HideSceneItemCheckBox.Enabled = false;
			}
		}

		#endregion

		#region Source

		private ObsSource AddSource(string id, string name)
		{
			// Create a new source
			ObsSource source = new ObsSource(ObsSourceType.Input, id, name);

			// Add the source to the source list
			_sources.Add(source);

			// Add the source to the source listbox
			SourceListBox.Items.Add(source.Name);
			return source;
		}

		private void SourceListBox_MouseDown(object sender, MouseEventArgs e)
		{
			// display the filter menu when rightclicking on a source in the sourcelistbox
			if (e.Button == MouseButtons.Right && _selectedSource() != null)
			{
				DisplayFilterSourceMenu();
			}
		}

		private void SourceListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// set source index
			_sourceIndex = SourceListBox.SelectedIndex;

			// enable/disable the enable/mute checkboxes and set their value
			if (_selectedSource() != null)
			{
				EnableSourceCheckBox.Enabled = true;
				MuteSourceCheckBox.Enabled = true;

				EnableSourceCheckBox.Checked = _selectedSource().Enabled;
				MuteSourceCheckBox.Checked = _selectedSource().Muted;
			}
			else
			{
				EnableSourceCheckBox.Enabled = false;
				MuteSourceCheckBox.Enabled = false;
			}
		}

		private void AddSourceButton_Click(object sender, EventArgs e)
		{
			DisplaySourceMenu(true);
		}

		private void DelSourceButton_Click(object sender, EventArgs e)
		{
			if (_selectedSource() != null)
			{
				//TODO: figure out a better way to clear all items with this specific source

				// remove all scene items that use the same pointer as the selected source
				foreach (var scene in _sceneItems)
				{
					scene.RemoveAll(x =>
					{
						using (var source = x.GetSource())
							if (source.GetPointer() != _selectedSource().GetPointer())
								return false;

						x.Remove();
						x.Dispose();
						return true;
					});
				}

				// repopulate the scene item listbox
				SceneItemListBox.Items.Clear();
				foreach (var item in _sceneItems[_sceneIndex])
				{
					using (ObsSource source = item.GetSource())
						SceneItemListBox.Items.Add(source.Name);
				}

				// dispose of the source
				_selectedSource().Dispose();

				// store index because a remove resets index to -1
				var oldindex = _sourceIndex;

				// remove the source from the source list
				_sources.RemoveAt(_sourceIndex);

				// remove the source from the source listbox
				SourceListBox.Items.RemoveAt(_sourceIndex);

				// select the source directly after it
				_sourceIndex = oldindex >= SourceListBox.Items.Count ? SourceListBox.Items.Count - 1 : oldindex;

				SourceListBox.SelectedIndex = _sourceIndex;
				
				// unselect items
				SceneItemListBox.SelectedIndex = -1;
				_itemIndex = -1;
			}
		}

		private void AddSourceToSceneButton_Click(object sender, EventArgs e)
		{
			// add currently selected source to the currently selected scene
			if (_selectedSource() != null)
			{
				AddSceneItem(_selectedSource());
			}
		}

		private void EnableSourceCheckBox_Click(object sender, EventArgs e)
		{
			if (_selectedSource() != null)
			{
				// invert the checkbox
				EnableSourceCheckBox.Checked = !EnableSourceCheckBox.Checked;

				// set enabled to the checkbox value
				_selectedSource().Enabled = EnableSourceCheckBox.Checked;
			}
		}

		private void MuteSourceCheckBox_Click(object sender, EventArgs e)
		{
			if (_selectedSource() != null)
			{
				// invert the checkbox
				MuteSourceCheckBox.Checked = !MuteSourceCheckBox.Checked;

				// set muted to checkbox value
				_selectedSource().Muted = MuteSourceCheckBox.Checked;
			}
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
					_sources[_sourceIndex].AddFilter(filter);
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
				var transformfrm = new TestTransform(_sceneItems[_sceneIndex][_itemIndex]);
				transformfrm.ShowDialog(this);
			};
			filtermenu.MenuItems.Add(transform);

			filtermenu.Show(this, PointToClient(Cursor.Position));
		}

		#endregion
	}
}