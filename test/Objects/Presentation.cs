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
		/// <summary> Scenes contained in this presentation </summary>
		public readonly BindingList<Scene> Scenes = new BindingList<Scene>();

		/// <summary> Sources contained in this presentation </summary>
		public readonly BindingList<Source> Sources = new BindingList<Source>();

		/// <summary> The currently selected Scene </summary>
		public Scene SelectedScene { get; private set; }

		/// <summary> The currently selected Item </summary>
		public Item SelectedItem { get; private set; }

		/// <summary> The currently selected Source </summary>
		public Source SelectedSource { get; private set; }

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

		public void SetScene(int index)
		{
			SelectedScene = Scenes[index];
			Obs.SetOutputScene(0, SelectedScene);
		}

		public void SetScene(Scene scene)
		{
			if (scene == SelectedScene)
				return;

			SelectedScene = scene;
			Obs.SetOutputScene(0, SelectedScene);
		}

		public void SetItem(int index)
		{
			SelectedItem = index == -1 ? null : SelectedScene.Items[index];
			if (SelectedItem != null)
			{
				foreach (Item sceneitem in SelectedScene.Items)
				{
					sceneitem.Selected = sceneitem == SelectedItem;
				}
			}
		}

		public void SetItem(Item item)
		{
			SelectedItem = item;
			if (SelectedItem != null)
			{
				foreach (Item sceneitem in SelectedScene.Items)
				{
					sceneitem.Selected = sceneitem == SelectedItem;
				}
			}
		}

		public void SetSource(int index)
		{
			SelectedSource = index == -1 ? null : Sources[index];
		}

		public void SetSource(Source source)
		{
			SelectedSource = source;
		}

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
			if (SelectedScene == null || Scenes.Count == 1)
				return;

			SelectedScene.ClearItems();
			SelectedScene.Dispose();

			int oldindex = Scenes.IndexOf(SelectedScene);
			Scenes.RemoveAt(oldindex);

			SelectedScene = oldindex < Scenes.Count ? Scenes[oldindex] : Scenes.Last();
		}

		public Item CreateItem(Source source)
		{
			Item item = SelectedScene.Add(source, source.Name);

			item.Position = new Vector2(0f, 0f);
			item.Scale = new Vector2(1.0f, 1.0f);
			item.SetBounds(new Vector2(1280, 720), ObsBoundsType.ScaleInner, ObsAlignment.Center);

			return item;
		}

		public void AddItem(Item item)
		{
			SelectedScene.Items.Insert(0, item);

			SetItem(0);
		}

		/// <summary>
		/// Deletes the currently selected Item
		/// </summary>
		public void DelItem()
		{
			if (SelectedItem == null)
				return;

			SelectedItem.Remove();
			SelectedItem.Dispose();

			int oldindex = SelectedScene.Items.IndexOf(SelectedItem);

			SelectedScene.Items.Remove(SelectedItem);

			if (SelectedScene.Items.Any())
			{
				SetItem(oldindex < SelectedScene.Items.Count ? SelectedScene.Items[oldindex] : SelectedScene.Items.Last());
			}
		}

		public Source CreateSource(string id, string name)
		{
			return new Source(id, name);
		}

		public void AddSource(Source source)
		{
			Sources.Insert(0, source);

			SetSource(0);
		}

		/// <summary>
		/// Deletes the currently selected Source
		/// </summary>
		public void DelSource()
		{
			if (SelectedSource == null)
				return;

			Source delsource = SelectedSource;

			foreach (var scene in Scenes)
			{
				scene.Items.RemoveAll(x =>
				{
					using (var source = x.GetSource())
					{
						if (source.GetPointer() != delsource.GetPointer()) return false;
					}
					x.Remove();
					x.Dispose();
					return true;
				});
			}

			delsource.Remove();
			delsource.Dispose();

			var oldindex = Sources.IndexOf(delsource);

			Sources.Remove(delsource);

			if (Sources.Any())
			{
				SetSource(oldindex < Sources.Count ? Sources[oldindex] : Sources.Last());
			}
		}

		/// <summary>
		/// Creates and shows a Source context menu at the mouse pointer
		/// </summary>
		public ContextMenuStrip SourceContextMenu()
		{
			var filtermenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };

			var enabled = new ToolStripBindableMenuItem
			{
				Text = "&Enabled",
				CheckOnClick = true,
			};
			enabled.DataBindings.Add(new Binding("Checked", SelectedSource, "Enabled", false, DataSourceUpdateMode.OnPropertyChanged));

			var muted = new ToolStripBindableMenuItem
			{
				Text = "&Muted",
				CheckOnClick = true,
			};
			muted.DataBindings.Add(new Binding("Checked", SelectedSource, "Muted", false, DataSourceUpdateMode.OnPropertyChanged));

			var filters = new ToolStripMenuItem("Edit Source Filters...");
			filters.Click += (sender, args) =>
			{
				var filterprop = new TestFilter(SelectedSource);
				filterprop.ShowDialog();
			};

			var properties = new ToolStripMenuItem("Edit Source Properties...");
			properties.Click += (sender, args) =>
			{
				var sourceprop = new TestProperties(SelectedSource);
				sourceprop.ShowDialog();
			};

			filtermenu.Items.AddRange(new ToolStripItem[]
			                          {
				                          enabled,
										  muted,
										  new ToolStripSeparator(), 
										  filters,
										  properties
			                          });

			return filtermenu;
		}

		/// <summary>
		/// Creates and shows an Add Source context menu at the mouse pointer
		/// </summary>
		public ContextMenuStrip AddSourceContextMenu()
		{
			var inputmenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };

			foreach (string inputType in Obs.GetSourceInputTypes())
			{
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Input, inputType);

				var menuitem = new ToolStripMenuItem(displayname + " (" + inputType + ")")
							   {
								   Tag = Tuple.Create(inputType, displayname + (Sources.Count + 1))
							   };

				inputmenu.Items.Add(menuitem);
			}
			return inputmenu;
		}

		/// <summary>
		/// Creates and shows an Item context menu at the mouse pointer
		/// </summary>
		public ContextMenuStrip ItemContextMenu()
		{
			var top = new ToolStripMenuItem("Move to &Top");
			top.Click += (o, args) =>
			{
				SetItem(SelectedScene.MoveItem(SelectedItem, obs_order_movement.OBS_ORDER_MOVE_TOP));
			};

			var up = new ToolStripMenuItem("Move &Up");
			up.Click += (o, args) =>
			{
				SetItem(SelectedScene.MoveItem(SelectedItem, obs_order_movement.OBS_ORDER_MOVE_UP));
			};

			var down = new ToolStripMenuItem("Move &Down");
			down.Click += (o, args) =>
			{
				SetItem(SelectedScene.MoveItem(SelectedItem, obs_order_movement.OBS_ORDER_MOVE_DOWN));
			};

			var bottom = new ToolStripMenuItem("Move to &Bottom");
			bottom.Click += (o, args) =>
			{
				SetItem(SelectedScene.MoveItem(SelectedItem, obs_order_movement.OBS_ORDER_MOVE_BOTTOM));
			};

			var transform = new ToolStripMenuItem("&Edit Transform Options...");
			transform.Click += (o, args) =>
			{
				var transformfrm = new TestTransform(SelectedItem);
				transformfrm.ShowDialog();
			};

			var prop = new ToolStripMenuItem("&Edit Source Properties...");
			prop.Click += (sender, args) =>
			{
				var propfrm = new TestProperties(SelectedItem.GetSource());
				propfrm.ShowDialog();
			};

			var visible = new ToolStripBindableMenuItem
						  {
							  Text = "&Visible",
							  CheckOnClick = true
						  };
			visible.DataBindings.Add(new Binding("Checked", SelectedItem, "Visible", false, DataSourceUpdateMode.OnPropertyChanged));

			var ordermenu = new ContextMenuStrip
							{
								Renderer = new AccessKeyMenuStripRenderer()
							};

			ordermenu.Items.AddRange(new ToolStripItem[]
			                         {
				                         top, 
				                         up, 
				                         down, 
				                         bottom, 
				                         new ToolStripSeparator(),
				                         visible,
				                         new ToolStripSeparator(), 
				                         transform,
										 prop
			                         });

			int index = SelectedScene.Items.IndexOf(SelectedItem);
			top.Enabled = up.Enabled = index != 0;
			down.Enabled = bottom.Enabled = index != SelectedScene.Items.Count - 1;
			return ordermenu;
		}
	}
}
