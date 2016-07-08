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
	using obs_scene_t = IntPtr;
	using obs_sceneitem_t = IntPtr;
	using obs_source_t = IntPtr;

	using size_t = UIntPtr;

	public static partial class libobs
	{
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_scene_t obs_scene_create(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		//EXPORT obs_scene_t *obs_scene_create_private(const char *name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_source_t obs_scene_duplicate(obs_scene_t scene,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name,
			obs_scene_duplicate_type type);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_scene_addref(obs_scene_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_scene_release(obs_scene_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_scene_get_source(obs_scene_t scene);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_scene_t obs_scene_from_source(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_sceneitem_t obs_scene_find_source(obs_scene_t scene,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
		public delegate bool sceneitem_enum_callback(obs_scene_t scene, obs_sceneitem_t sceneItem, IntPtr data);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_scene_enum_items(obs_scene_t scene, sceneitem_enum_callback callback, IntPtr param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_scene_reorder_items(obs_scene_t scene, obs_sceneitem_t item_order, size_t item_order_size);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_sceneitem_t obs_scene_add(obs_scene_t scene, obs_source_t source);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void obs_scene_atomic_update_func(IntPtr data, obs_scene_t scene);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_scene_atomic_update(obs_scene_t scene, obs_scene_atomic_update_func func, IntPtr data);

		public enum obs_scene_duplicate_type : int
		{
			OBS_SCENE_DUP_REFS,
			OBS_SCENE_DUP_COPY,
			OBS_SCENE_DUP_PRIVATE_REFS,
			OBS_SCENE_DUP_PRIVATE_COPY
		};
	}
}