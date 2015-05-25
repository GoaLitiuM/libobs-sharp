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
	using obs_data_t = IntPtr;
	using obs_properties_t = IntPtr;
	using obs_property_t = IntPtr;

	using size_t = UIntPtr;
	using int64_t = Int64;

	public static partial class libobs
	{
		[UnmanagedFunctionPointer(importCall)]
		public delegate bool obs_property_modified_t(obs_properties_t props, obs_property_t property, obs_data_t settings);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_property_set_modified_callback(obs_property_t p,
			[MarshalAs(UnmanagedType.FunctionPtr)] obs_property_modified_t modified);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_property_modified(obs_property_t p, obs_data_t settings);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_property_button_clicked(obs_property_t p, IntPtr obj);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_property_set_visible(obs_property_t p, bool visible);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_property_set_enabled(obs_property_t p, bool enabled);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_property_set_description(obs_property_t p,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string description);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_property_name(obs_property_t p);
		
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_property_description(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_property_type obs_property_get_type(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_property_enabled(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_property_visible(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_property_next(out obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern int obs_property_int_min(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern int obs_property_int_max(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern int obs_property_int_step(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_number_type obs_property_int_type(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern double obs_property_float_min(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern double obs_property_float_max(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern double obs_property_float_step(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_number_type obs_property_float_type(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_text_type obs_proprety_text_type(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_path_type obs_property_path_type(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_property_path_filter(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_property_path_default_path(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_combo_type obs_property_list_type(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_combo_format obs_property_list_format(obs_property_t p);

		//EXPORT void obs_property_list_clear(obs_property_t *p);

		//EXPORT size_t obs_property_list_add_string(obs_property_t *pconst char *name, const char *val);
		//EXPORT size_t obs_property_list_add_int(obs_property_t *pconst char *name, long long val);
		//EXPORT size_t obs_property_list_add_float(obs_property_t *pconst char *name, double val);
		//EXPORT void obs_property_list_insert_string(obs_property_t *p, size_t idxconst char *name, const char *val);
		//EXPORT void obs_property_list_insert_int(obs_property_t *p, size_t idxconst char *name, long long val);
		//EXPORT void obs_property_list_insert_float(obs_property_t *p, size_t idxconst char *name, double val);
		//EXPORT void obs_property_list_item_disable(obs_property_t *p, size_t idxbool disabled);
		//EXPORT bool obs_property_list_item_disabled(obs_property_t *p, size_t idx);
		//EXPORT void obs_property_list_item_remove(obs_property_t *p, size_t idx);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern size_t obs_property_list_item_count(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_property_list_item_name(obs_property_t p, size_t idx);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_property_list_item_string(obs_property_t p, size_t idx);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern int64_t obs_property_list_item_int(obs_property_t p, size_t idx);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern double obs_property_list_item_float(obs_property_t p, size_t idx);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_properties_text_set_type(obs_property_t p, obs_text_type type);

		[UnmanagedFunctionPointer(importCall)]
		public delegate bool obs_property_clicked_t(obs_properties_t props, obs_property_t property, IntPtr data);

		public enum obs_property_type : int
		{
			OBS_PROPERTY_INVALID,
			OBS_PROPERTY_BOOL,
			OBS_PROPERTY_INT,
			OBS_PROPERTY_FLOAT,
			OBS_PROPERTY_TEXT,
			OBS_PROPERTY_PATH,
			OBS_PROPERTY_LIST,
			OBS_PROPERTY_COLOR,
			OBS_PROPERTY_BUTTON,
			OBS_PROPERTY_FONT,
		};

		public enum obs_combo_format : int
		{
			OBS_COMBO_FORMAT_INVALID,
			OBS_COMBO_FORMAT_INT,
			OBS_COMBO_FORMAT_FLOAT,
			OBS_COMBO_FORMAT_STRING,
		};

		public enum obs_combo_type : int
		{
			OBS_COMBO_TYPE_INVALID,
			OBS_COMBO_TYPE_EDITABLE,
			OBS_COMBO_TYPE_LIST,
		};

		public enum obs_path_type : int
		{
			OBS_PATH_FILE,
			OBS_PATH_DIRECTORY,
		};

		public enum obs_text_type : int
		{
			OBS_TEXT_DEFAULT,
			OBS_TEXT_PASSWORD,
			OBS_TEXT_MULTILINE,
		};

		public enum obs_number_type : int
		{
			OBS_NUMBER_SCROLLER,
			OBS_NUMBER_SLIDER
		};
	}
}