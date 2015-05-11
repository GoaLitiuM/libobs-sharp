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
using System.Diagnostics;

using OBS;

using test.Utility;

namespace test
{
	public class Scene : ObsScene
	{
		public BindingList<Item> Items { get; set; }

		public Scene(string name)
			: base(name)
		{
			Items = new BindingList<Item>();
		}

		public Scene(IntPtr pointer)
			: base(pointer)
		{
			Items = new BindingList<Item>();
		}

		/// <summary>
		/// Returns the name of the scene from obs-lib
		/// </summary>
		public string Name
		{
			get { return GetName(); }
		}

		/// <summary>
		/// Adds an Item to Items
		/// </summary>
		/// <param name="source">Source to use to create item</param>
		/// <param name="name">Name of the item (UI only)</param>
		/// <returns></returns>
		public Item Add(Source source, string name)
		{
			var sceneitem = base.Add(source);
			var item = new Item(sceneitem.GetPointer()) { Name = name };
			return item;
		}

		/// <summary>
		/// Removes, Disposes and Clears all items from Items
		/// </summary>
		public void ClearItems()
		{
			foreach (var item in Items)
			{
				item.Remove();
				item.Dispose();
			}

			Items.Clear();
		}

		/// <summary>
		///	Moves Item in both the local list and in the obs viewport
		/// </summary>
		/// <param name="item">Item to move</param>
		/// <param name="direction">Where to move the item to</param>
		/// <returns>New index of "item"</returns>
		public int MoveItem(Item item, obs_order_movement direction)
		{
			var oldindex = Items.IndexOf(item);
			int newindex = -1;
			switch (direction)
			{
				case obs_order_movement.OBS_ORDER_MOVE_UP:
					newindex = oldindex - 1;
					break;
				case obs_order_movement.OBS_ORDER_MOVE_DOWN:
					newindex = oldindex + 1;
					break;
				case obs_order_movement.OBS_ORDER_MOVE_TOP:
					newindex = 0;
					break;
				case obs_order_movement.OBS_ORDER_MOVE_BOTTOM:
					newindex = Items.Count - 1;
					break;
			}
			
			item.SetOrder(direction);

			Items.Move(oldindex, newindex);
			
			Debug.WriteLine("{0} new index is {1}", item.Name, newindex);
			return newindex;
		}
	}
}