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
using System.Runtime.CompilerServices;

using OBS;

using test.Utility;

namespace test.Objects
{
	public sealed class Presentation : INotifyPropertyChanged
	{
		public Scene SelectedScene
		{
			get { return _selectedScene; }
			set
			{
				_selectedScene = value;
				Obs.SetOutputScene(0, value);
				OnPropertyChanged(); 
			}
		}

		public Item SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;
				OnPropertyChanged(); 
			}
		}

		public Source SelectedSource
		{
			get { return _selectedSource; }
			set
			{
				_selectedSource = value;
				OnPropertyChanged(); 
			}
		}

		public readonly BindingList<Scene> Scenes = new BindingList<Scene>();

		public readonly BindingList<Source> Sources = new BindingList<Source>();
		private int _sceneIndex;
		private int _itemIndex;
		private int _sourceIndex;
		private Scene _selectedScene;
		private Item _selectedItem;
		private Source _selectedSource;

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

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
