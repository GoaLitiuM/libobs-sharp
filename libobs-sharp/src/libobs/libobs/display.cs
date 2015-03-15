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
using System.Runtime.InteropServices;

namespace OBS
{
	using obs_display_t = IntPtr;

	using uint32_t = UInt32;

	public static partial class libobs
	{
		/* ------------------------------------------------------------------------- */
		/* Display context */

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_display_t obs_display_create(ref gs_init_data graphics_data);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_display_destroy(obs_display_t display);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_display_resize(obs_display_t display, uint32_t cx, uint32_t cy);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_display_add_draw_callback(obs_display_t display, draw_callback draw, IntPtr param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_display_remove_draw_callback(obs_display_t display, draw_callback draw, IntPtr param);
	}
}