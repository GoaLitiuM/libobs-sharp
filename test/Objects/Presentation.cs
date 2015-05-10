/***************************************************************************
	Copyright (C) 2014-2015 by Nick Thijssen <lamah83@gmaill.com>

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

using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using OBS;

using test.Utility;

namespace test.Objects
{
	public sealed class Presentation
	{
		public Scene SelectedScene { get; private set; }

		public Item SelectedItem { get; private set; }

		public Source SelectedSource { get; private set; }

		public int SceneIndex
		{
			get
			{
				return SelectedScene != null ? Scenes.IndexOf(SelectedScene) : -1;
			}
			set
			{
				SelectedScene = value != -1 ? Scenes[value] : null;
				if (SelectedScene != null)
				{
					Obs.SetOutputScene(0, SelectedScene);
				}
			}
		}

		public int ItemIndex
		{
			get
			{
				return SelectedItem != null ? SelectedScene.Items.IndexOf(SelectedItem) : -1;
			}
			set
			{
				SelectedItem = value != -1 ? SelectedScene.Items[value] : null;
			}
		}

		public int SourceIndex
		{
			get
			{
				return SelectedSource != null ? Sources.IndexOf(SelectedSource) : -1;
			}
			set
			{

				SelectedSource = value != -1 ? Sources[value] : null;
			}
		}

		public readonly BindingList<Scene> Scenes = new BindingList<Scene>();

		public readonly BindingList<Source> Sources = new BindingList<Source>();

		public Scene AddScene()
		{
			// Create new scene
			Scene scene = new Scene("test scene (" + (Scenes.Count + 1) + ")");

			// Show the scene in the viewport
			//Obs.SetOutputScene(0, scene);

			// Add scene to scenelist
			Scenes.Add(scene);

			// select the new scene
			SelectedScene = scene;
			return scene;
		}

		public void DelScene()
		{
			// dont delete if only scene or no scene selected
			if (SelectedScene == null || Scenes.Count == 1) return;

			// Dispose of all items
			SelectedScene.ClearItems();

			// Dispose of scene
			SelectedScene.Dispose();

			// store old index
			int oldindex = Scenes.IndexOf(SelectedScene);

			// remove scene from list
			Scenes.RemoveAt(oldindex);

			// Select the next scene
			if (oldindex < Scenes.Count)
			{
				SelectedScene = Scenes[oldindex];
			}
			else
			{
				SelectedScene = Scenes.Last();
			}
		}

		public ObsSceneItem AddItem(Source source)
		{
			// generate an item from soruce
			var item = SelectedScene.Add(source, source.Name);

			// set its proportions
			item.Position = new Vector2(0f, 0f);
			item.Scale = new Vector2(1.0f, 1.0f);
			item.SetBounds(new Vector2(1280, 720), ObsBoundsType.ScaleInner, ObsAlignment.Center);

			// select new item
			SelectedItem = item;

			return item;
		}

		public void DelItem()
		{
			// dont delete if no scene is deleted
			if (SelectedItem == null) return;

			// dispose of scen
			SelectedItem.Remove();
			SelectedItem.Dispose();

			// store old index
			int oldindex = SelectedScene.Items.IndexOf(SelectedItem);

			// remove disposed item from list
			SelectedScene.Items.Remove(SelectedItem);

			// select next item
			if (SelectedScene.Items.Any())
			{
				if (oldindex < SelectedScene.Items.Count)
				{
					SelectedItem = SelectedScene.Items[oldindex];
				}
				else
				{
					SelectedItem = SelectedScene.Items.Last();
				}
			}
		}

		public Source AddSource(string id, string name)
		{
			// Create a new source
			Source source = new Source(ObsSourceType.Input, id, name);

			// Add the source to the source list
			Sources.Add(source);

			// Select new item
			SelectedSource = source;

			return source;
		}

		public void DelSource()
		{
			if (SelectedSource == null) return;

			// duplicate pointer for REASONS
			var pointer = SelectedSource.GetPointer();

			// remove all scene items that use the same pointer as the selected source
			foreach (var scene in Scenes)
			{
				scene.Items.RemoveAll(x =>
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
			SelectedSource.Remove();
			SelectedSource.Dispose();

			// store index because a remove resets index to -1
			var oldindex = Sources.IndexOf(SelectedSource);

			// remove the source from the source list
			Sources.Remove(SelectedSource);

			// select the next source
			if (Sources.Any())
			{
				if (oldindex < Sources.Count)
				{
					SelectedSource = Sources[oldindex];
				}
				else
				{
					SelectedSource = Sources.Last();
				}
			}
		}

		public void ShowItemContextMenu(Form sender)
		{
			var top = new ToolStripMenuItem("Move to &Top");
			top.Click += (o, args) =>
			{
				ItemIndex = SelectedScene.MoveItem(SelectedItem, obs_order_movement.OBS_ORDER_MOVE_TOP);
			};

			var up = new ToolStripMenuItem("Move &Up");
			up.Click += (o, args) =>
			{
				ItemIndex = SelectedScene.MoveItem(SelectedItem, obs_order_movement.OBS_ORDER_MOVE_UP);
			};

			var down = new ToolStripMenuItem("Move &Down");
			down.Click += (o, args) =>
			{
				ItemIndex = SelectedScene.MoveItem(SelectedItem, obs_order_movement.OBS_ORDER_MOVE_DOWN);
			};

			var bottom = new ToolStripMenuItem("Move to &Bottom");
			bottom.Click += (o, args) =>
			{
				ItemIndex = SelectedScene.MoveItem(SelectedItem, obs_order_movement.OBS_ORDER_MOVE_BOTTOM);
			};

			var transform = new ToolStripMenuItem("&Edit Transform Options...");
			transform.Click += (o, args) =>
			{
				var transformfrm = new TestTransform(SelectedItem);
				transformfrm.ShowDialog(sender);
			};

			var visible = new ToolStripBindableMenuItem
			{
				Text = "&Visible",
				CheckOnClick = true
			};
			visible.DataBindings.Add(new Binding("Checked", SelectedItem, "Visible", false, DataSourceUpdateMode.OnPropertyChanged));


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

			ordermenu.Show(sender, sender.PointToClient(Cursor.Position));

			int index = ItemIndex;
			top.Enabled = up.Enabled = index != 0;
			down.Enabled = bottom.Enabled = index != SelectedScene.Items.Count - 1;
		}

		public void ShowSourceContextMenu(Form sender)
		{
			//TODO: actually use this somewhere :p
			var filtermenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };

			foreach (var filterType in Obs.GetSourceFilterTypes())
			{
				string type = filterType;
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Filter, filterType);
				int index = Sources.Count + 1;

				var menuitem = new ToolStripMenuItem(displayname + " (" + filterType + ")");

				menuitem.Click += (s, args) =>
				{
					ObsSource filter = new ObsSource(ObsSourceType.Filter, type, displayname + index);
					SelectedSource.AddFilter(filter);
				};

				filtermenu.Items.Add(menuitem);
			}
			filtermenu.Items.Add("-");
			var properties = new ToolStripMenuItem("Edit Source Properties...");
			properties.Click += (s, args) =>
			{
				var propfrm = new TestProperties(SelectedSource);
				propfrm.ShowDialog(sender);
			};
			filtermenu.Items.Add(properties);

			filtermenu.Show(sender, sender.PointToClient(Cursor.Position));
		}

		public void ShowAddSourceContextMenu(Form sender, bool deleteaftercomplete = false)
		{
			// TODO: there's a dirty hack in place here
			/* 
			 * adds an item then removes it once its complete because it wont render unless the source is visible on the scene
			 * fix it so sources are displayed even if not being shown on canvas
			 */

			// create source context menu
			var inputmenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };

			// create a context menu item for each source type
			foreach (string inputType in Obs.GetSourceInputTypes())
			{
				// The variable dissapears when the loop ends so it needs to be copied
				string type = inputType;

				// create display name
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Input, inputType);

				// create menu item
				var menuitem = new ToolStripMenuItem(displayname + " (" + type + ")");

				// attach menu item click event
				menuitem.Click += (s, args) =>
				{
					// create a source based off the menu item name
					var source = AddSource(type, displayname + (Sources.Count + 1));

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
							Item item = SelectedScene.Items.Last();
							item.Remove();
							item.Dispose();
							SelectedScene.Items.Remove(item);
						};
					}
					// show property dialog
					prop.Show();
				};

				inputmenu.Items.Add(menuitem);
			}

			inputmenu.Show(sender, sender.PointToClient(Cursor.Position));
		}
	}
}
