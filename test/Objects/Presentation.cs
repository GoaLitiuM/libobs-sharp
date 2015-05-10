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

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using OBS;

using test.Utility;

namespace test.Objects
{
	public sealed class Presentation : IDisposable
	{
		public void Dispose()
		{
			foreach (var scene in Scenes)
				scene.ClearItems();

			
			foreach (var source in Sources)
			{
				source.Remove();
				source.Dispose();
			}

			foreach (var scene in Scenes)
				scene.Dispose();
		}

		/// <summary>
		/// The currently selected Scene
		/// </summary>
		public Scene SelectedScene { get; private set; }

		/// <summary>
		/// The currently selected Item
		/// </summary>
		public Item SelectedItem { get; private set; }

		/// <summary>
		/// The currently selected Source
		/// </summary>
		public Source SelectedSource { get; private set; }

		/// <summary>
		/// Gets the index of the currently selected Scene. Sets the selected Scene by index.
		/// </summary>
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
		
		/// <summary>
		/// Gets the index of the currently selected Item. Sets the selected Item by index.
		/// </summary>
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

		/// <summary>
		/// Gets the index of the currently selected Source. Sets the selected Source by index.
		/// </summary>
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

		/// <summary>
		/// Scenes contained in this presentation
		/// </summary>
		public readonly BindingList<Scene> Scenes = new BindingList<Scene>();

		/// <summary>
		///  Sources contained in this presentation
		/// </summary>
		public readonly BindingList<Source> Sources = new BindingList<Source>();

		/// <summary>
		///  Creates and adds a new Scene to the presentation
		/// </summary>
		/// <returns>The newly created Scene</returns>
		public Scene AddScene()
		{
			Scene scene = new Scene("test scene (" + (Scenes.Count + 1) + ")");

			Scenes.Add(scene);

			SelectedScene = scene;
			return scene;
		}
		
		/// <summary>
		/// Deletes the currently selected Scene
		/// </summary>
		public void DelScene()
		{
			if (SelectedScene == null || Scenes.Count == 1) return;

			SelectedScene.ClearItems();

			SelectedScene.Dispose();

			int oldindex = Scenes.IndexOf(SelectedScene);

			Scenes.RemoveAt(oldindex);

			SelectedScene = oldindex < Scenes.Count ? Scenes[oldindex] : Scenes.Last();
		}

		/// <summary>
		/// Creates and adds a new Item to the currently selected Scene
		/// </summary>
		/// <param name="source">Source to use to create a new item</param>
		/// <returns>The newly created Item</returns>
		public ObsSceneItem AddItem(Source source)
		{
			var item = SelectedScene.Add(source, source.Name);

			item.Position = new Vector2(0f, 0f);
			item.Scale = new Vector2(1.0f, 1.0f);
			item.SetBounds(new Vector2(1280, 720), ObsBoundsType.ScaleInner, ObsAlignment.Center);

			SelectedItem = item;

			return item;
		}

		/// <summary>
		/// Deletes the currently selected Item
		/// </summary>
		public void DelItem()
		{
			if (SelectedItem == null) return;

			SelectedItem.Remove();
			SelectedItem.Dispose();

			int oldindex = SelectedScene.Items.IndexOf(SelectedItem);

			SelectedScene.Items.Remove(SelectedItem);

			if (SelectedScene.Items.Any())
			{
				SelectedItem = oldindex < SelectedScene.Items.Count ? SelectedScene.Items[oldindex] : SelectedScene.Items.Last();
			}
		}

		/// <summary>
		/// Creates a new Source and adds it to the source list
		/// </summary>
		/// <param name="id">ID of type of source to use</param>
		/// <param name="name">Name of the source</param>
		/// <returns>The newly created source</returns>
		public Source AddSource(string id, string name)
		{
			Source source = new Source(ObsSourceType.Input, id, name);

			Sources.Add(source);

			SelectedSource = source;

			return source;
		}

		/// <summary>
		/// Deletes the currently selected Source
		/// </summary>
		public void DelSource()
		{
			if (SelectedSource == null) return;

			var pointer = SelectedSource.GetPointer();

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

			SelectedSource.Remove();
			SelectedSource.Dispose();

			var oldindex = Sources.IndexOf(SelectedSource);

			Sources.Remove(SelectedSource);

			if (Sources.Any())
			{
				SelectedSource = oldindex < Sources.Count ? Sources[oldindex] : Sources.Last();
			}
		}

		/// <summary>
		/// Creates and shows a Source context menu at the mouse pointer
		/// </summary>
		/// <param name="sender">Form from which this method is called</param>
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

		/// <summary>
		/// Creates and shows an Add Source context menu at the mouse pointer
		/// </summary>
		/// <param name="sender">Form from which this method is called</param>
		public void ShowAddSourceContextMenu(Form sender, bool deleteaftercomplete = false)
		{
			// TODO: there's a dirty hack in place here
			/* 
			 * adds an item then removes it once its complete because it wont render unless the source is visible on the scene
			 * fix it so sources are displayed even if not being shown on canvas
			 */

			var inputmenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };

			foreach (string inputType in Obs.GetSourceInputTypes())
			{
				string type = inputType;

				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Input, inputType);

				var menuitem = new ToolStripMenuItem(displayname + " (" + type + ")");

				menuitem.Click += (s, args) =>
				{
					var source = AddSource(type, displayname + (Sources.Count + 1));

					AddItem(source);

					var prop = new TestProperties(source);

					if (deleteaftercomplete)
					{
						prop.Disposed += (o, eventArgs) =>
						{
							Item item = SelectedScene.Items.Last();
							item.Remove();
							item.Dispose();
							SelectedScene.Items.Remove(item);
						};
					}
					prop.Show();
				};

				inputmenu.Items.Add(menuitem);
			}

			inputmenu.Show(sender, sender.PointToClient(Cursor.Position));
		}

		/// <summary>
		/// Creates and shows an Item context menu at the mouse pointer
		/// </summary>
		/// <param name="sender">Form from which this method is called</param>
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
	}
}
