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
	public class ObsDataArray : IDisposable, IEnumerable<ObsData>
	{
		internal IntPtr instance;    //pointer to unmanaged object

		public ObsDataArray()
		{
			instance = libobs.obs_data_array_create();
		}

		public ObsDataArray(IntPtr ptr)
		{
			instance = ptr;

			libobs.obs_data_array_addref(instance);
		}

		public unsafe void Dispose()
		{
			if (instance == IntPtr.Zero)
				return;

			libobs.obs_data_array_release(instance);

			instance = IntPtr.Zero;
		}

		public IntPtr GetPointer()
		{
			return instance;
		}

		public int Count
		{
			get { return (int)libobs.obs_data_array_count(instance); }
		}

		public int Add(ObsData obj)
		{
			return (int)libobs.obs_data_array_push_back(instance, obj.GetPointer());
		}

		public void Insert(ObsData obj, int index)
		{
			if (index < 0 || index > Count)
				throw new IndexOutOfRangeException();

			libobs.obs_data_array_insert(instance, (UIntPtr)index, obj.GetPointer());
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index > Count)
				throw new IndexOutOfRangeException();

			libobs.obs_data_array_erase(instance, (UIntPtr)index);
		}

		public ObsData this[int index]
		{
			get
			{
				if (index < 0 || index > Count)
					throw new IndexOutOfRangeException();

				IntPtr ptr = libobs.obs_data_array_item(instance, (UIntPtr)index);
				if (ptr == IntPtr.Zero)
					return null;

                return new ObsData(ptr);
			}
		}

		public IEnumerator<ObsData> GetEnumerator()
		{
			int count = Count;
			for (int i = 0; i < count; i++)
			{
				ObsData item = ItemAt(i);
				yield return item;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public ObsData ItemAt(int index)
		{
			if (index < 0 || index > Count)
				throw new IndexOutOfRangeException();

			IntPtr ptr = libobs.obs_data_array_item(instance, (UIntPtr)index);
			if (ptr == IntPtr.Zero)
				return null;

            return new ObsData(ptr);
		}
	}
}