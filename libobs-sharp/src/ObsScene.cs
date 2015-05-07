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

namespace OBS
{
	public class ObsScene : IDisposable, IEnumerable<ObsSceneItem>
	{
		internal IntPtr instance;    //pointer to unmanaged object

		public unsafe ObsScene(string name)
		{
			instance = libobs.obs_scene_create(name);

			if (instance == null)
				throw new ApplicationException("obs_scene_create failed");
		}

		public unsafe ObsScene(IntPtr pointer)
		{
			instance = pointer;

			if (instance == null)
				throw new ApplicationException("obs_scene_create failed");
		}

		public unsafe void Dispose()
		{
			if (instance == IntPtr.Zero)
				return;

			libobs.obs_scene_release(instance);

			instance = IntPtr.Zero;
		}

		public unsafe IntPtr GetPointer()
		{
			return instance;
		}

		/// <summary> Gets name of the underlying source. </summary>
		public unsafe string GetName()
		{
			using (ObsSource source = GetSource())
				return source.Name;
		}

		public string ListName
		{
			get { return GetName(); }
		}

		/// <summary> Sets name of the underlying source. </summary>
		public unsafe void SetName(string name)
		{
			using (ObsSource source = GetSource())
				source.Name = name;
		}

		public unsafe ObsSource GetSource()
		{
			IntPtr ptr = libobs.obs_scene_get_source(instance);
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsSource(ptr);
		}

		public unsafe ObsSceneItem Add(ObsSource source)
		{
			IntPtr ptr = libobs.obs_scene_add(instance, source.GetPointer());
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsSceneItem(ptr);
		}

		public unsafe void EnumItems(libobs.sceneitem_enum_callback callback, IntPtr param)
		{
			libobs.obs_scene_enum_items(instance, callback, param);
		}

		public unsafe ObsSceneItem[] GetItems()
		{
			List<ObsSceneItem> items = new List<ObsSceneItem>();
			EnumItems((IntPtr scene, IntPtr item, IntPtr data) =>
			{
				items.Add(new ObsSceneItem(item));
				return true;
			}, IntPtr.Zero);

			return items.ToArray();
		}

		public IEnumerator<ObsSceneItem> GetEnumerator()
		{
			ObsSceneItem[] items = GetItems();
			foreach (ObsSceneItem item in items)
				yield return item;

			foreach (ObsSceneItem item in items)
				item.Dispose();
        }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
