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

using OBS;

namespace test
{
	public class Scene : ObsScene
	{
		private readonly ObsScene _instance;

		public BindingList<Item> Items { get; set; }

		public Scene(string name)
			: base(name)
		{
			_instance = GetBase();
			Items = new BindingList<Item>();
		}

		public Scene(IntPtr pointer)
			: base(pointer)
		{
			_instance = GetBase();
			Items = new BindingList<Item>();
		}

		public ObsScene GetInstance()
		{
			return _instance;
		}

		public string Name
		{
			get { return GetName(); }
		}

		public Item Add(Source source, string name)
		{
			var sceneitem = base.Add(source);
			var item = new Item(sceneitem.GetPointer()) { Name = name };
			Items.Insert(0, item);
			return item;
		}

		public void ClearItems()
		{
			foreach (var item in Items)
			{
				item.Remove();
				item.Dispose();
			}

			Items.Clear();
		}
	}
}