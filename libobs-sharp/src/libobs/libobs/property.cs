/***************************************************************************
	Copyright (C) 2014 by Ari Vuollet <ari.vuollet@kapsi.fi>
	
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
using System.Runtime.InteropServices;

namespace OBS
{
    using obs_properties_t = IntPtr;
    using obs_property_t = IntPtr;
    using obs_property_modified_t = IntPtr;

    using int64_t = Int64;
    using size_t = IntPtr;	//UIntPtr?

    public static partial class libobs
    {
        /* ------------------------------------------------------------------------- */

        //EXPORT obs_properties_t *obs_properties_create(void);
        //EXPORT obs_properties_t *obs_properties_create_param(void *paramvoid (*destroy)(void *param));

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void obs_properties_destroy(obs_properties_t props);

        //EXPORT void obs_properties_set_param(obs_properties_t *propsvoid *param, void (*destroy)(void *param));
        //EXPORT void *obs_properties_get_param(obs_properties_t *props);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern obs_property_t obs_properties_first(obs_properties_t props);

        //EXPORT obs_property_t *obs_properties_get(obs_properties_t *propsconst char *property);

        /* ------------------------------------------------------------------------- */

        //EXPORT obs_property_t *obs_properties_add_bool(obs_properties_t *propsconst char *name, const char *description);
        //EXPORT obs_property_t *obs_properties_add_int(obs_properties_t *propsconst char *name, const char *descriptionint min, int max, int step);
        //EXPORT obs_property_t *obs_properties_add_float(obs_properties_t *propsconst char *name, const char *descriptiondouble min, double max, double step);
        //EXPORT obs_property_t *obs_properties_add_text(obs_properties_t *propsconst char *name, const char *descriptionenum obs_text_type type);
        //EXPORT obs_property_t *obs_properties_add_path(obs_properties_t *propsconst char *name, const char *descriptionenum obs_path_type type, const char *filterconst char *default_path);
        //EXPORT obs_property_t *obs_properties_add_list(obs_properties_t *propsconst char *name, const char *descriptionenum obs_combo_type type, enum obs_combo_format format);
        //EXPORT obs_property_t *obs_properties_add_color(obs_properties_t *propsconst char *name, const char *description);
        //EXPORT obs_property_t *obs_properties_add_button(obs_properties_t *propsconst char *name, const char *textobs_property_clicked_t callback);
        //EXPORT obs_property_t *obs_properties_add_font(obs_properties_t *propsconst char *name, const char *description);

        /* ------------------------------------------------------------------------- */

        //EXPORT void obs_property_set_modified_callback(obs_property_t *pobs_property_modified_t modified);
        //EXPORT bool obs_property_modified(obs_property_t *p, obs_data_t *settings);
        //EXPORT bool obs_property_button_clicked(obs_property_t *p, void *obj);
        //EXPORT void obs_property_set_visible(obs_property_t *p, bool visible);
        //EXPORT void obs_property_set_enabled(obs_property_t *p, bool enabled);
        //EXPORT void obs_property_set_description(obs_property_t *p, const char *description);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_property_name")]
        private static extern IntPtr import_obs_property_name(obs_property_t p);

        public static string obs_property_name(obs_property_t p)
        {
            IntPtr strPtr = import_obs_property_name(p);
            return Marshal.PtrToStringAnsi(strPtr);
        }

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_property_description")]
        private static extern IntPtr import_obs_property_description(obs_property_t p);

        public static string obs_property_description(obs_property_t p)
        {
            IntPtr strPtr = import_obs_property_description(p);
            return Marshal.PtrToStringAnsi(strPtr);
        }

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
        public static extern double obs_property_float_min(obs_property_t p);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern double obs_property_float_max(obs_property_t p);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern double obs_property_float_step(obs_property_t p);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern obs_text_type obs_proprety_text_type(obs_property_t p);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern obs_path_type obs_property_path_type(obs_property_t p);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_property_path_filter")]
        private static extern IntPtr import_obs_property_path_filter(obs_property_t p);

        public static string obs_property_path_filter(obs_property_t p)
        {
            IntPtr strPtr = import_obs_property_path_filter(p);
            return Marshal.PtrToStringAnsi(strPtr);
        }

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_property_path_default_path")]
        private static extern IntPtr import_obs_property_path_default_path(obs_property_t p);

        public static string obs_property_path_default_path(obs_property_t p)
        {
            IntPtr strPtr = import_obs_property_path_default_path(p);
            return Marshal.PtrToStringAnsi(strPtr);
        }

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

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_property_list_item_name")]
        private static extern IntPtr import_obs_property_list_item_name(obs_property_t p, size_t idx);

        public static string obs_property_list_item_name(obs_property_t p, size_t idx)
        {
            IntPtr strPtr = import_obs_property_list_item_name(p, idx);
            return MarshalUTF8String(strPtr);
        }

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_property_list_item_string")]
        private static extern IntPtr import_obs_property_list_item_string(obs_property_t p, size_t idx);

        public static string obs_property_list_item_string(obs_property_t p, size_t idx)
        {
            IntPtr strPtr = import_obs_property_list_item_string(p, idx);
            return MarshalUTF8String(strPtr);
        }

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern int64_t obs_property_list_item_int(obs_property_t p, size_t idx);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern double obs_property_list_item_float(obs_property_t p, size_t idx);


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
    }
}