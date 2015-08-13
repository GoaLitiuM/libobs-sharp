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
	using delegateTuple = Tuple<libobs.draw_callback, IntPtr>;

	public class ObsDisplay : IDisposable
	{
		internal IntPtr instance;   //pointer to unmanaged object

		// stored references to all draw delegates
		public static List<delegateTuple> delegateRefs = new List<delegateTuple>();

		public ObsDisplay(libobs.gs_init_data graphicsData)
		{
			instance = libobs.obs_display_create(ref graphicsData);

			if (instance == null)
				throw new ApplicationException("obs_display_create failed");
		}

		public ObsDisplay(IntPtr ptr)
		{
			instance = ptr;
		}

		public unsafe void Dispose()
		{
			if (instance == IntPtr.Zero)
				return;

			libobs.obs_display_destroy(instance);

			instance = IntPtr.Zero;
		}

		public unsafe IntPtr GetPointer()
		{
			return instance;
		}

		public unsafe void Resize(uint cx, uint cy)
		{
			libobs.obs_display_resize(instance, cx, cy);
		}

		public unsafe void AddDrawCallback(libobs.draw_callback callback, IntPtr param)
		{
			// store the delegate tuple to prevent delegate getting removed
			// by garbage collector

			delegateTuple tuple = new delegateTuple(callback, param);
			delegateRefs.Add(tuple);
			libobs.obs_display_add_draw_callback(instance, tuple.Item1, tuple.Item2);
		}

		public unsafe void RemoveDrawCallback(libobs.draw_callback callback, IntPtr param)
		{
			// use the reference of the archived delegate.

			// if delegate was created as the method were called (user passed
			// function as a first parameter instead of the actual delegate),
			// it would not match with the callback stored in libobs.

			delegateTuple tuple = new delegateTuple(callback, param);
			int index = delegateRefs.IndexOf(tuple);
			if (index != -1)
			{
				tuple = delegateRefs[index];
				delegateRefs.RemoveAt(index);
			}

			libobs.obs_display_remove_draw_callback(instance, tuple.Item1, tuple.Item2);
		}
	}
}