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
	using obs_data_array_t = IntPtr;
	using obs_data_item_t = IntPtr;
	using obs_data_t = IntPtr;

	using int64_t = Int64;
	using size_t = UIntPtr;

	public static partial class libobs
	{
		/* ------------------------------------------------------------------------- */
		/* Main usage functions */

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_t obs_data_create();

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_t obs_data_create_from_json(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string json_string);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_t obs_data_create_from_json_file(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string json_file);
			
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_t obs_data_create_from_json_file_safe(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string json_file,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string backup_ext);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_data_addref(obs_data_t data);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_data_release(obs_data_t data);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_data_get_json(obs_data_t data);
		
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_save_json(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string file);
			
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_save_json_safe(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string file,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string temp_ext,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string backup_ext);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_data_apply(obs_data_t target, obs_data_t apply_data);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_erase(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_clear(obs_data_t data);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]    
		public static extern void obs_data_set_string(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_int(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, int64_t val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_double(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, double val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_bool(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name,
			[MarshalAs(UnmanagedType.I1)] bool val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_obj(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, obs_data_t obj);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_array(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, obs_data_array_t array);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_default_string(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_default_int(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, int64_t val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_default_double(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, double val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_default_bool(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name,
			[MarshalAs(UnmanagedType.I1)] bool val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_default_obj(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, obs_data_t obj);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_autoselect_string(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_autoselect_int(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, int64_t val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_autoselect_double(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, double val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_autoselect_bool(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name,
			[MarshalAs(UnmanagedType.I1)] bool val);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_autoselect_obj(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, obs_data_t obj);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_data_get_string(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern int64_t obs_data_get_int(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern double obs_data_get_double(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_get_bool(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_t obs_data_get_obj(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_array_t obs_data_get_array(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_data_get_default_string(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern int64_t obs_data_get_default_int(obs_data_t data,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern double obs_data_get_default_double(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_get_default_bool(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_t obs_data_get_default_obj(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_array_t obs_data_get_default_array(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_data_get_autoselect_string(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);
		
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern int64_t obs_data_get_autoselect_int(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern double obs_data_get_autoselect_double(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_get_autoselect_bool(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_t obs_data_get_autoselect_obj(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_array_t obs_data_get_autoselect_array(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_array_t obs_data_array_create();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_data_array_addref(obs_data_array_t array);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_data_array_release(obs_data_array_t array);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern size_t obs_data_array_count(obs_data_array_t array);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_t obs_data_array_item(obs_data_array_t array, size_t idx);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern size_t obs_data_array_push_back(obs_data_array_t array, obs_data_t obj);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_data_array_insert(obs_data_array_t array, size_t idx, obs_data_t obj);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_data_array_erase(obs_data_array_t array, size_t idx);

		/* ------------------------------------------------------------------------- */
		/* Item status inspection */

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_has_user_value(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_has_default_value(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_has_autoselect_value(obs_data_t data, 
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_item_has_user_value(obs_data_item_t data);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_item_has_default_value(obs_data_item_t data);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_data_item_has_autoselect_value(obs_data_item_t data);

		//EXPORT void obs_data_unset_user_value(obs_data_t *data, const char *name);
		//EXPORT void obs_data_unset_default_value(obs_data_t *data, const char *name);
		//EXPORT void obs_data_unset_autoselect_value(obs_data_t *data, const char *name);
		//EXPORT void obs_data_item_unset_user_value(obs_data_item_t *data);
		//EXPORT void obs_data_item_unset_default_value(obs_data_item_t *data);
		//EXPORT void obs_data_item_unset_autoselect_value(obs_data_item_t *data);

		//EXPORT obs_data_item_t *obs_data_first(obs_data_t *data);
		//EXPORT obs_data_item_t *obs_data_item_byname(obs_data_t *data, const char *name);
		//EXPORT bool obs_data_item_next(obs_data_item_t **item);
		//EXPORT void obs_data_item_release(obs_data_item_t **item);
		//EXPORT void obs_data_item_remove(obs_data_item_t **item);

		//EXPORT enum obs_data_type obs_data_item_gettype(obs_data_item_t *item);
		//EXPORT enum obs_data_number_type obs_data_item_numtype(obs_data_item_t *item);

		//EXPORT void obs_data_item_set_string(obs_data_item_t **item, const char *val);
		//EXPORT void obs_data_item_set_int(obs_data_item_t **item, long long val);
		//EXPORT void obs_data_item_set_double(obs_data_item_t **item, double val);
		//EXPORT void obs_data_item_set_bool(obs_data_item_t **item, bool val);
		//EXPORT void obs_data_item_set_obj(obs_data_item_t **item, obs_data_t *val);
		//EXPORT void obs_data_item_set_array(obs_data_item_t **itemobs_data_array_t *val);

		//EXPORT void obs_data_item_set_default_string(obs_data_item_t **itemconst char *val);
		//EXPORT void obs_data_item_set_default_int(obs_data_item_t **item, long long val);
		//EXPORT void obs_data_item_set_default_double(obs_data_item_t **item, double val);
		//EXPORT void obs_data_item_set_default_bool(obs_data_item_t **item, bool val);
		//EXPORT void obs_data_item_set_default_obj(obs_data_item_t **itemobs_data_t *val);
		//EXPORT void obs_data_item_set_default_array(obs_data_item_t **itemobs_data_array_t *val);

		//EXPORT void obs_data_item_set_autoselect_string(obs_data_item_t **itemconst char *val);
		//EXPORT void obs_data_item_set_autoselect_int(obs_data_item_t **itemlong long val);
		//EXPORT void obs_data_item_set_autoselect_double(obs_data_item_t **itemdouble val);
		//EXPORT void obs_data_item_set_autoselect_bool(obs_data_item_t **item, bool val);
		//EXPORT void obs_data_item_set_autoselect_obj(obs_data_item_t **itemobs_data_t *val);
		//EXPORT void obs_data_item_set_autoselect_array(obs_data_item_t **itemobs_data_array_t *val);

		//EXPORT const char *obs_data_item_get_string(obs_data_item_t *item);
		//EXPORT long long obs_data_item_get_int(obs_data_item_t *item);
		//EXPORT double obs_data_item_get_double(obs_data_item_t *item);
		//EXPORT bool obs_data_item_get_bool(obs_data_item_t *item);
		//EXPORT obs_data_t *obs_data_item_get_obj(obs_data_item_t *item);
		//EXPORT obs_data_array_t *obs_data_item_get_array(obs_data_item_t *item);

		//EXPORT const char *obs_data_item_get_default_string(obs_data_item_t *item);
		//EXPORT long long obs_data_item_get_default_int(obs_data_item_t *item);
		//EXPORT double obs_data_item_get_default_double(obs_data_item_t *item);
		//EXPORT bool obs_data_item_get_default_bool(obs_data_item_t *item);
		//EXPORT obs_data_t *obs_data_item_get_default_obj(obs_data_item_t *item);
		//EXPORT obs_data_array_t *obs_data_item_get_default_array(obs_data_item_t *item);

		//EXPORT const char *obs_data_item_get_autoselect_string(obs_data_item_t *item);
		//EXPORT long long obs_data_item_get_autoselect_int(obs_data_item_t *item);
		//EXPORT double obs_data_item_get_autoselect_double(obs_data_item_t *item);
		//EXPORT bool obs_data_item_get_autoselect_bool(obs_data_item_t *item);
		//EXPORT obs_data_t *obs_data_item_get_autoselect_obj(obs_data_item_t *item);
		//EXPORT obs_data_array_t *obs_data_item_get_autoselect_array(obs_data_item_t *item);

		//EXPORT void obs_data_set_vec2(obs_data_t *data, const char *nameconst struct vec2 *val);
		//EXPORT void obs_data_set_vec3(obs_data_t *data, const char *nameconst struct vec3 *val);
		//EXPORT void obs_data_set_vec4(obs_data_t *data, const char *nameconst struct vec4 *val);
		//EXPORT void obs_data_set_quat(obs_data_t *data, const char *nameconst struct quat *val);

		//EXPORT void obs_data_set_default_vec2(obs_data_t *data, const char *nameconst struct vec2 *val);
		//EXPORT void obs_data_set_default_vec3(obs_data_t *data, const char *nameconst struct vec3 *val);
		//EXPORT void obs_data_set_default_vec4(obs_data_t *data, const char *nameconst struct vec4 *val);
		//EXPORT void obs_data_set_default_quat(obs_data_t *data, const char *nameconst struct quat *val);

		//EXPORT void obs_data_set_autoselect_vec2(obs_data_t *data, const char *nameconst struct vec2 *val);
		//EXPORT void obs_data_set_autoselect_vec3(obs_data_t *data, const char *nameconst struct vec3 *val);
		//EXPORT void obs_data_set_autoselect_vec4(obs_data_t *data, const char *nameconst struct vec4 *val);
		//EXPORT void obs_data_set_autoselect_quat(obs_data_t *data, const char *nameconst struct quat *val);

		//EXPORT void obs_data_get_vec2(obs_data_t *data, const char *namestruct vec2 *val);
		//EXPORT void obs_data_get_vec3(obs_data_t *data, const char *namestruct vec3 *val);
		//EXPORT void obs_data_get_vec4(obs_data_t *data, const char *namestruct vec4 *val);
		//EXPORT void obs_data_get_quat(obs_data_t *data, const char *namestruct quat *val);

		//EXPORT void obs_data_get_default_vec2(obs_data_t *data, const char *namestruct vec2 *val);
		//EXPORT void obs_data_get_default_vec3(obs_data_t *data, const char *namestruct vec3 *val);
		//EXPORT void obs_data_get_default_vec4(obs_data_t *data, const char *namestruct vec4 *val);
		//EXPORT void obs_data_get_default_quat(obs_data_t *data, const char *namestruct quat *val);

		//EXPORT void obs_data_get_autoselect_vec2(obs_data_t *data, const char *namestruct vec2 *val);
		//EXPORT void obs_data_get_autoselect_vec3(obs_data_t *data, const char *namestruct vec3 *val);
		//EXPORT void obs_data_get_autoselect_vec4(obs_data_t *data, const char *namestruct vec4 *val);
		//EXPORT void obs_data_get_autoselect_quat(obs_data_t *data, const char *namestruct quat *val);

		public enum obs_data_type : int
		{
			OBS_DATA_NULL,
			OBS_DATA_STRING,
			OBS_DATA_NUMBER,
			OBS_DATA_BOOLEAN,
			OBS_DATA_OBJECT,
			OBS_DATA_ARRAY
		};
	}
}