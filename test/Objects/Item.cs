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

using OBS;

namespace test
{
	public class Item : ObsSceneItem
	{
		private readonly ObsSceneItem _instance;

		public Item(IntPtr sceneItem)
			: base(sceneItem)
		{
			_instance = GetBase();
		}

		/// <summary>
		/// Gets or Sets the Name of Item (UI only)
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The base class which this is inherited from
		/// </summary>
		/// <returns>The base ObsSceneItem of this Item</returns>
		public ObsSceneItem GetInstance()
		{
			return _instance;
		}
	}
}