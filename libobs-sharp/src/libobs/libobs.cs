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
	#region Opaque Types

	using audio_line_t = IntPtr;
	using audio_resampler_t = IntPtr;
	using gs_effect_t = IntPtr;
	using gs_eparam_t = IntPtr;
	using gs_indexbuffer_t = IntPtr;
	using gs_init_data_t = IntPtr;
	using gs_rect_t = IntPtr;
	using gs_samplerstate_t = IntPtr;
	using gs_shader_t = IntPtr;
	using gs_swapchain_t = IntPtr;
	using gs_technique_t = IntPtr;
	using gs_texrender_t = IntPtr;
	using gs_texture_t = IntPtr;
	using gs_vertbuffer_t = IntPtr;
	using obs_audio_data_t = IntPtr;
    using obs_data_array_t = IntPtr;
	using obs_data_item_t = IntPtr;
	using obs_data_t = IntPtr;
	using obs_display_t = IntPtr;
	using obs_properties_t = IntPtr;
	using obs_property_t = IntPtr;
	using obs_property_modified_t = IntPtr;
	using obs_sceneitem_t = IntPtr;
	using obs_scene_t = IntPtr;
	using obs_source_enum_proc_t = IntPtr;
	using obs_source_frame_t = IntPtr;
	using obs_source_t = IntPtr;
	using proc_handler_t = IntPtr;
	using pthread_mutex_t = IntPtr;
	using signal_handler_t = IntPtr;

	using int64_t = Int64;
	using size_t = IntPtr;	//UIntPtr?
	using uint32_t = UInt32;
	using uint64_t = UInt64;
	using uint8_t = Byte;

	using axisang = libobs.vec4;
	using quat = libobs.vec4;

	#endregion

	public static partial class libobs
	{
		public const string importLibrary = "obs";	//extension is handled automatically
		public const CallingConvention importCall = CallingConvention.Cdecl;
		public const CharSet importCharSet = CharSet.Ansi;

		//marshalling type changes: [c/c++ = c#]
		//bool = [MarshalAs(UnmanagedType.I1)] bool
		//size_t = IntPtr
		//long = int (32-bit env)
		//enums = int type

		#region Startup
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_startup(string locale);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_shutdown();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_load_all_modules();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern int obs_reset_video(ref obs_video_info ovi);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_reset_audio(ref audio_output_info ai);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_get_video_info(ref obs_video_info ovi);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_get_audio_info(ref audio_output_info ai);
		#endregion

		#region Rendering
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_render_main_view();

		[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
		public delegate void draw_callback(IntPtr param, uint32_t cx, uint32_t cy);


		/** Adds a draw callback to the main render context */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_add_draw_callback([MarshalAs(UnmanagedType.FunctionPtr)] draw_callback draw, IntPtr param);

		/** Removes a draw callback to the main render context */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_remove_draw_callback([MarshalAs(UnmanagedType.FunctionPtr)] draw_callback draw, IntPtr param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_resize(uint32_t cx, uint32_t cy);


		/** Returns the default effect for generic RGB/YUV drawing */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_default_effect();

		/** Returns the solid effect for drawing solid colors */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_solid_effect();


		/** Adds a new window display linked to the main render pipeline.
		  * This creates a new swap chain which updates every frame. */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_display_t obs_display_create(ref gs_init_data graphics_data);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_display_destroy(obs_display_t display);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_display_resize(obs_display_t display, uint32_t cx, uint32_t cy);

		/** Adds a draw callback for this display context */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_display_add_draw_callback(obs_display_t display, draw_callback draw, IntPtr param);

		/** Removes a draw callback for this display context */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_display_remove_draw_callback(obs_display_t display, draw_callback draw, IntPtr param);

		#endregion

		#region Source
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_source_t obs_source_create(obs_source_type type, string id, string name, obs_data_t settings);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_addref(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_release(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_add_source(obs_source source);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_source_get_name")]
		private static extern IntPtr import_obs_source_get_name(obs_source_t source);

		public static string obs_source_get_name(obs_source_t source)
		{
			IntPtr strPtr = import_obs_source_get_name(source);
			return Marshal.PtrToStringAnsi(strPtr);
		}

        /* Gets the settings string for a source */
        [DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_t obs_source_get_settings(obs_source_t source);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_filter_add(obs_source_t source, obs_source_t filter);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_source_t obs_get_source_by_name(string name);


        /** Updates settings for this source */
        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void obs_source_update(obs_source_t source, obs_data_t settings);

		/** Renders a video source. */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_video_render(obs_source_t source);

		/** Gets the width of a source (if it has video) */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern uint32_t obs_source_get_width(obs_source_t source);

		/** Gets the height of a source (if it has video) */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern uint32_t obs_source_get_height(obs_source_t source);


        /** Saves a source to settings data */
        [DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_t obs_save_source(obs_source_t source);

        /** Loads a source from settings data */
        [DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_load_source(obs_data_t data);

        /** Loads sources from a data array */
        [DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_load_sources(obs_data_array_t array);

        /** Saves sources to a data array */
        [DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_array_t obs_save_sources();

		#endregion

		#region Scene
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_scene_t obs_scene_create(string name);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_scene_addref(obs_scene_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_scene_release(obs_scene_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_sceneitem_t obs_scene_add(obs_scene_t scene, obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_scene_get_source(obs_scene_t scene);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_scene_t obs_scene_from_source(obs_source_t source);

		/** Determines whether a source is within a scene */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_sceneitem_t obs_scene_find_source(obs_scene_t scene, string name);

		[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
		public delegate bool sceneitem_enum_callback(obs_scene_t scene, obs_sceneitem_t sceneItem, IntPtr data);

		/** Enumerates sources within a scene */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_scene_enum_items(obs_scene_t scene, sceneitem_enum_callback callback, IntPtr param);
		#endregion

		#region Scene Item
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_addref(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_release(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_remove(obs_sceneitem_t item);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_sceneitem_get_source(obs_sceneitem_t scene);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_select(obs_sceneitem_t item, [MarshalAs(UnmanagedType.I1)] bool select);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_sceneitem_selected(obs_sceneitem_t item);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_get_pos(obs_sceneitem_t item, out vec2 pos);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern float obs_sceneitem_get_rot(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_get_scale(obs_sceneitem_t item, out vec2 scale);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern uint32_t obs_sceneitem_get_alignment(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_get_draw_transform(obs_sceneitem_t item, out matrix4 transform);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_get_box_transform(obs_sceneitem_t item, out matrix4 transform);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_set_pos(obs_sceneitem_t item, out vec2 pos);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_set_rot(obs_sceneitem_t item, float rot_deg);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_set_scale(obs_sceneitem_t item, out vec2 scale);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_set_alignment(obs_sceneitem_t item, uint32_t alignment);
		#endregion

		#region Properties
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



		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_properties_t obs_get_source_properties(obs_source_type type, string id);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_properties_t obs_source_properties(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_property_t obs_properties_first(obs_properties_t props);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_properties_destroy(obs_properties_t props);
		#endregion

        #region Data

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern obs_data_t obs_data_create();

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern obs_data_t obs_data_create_from_json(string json_string);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void obs_data_addref(obs_data_t data);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void obs_data_release(obs_data_t data);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_data_get_json")]
        private static extern IntPtr import_obs_data_get_json(obs_data_t data);

        public static string obs_data_get_json(obs_data_t data)
        {
            string str = null;
            IntPtr strPtr = import_obs_data_get_json(data);

            if (strPtr != IntPtr.Zero)
                str = Marshal.PtrToStringAnsi(strPtr);

            return str;
        }

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void obs_data_apply(obs_data_t target, obs_data_t apply_data);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_erase(obs_data_t data, string name);


        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_string(obs_data_t data, string name, string val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_int(obs_data_t data, string name, int64_t val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_double(obs_data_t data, string name, double val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_bool(obs_data_t data, string name, [MarshalAs(UnmanagedType.I1)] bool val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_obj(obs_data_t data, string name, obs_data_t obj);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_data_set_array(obs_data_t data, string name, obs_data_array_t array);


        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_default_string(obs_data_t data, string name, string val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_default_int(obs_data_t data, string name, int64_t val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_default_double(obs_data_t data, string name, double val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_default_bool(obs_data_t data, string name, [MarshalAs(UnmanagedType.I1)] bool val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_default_obj(obs_data_t data, string name, obs_data_t obj);


        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_autoselect_string(obs_data_t data, string name, string val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_autoselect_int(obs_data_t data, string name, int64_t val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_autoselect_double(obs_data_t data, string name, double val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_autoselect_bool(obs_data_t data, string name, [MarshalAs(UnmanagedType.I1)] bool val);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern void obs_data_set_autoselect_obj(obs_data_t data, string name, obs_data_t obj);


        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_data_get_string")]
        private static extern IntPtr import_obs_data_get_string(obs_data_t data, string name);

        public static string obs_data_get_string(obs_data_t data, string name)
        {
            string str = null;
            IntPtr strPtr = import_obs_data_get_string(data, name);

            if (strPtr != IntPtr.Zero)
                str = Marshal.PtrToStringAnsi(strPtr);

            return str;
        }

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern int64_t obs_data_get_int(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern double obs_data_get_double(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_get_bool(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern obs_data_t obs_data_get_obj(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern obs_data_array_t obs_data_get_array(obs_data_t data, string name);


        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_data_get_default_string")]
        private static extern IntPtr import_obs_data_get_default_string(obs_data_t data, string name);

        public static string obs_data_get_default_string(obs_data_t data, string name)
        {
            string str = null;
            IntPtr strPtr = import_obs_data_get_default_string(data, name);

            if (strPtr != IntPtr.Zero)
                str = Marshal.PtrToStringAnsi(strPtr);

            return str;
        }

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern int64_t obs_data_get_default_int(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern double obs_data_get_default_double(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_get_default_bool(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern obs_data_t obs_data_get_default_obj(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern obs_data_array_t obs_data_get_default_array(obs_data_t data, string name);


        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_data_get_autoselect_string")]
        private static extern IntPtr import_obs_data_get_autoselect_string(obs_data_t data, string name);

        public static string obs_data_get_autoselect_string(obs_data_t data, string name)
        {
            string str = null;
            IntPtr strPtr = import_obs_data_get_autoselect_string(data, name);

            if (strPtr != IntPtr.Zero)
                str = Marshal.PtrToStringAnsi(strPtr);

            return str;
        }

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern int64_t obs_data_get_autoselect_int(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern double obs_data_get_autoselect_double(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_get_autoselect_bool(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern obs_data_t obs_data_get_autoselect_obj(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        public static extern obs_data_array_t obs_data_get_autoselect_array(obs_data_t data, string name);


        //Item status inspection

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_has_user_value(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_has_default_value(obs_data_t data, string name);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_has_autoselect_value(obs_data_t data, string name);


        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_item_has_user_value(obs_data_item_t data);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_item_has_default_value(obs_data_item_t data);

        [DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool obs_data_item_has_autoselect_value(obs_data_item_t data); 

        #endregion

        #region Misc/Uncategorized

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_set_output_source(uint32_t channel, obs_source_t source);


		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_source_get_display_name")]
		private static extern IntPtr import_obs_source_get_display_name(obs_source_type type, string id);

		public static string obs_source_get_display_name(obs_source_type type, string id)
		{
			string str = null;
			IntPtr strPtr = import_obs_source_get_display_name(type, id);

			if (strPtr != IntPtr.Zero)
				str = Marshal.PtrToStringAnsi(strPtr);

			return str;
		}


		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_enum_input_types")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool import_obs_enum_input_types(size_t idx, out IntPtr id);

		public static bool obs_enum_input_types(int idx, out string id)
		{
			IntPtr strPtr = IntPtr.Zero;
			bool ret = import_obs_enum_input_types((size_t)idx, out strPtr);

			if (strPtr != IntPtr.Zero)
				id = Marshal.PtrToStringAnsi(strPtr);
			else
				id = null;

			return ret;
		}

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_enum_filter_types")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool import_obs_enum_filter_types(size_t idx, out IntPtr id);

		public static bool obs_enum_filter_types(int idx, out string id)
		{
			IntPtr strPtr = IntPtr.Zero;
			bool ret = import_obs_enum_filter_types((size_t)idx, out strPtr);

			if (strPtr != IntPtr.Zero)
				id = Marshal.PtrToStringAnsi(strPtr);
			else
				id = null;

			return ret;
		}

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "obs_enum_transition_types")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool import_obs_enum_transition_types(size_t idx, out IntPtr id);

		public static bool obs_enum_transition_types(int idx, out string id)
		{
			IntPtr strPtr = IntPtr.Zero;
			bool ret = import_obs_enum_transition_types((size_t)idx, out strPtr);

			if (strPtr != IntPtr.Zero)
				id = Marshal.PtrToStringAnsi(strPtr);
			else
				id = null;

			return ret;
		}
		#endregion

		#region Graphics Subsystem
		/** Helper function for entering the OBS graphics context */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_enter_graphics();

		/** Helper function for leaving the OBS graphics context */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_leave_graphics();


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_begin_scene();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_draw(gs_draw_mode draw_mode, uint32_t start_vert, uint32_t num_verts);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_end_scene();


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_clear(uint32_t clear_flags, out vec4 color, float depth, uint8_t stencil);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_viewport_push();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_viewport_pop();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_projection_push();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_projection_pop();


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_ortho(float left, float right, float top, float bottom, float znear, float zfar);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_frustum(float left, float right, float top, float bottom, float znear, float zfar);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_set_viewport(int x, int y, int width, int height);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_get_viewport(out gs_rect rect);

		/** sets the viewport to current swap chain size */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_reset_viewport();

		/** sets default screen-sized orthographich mode */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_set_2d_mode();

		/** sets default screen-sized perspective mode */
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_set_3d_mode(double fovy, double znear, double zvar);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_load_vertexbuffer(gs_vertbuffer_t vertbuffer);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_load_indexbuffer(gs_indexbuffer_t indexbuffer);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_load_texture(gs_texture_t tex, int unit);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_load_samplerstate(gs_samplerstate_t samplerstate, int unit);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_load_vertexshader(gs_shader_t vertshader);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_load_pixelshader(gs_shader_t pixelshader);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_render_start([MarshalAs(UnmanagedType.I1)] bool b_new);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_render_stop(gs_draw_mode mode);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_vertbuffer_t gs_render_save();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_vertex2f(float x, float y);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_vertex3f(float x, float y, float z);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_normal3f(float x, float y, float z);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_color(uint32_t color);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_texcoord(float x, float y, int unit);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_vertex2v(out vec2 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_vertex3v(out vec3 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_normal3v(out vec3 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_color4v(out vec4 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_texcoord2v(out vec2 v, int unit);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_vertbuffer_t gs_vertexbuffer_create(out gs_vb_data data, uint32_t flags);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_vertexbuffer_flush(gs_vertbuffer_t vertbuffer);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_vertexbuffer_destroy(gs_vertbuffer_t vertbuffer);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern size_t gs_effect_get_num_params(gs_effect_t effect);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_eparam_t gs_effect_get_param_by_idx(gs_effect_t effect, size_t param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_eparam_t gs_effect_get_param_by_name(gs_effect_t effect, string name);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern size_t gs_technique_begin(gs_technique_t technique);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_technique_end(gs_technique_t technique);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool gs_technique_begin_pass(gs_technique_t technique, size_t pass);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool gs_technique_begin_pass_by_name(gs_technique_t technique, string name);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_technique_end_pass(gs_technique_t technique);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_technique_t gs_effect_get_technique(gs_effect_t effect, string name);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_bool(gs_eparam_t param, [MarshalAs(UnmanagedType.I1)] bool val);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_float(gs_eparam_t param, float val);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_int(gs_eparam_t param, int val);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_matrix4(gs_eparam_t param, out matrix4 val);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_vec2(gs_eparam_t param, out vec2 val);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_vec3(gs_eparam_t param, out vec3 val);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_vec4(gs_eparam_t param, out vec4 val);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_texture(gs_eparam_t param, gs_texture_t val);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_val(gs_eparam_t param, IntPtr val, size_t size);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_effect_set_default(gs_eparam_t param);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern float vec3_plane_dist(out vec3 v, out plane p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec3_transform(out vec3 dst, out vec3 v, out matrix4 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec3_rotate(out vec3 dst, out vec3 v, out matrix3 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec3_transform3x4(out vec3 dst, out vec3 v, out matrix3 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec3_mirror(out vec3 dst, out vec3 v, out plane p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec3_mirrorv(out vec3 dst, out vec3 v, out vec3 vec);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec3_rand(out vec3 dst, int positive_only);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_from_quat(out matrix3 dst, out quat q);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_from_axisang(out matrix3 dst, out axisang aa);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_from_matrix4(out matrix3 dst, out matrix4 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_mul(out matrix3 dst, out matrix3 m1, out matrix3 m2);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_rotate(out matrix3 dst, out matrix3 m, out quat q);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_rotate_aa(out matrix3 dst, out matrix3 m, out axisang aa);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_scale(out matrix3 dst, out matrix3 m, out vec3 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_transpose(out matrix3 dst, out matrix3 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_inv(out matrix3 dst, out matrix3 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_mirror(out matrix3 dst, out matrix3 m, out plane p);
		
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_mirrorv(out matrix3 dst, out matrix3 m, out vec3 v);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern float matrix4_determinant(out matrix4 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix4_translate3v(out matrix4 dst, out matrix4 m, out vec3 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix4_translate4v(out matrix4 dst, out matrix4 m, out vec4 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix4_rotate(out matrix4 dst, out matrix4 m, out quat q);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix4_rotate_aa(out matrix4 dst, out matrix4 m, out axisang aa);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix4_scale(out matrix4 dst, out matrix4 m, out vec3 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern bool matrix4_inv(out matrix4 dst, out matrix4 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix4_transpose(out matrix4 dst, out matrix4 m);


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_push();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_pop();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_identity();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_transpose();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_set(out matrix4 matrix);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_get(out matrix4 dst);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_mul(out matrix4 matrix);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_rotquat(out quat rot);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_rotaa(out axisang rot);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_translate(out vec3 pos);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_scale(out vec3 scale);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_rotaa4f(float x, float y, float z, float angle);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_translate3f(float x, float y, float z);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_matrix_scale3f(float x, float y, float z);
		#endregion

		#region Structures
		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public struct obs_video_info
		{
			public string graphics_module; //Marshal.PtrToStringAnsi

			public uint32_t fps_num;       /**< Output FPS numerator */
			public uint32_t fps_den;       /**< Output FPS denominator */

			public uint32_t window_width;  /**< Window width */
			public uint32_t window_height; /**< Window height */

			public uint32_t base_width;    /**< Base compositing width */
			public uint32_t base_height;   /**< Base compositing height */

			public uint32_t output_width;  /**< Output width */
			public uint32_t output_height; /**< Output height */
			public video_format output_format; /**< Output format */

			/** Video adapter index to use (NOTE: avoid for optimus laptops) */
			public uint32_t adapter;

			public gs_window window;        /**< Window to render to */

			/** Use shaders to convert to different color formats */

			[MarshalAs(UnmanagedType.I1)]
			public bool gpu_conversion;
		};



		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public unsafe struct obs_data
		{
			public int refs;
			public char* json;
			public obs_data_item_t first_item;
		};

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public struct obs_data_item
		{
			public int refs;
			public obs_data_t parent;
			public obs_data_item_t next;
			public obs_data_type type;
			public size_t name_len;
			public size_t data_len;
			public size_t data_size;
			public size_t default_len;
			public size_t default_size;
			public size_t autoselect_size;
			public size_t capacity;
		};

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public unsafe struct obs_source
		{
			public obs_context_data context;
			public obs_source_info info;
			public int refs;

            /* general exposed flags that can be set for the source */
            public uint32_t flags;

			/* indicates ownership of the info.id buffer */
			[MarshalAs(UnmanagedType.I1)]
			public bool owns_info_id;

			/* signals to call the source update in the video thread */
			[MarshalAs(UnmanagedType.I1)]
			public bool defer_update;

			/* ensures show/hide are only called once */
			public int show_refs;

			/* ensures activate/deactivate are only called once */
			public int activate_refs;

			/* prevents infinite recursion when enumerating sources */
			public int enum_refs;

			/* used to indicate that the source has been removed and all
			 * references to it should be released (not exactly how I would prefer
			 * to handle things but it's the best option) */

			[MarshalAs(UnmanagedType.I1)]
			public bool removed;

			/* timing (if video is present, is based upon video) */
			[MarshalAs(UnmanagedType.I1)]
			public bool timing_set;
			public uint64_t timing_adjust;
			public uint64_t next_audio_ts_min;
			public uint64_t last_frame_ts;
			public uint64_t last_sys_timestamp;

			/* audio */
			[MarshalAs(UnmanagedType.I1)]
			public bool audio_failed;
			public resample_info sample_info;
			public audio_resampler_t resampler;
			public audio_line_t audio_line;
			public pthread_mutex_t audio_mutex;
			public obs_audio_data audio_data;
			public size_t audio_storage_size;
			public float user_volume;
			public float present_volume;
			public int64_t sync_offset;

			/* audio levels*/
			public float vol_mag;
			public float vol_max;
			public float vol_peak;
			public size_t vol_update_count;

			/* transition volume is meant to store the sum of transitioning volumes
			 * of a source, i.e. if a source is within both the "to" and "from"
			 * targets of a transition, it would add both volumes to this variable,
			 * and then when the transition frame is complete, is applies the value
			 * to the presentation volume. */
			public float transition_volume;

			/* async video data */
			public gs_texture_t async_texture;
			public gs_texrender_t async_convert_texrender;

			[MarshalAs(UnmanagedType.I1)]
			public bool async_gpu_conversion;
			public video_format async_format;
			public gs_color_format async_texture_format;
			public fixed float async_color_matrix[16];

			[MarshalAs(UnmanagedType.I1)]
			public bool async_full_range;
			public fixed float async_color_range_min[3];
			public fixed float async_color_range_max[3];
			public fixed int async_plane_offset[2];

			[MarshalAs(UnmanagedType.I1)]
			public bool async_flip;
			public darray /*obs_source_frame**/ video_frames;
			public pthread_mutex_t video_mutex;
			public uint32_t async_width;
			public uint32_t async_height;
			public uint32_t async_convert_width;
			public uint32_t async_convert_height;

			/* filters */
			public obs_source_t filter_parent;
			public obs_source_t filter_target;
			public darray /*obs_source**/ filters;
			public pthread_mutex_t filter_mutex;
			public gs_texrender_t filter_texrender;

			[MarshalAs(UnmanagedType.I1)]
			public bool rendering_filter;
		}

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public unsafe struct obs_source_info
		{
			public char* id;
			public obs_source_type type;
			public uint32_t output_flags;

			public IntPtr get_name;
			public IntPtr create;
			public IntPtr destroy;
			public IntPtr get_width;
			public IntPtr get_height;
			public IntPtr get_defaults;
			public IntPtr get_properties;
			public IntPtr update;
			public IntPtr activate;
			public IntPtr deactivate;
			public IntPtr show;
			public IntPtr hide;
			public IntPtr video_tick;
			public IntPtr video_render;
			public IntPtr filter_video;
			public IntPtr filter_audio;
			public IntPtr enum_sources;
			public IntPtr save;
			public IntPtr load;

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate string delegate_get_name();

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate IntPtr delegate_create(obs_data_t settings, obs_source_t source);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_destroy(IntPtr data);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate uint32_t delegate_get_width(IntPtr data);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate uint32_t delegate_get_height(IntPtr data);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_get_defaults(obs_data_t settings);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate obs_properties_t delegate_get_properties();

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_update(IntPtr data, obs_data_t settings);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_activate(IntPtr data);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_deactivate(IntPtr data);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_show(IntPtr data);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_hide(IntPtr data);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_video_tick(IntPtr data, float seconds);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_video_render(IntPtr data, gs_effect_t effect);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate obs_source_frame_t delegate_filter_video(IntPtr data, obs_source_frame_t frame);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate obs_audio_data_t delegate_filter_audio(IntPtr data, obs_audio_data_t audio);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_enum_sources(IntPtr data, obs_source_enum_proc_t enum_callback, IntPtr param);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_save(IntPtr data, obs_data_t settings);

			[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
			unsafe delegate void delegate_load(IntPtr data, obs_data_t settings);
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct obs_scene
		{
			public obs_source source;
			public pthread_mutex_t mutex;
			public obs_sceneitem_t first_item;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct obs_scene_item
		{
			public int refs;
			public bool removed;

			public obs_scene_t parent;
			public obs_source_t source;

			[MarshalAs(UnmanagedType.I1)]
			public bool visible;

			[MarshalAs(UnmanagedType.I1)]
			public bool selected;

			public vec2 pos;
			public vec2 scale;
			public float rot;
			public uint32_t align;

			/* last width/height of the source, this is used to check whether
			 * ths transform needs updating */
			public uint32_t last_width;
			public uint32_t last_height;

			public matrix4 box_transform;
			public matrix4 draw_transform;

			public obs_bounds_type bounds_type;
			public uint32_t bounds_align;
			public vec2 bounds;

			/* would do **prev_next, but not really great for reordering */
			public obs_sceneitem_t prev;
			public obs_sceneitem_t next;
		};

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public unsafe struct obs_context_data
		{
			public char* name;
			public IntPtr data;
			public obs_data_t settings;
			public signal_handler_t signals;
			public proc_handler_t procs;

			public darray /*char**/ rename_cache;
			public pthread_mutex_t rename_cache_mutex;

			public pthread_mutex_t mutex;
			public obs_context_data* next;
			public obs_context_data* prev_next;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct obs_audio_data
		{
			public fixed uint32_t data[MAX_AV_PLANES];
			public uint32_t frames;
			public uint64_t timestamp;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct obs_property
		{
			public char* name;
			public char* desc;
			public obs_property_type type;

			[MarshalAs(UnmanagedType.I1)]
			public bool visible;

			[MarshalAs(UnmanagedType.I1)]
			public bool enabled;

			public obs_properties_t parent;
			public obs_property_modified_t modified;
			public obs_property_t next;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct obs_properties
		{
			public IntPtr param;
			public IntPtr destroy; //void (*destroy)(void *param);

			public obs_property_t first_property;
			public obs_property_t* last;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct obs_display
		{
			[MarshalAs(UnmanagedType.I1)]
			public bool size_changed;

			public uint32_t cx, cy;
			public gs_swapchain_t swap;
			public pthread_mutex_t draw_callbacks_mutex;
			public darray draw_callbacks; //draw_callback

			public obs_display_t next;
			public obs_display_t* prev_next;
		};

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public unsafe struct darray
		{
			public IntPtr array;
			public size_t num;
			public size_t capacity;
		};

		[StructLayout(LayoutKind.Explicit, Size = 8)]
		public struct vec2
		{
			public vec2(float x, float y)
			{
				this.x = x;
				this.y = y;
			}

			[FieldOffset(0)]
			public float x;

			[FieldOffset(4)]
			public float y;

			[FieldOffset(0)]
			public unsafe fixed float ptr[2];
		};

		[StructLayout(LayoutKind.Explicit, Size = 12)]
		public struct vec3
		{
			public vec3(float x, float y, float z)
			{
				this.x = x;
				this.y = y;
				this.z = z;
			}

			[FieldOffset(0)]
			public float x;

			[FieldOffset(4)]
			public float y;

			[FieldOffset(8)]
			public float z;

			[FieldOffset(0)]
			public unsafe fixed float ptr[3];
		};

		[StructLayout(LayoutKind.Explicit, Size = 16)]
		public struct vec4
		{
			public vec4(float x, float y, float z, float w)
			{
				this.x = x;
				this.y = y;
				this.z = z;
				this.w = w;
			}

			[FieldOffset(0)]
			public float x;

			[FieldOffset(4)]
			public float y;

			[FieldOffset(8)]
			public float z;

			[FieldOffset(12)]
			public float w;

			[FieldOffset(0)]
			public unsafe fixed float ptr[4];
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct matrix3
		{
			public vec3 x;
			public vec3 y;
			public vec3 z;
			public vec3 t;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct matrix4
		{
			public vec4 x, y, z, t;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct gs_rect
		{
			public int x, y, cx, cy;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct plane
		{
			public vec3 dir;
			public float dist;
		};
#endregion
		#region Enums
		public enum obs_source_type : int
		{
			OBS_SOURCE_TYPE_INPUT,
			OBS_SOURCE_TYPE_FILTER,
			OBS_SOURCE_TYPE_TRANSITION,
		};

		public enum obs_data_type : int
		{
			OBS_DATA_NULL,
			OBS_DATA_STRING,
			OBS_DATA_NUMBER,
			OBS_DATA_BOOLEAN,
			OBS_DATA_OBJECT,
			OBS_DATA_ARRAY
		};

		/**
		 * Used with scene items to indicate the type of bounds to use for scene items.
		 * Mostly determines how the image will be scaled within those bounds, or
		 * whether to use bounds at all.
		 */

		public enum obs_bounds_type : int
		{
			OBS_BOUNDS_NONE,            /**< no bounds */
			OBS_BOUNDS_STRETCH,         /**< stretch (ignores base scale) */
			OBS_BOUNDS_SCALE_INNER,     /**< scales to inner rectangle */
			OBS_BOUNDS_SCALE_OUTER,     /**< scales to outer rectangle */
			OBS_BOUNDS_SCALE_TO_WIDTH,  /**< scales to the width  */
			OBS_BOUNDS_SCALE_TO_HEIGHT, /**< scales to the height */
			OBS_BOUNDS_MAX_ONLY,        /**< no scaling, maximum size only */
		};


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
			OBS_COMBO_FORMAT_STRING
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
			OBS_PATH_DIRECTORY
		};

		public enum obs_text_type : int
		{
			OBS_TEXT_DEFAULT,
			OBS_TEXT_PASSWORD,
			OBS_TEXT_MULTILINE,
		};
		#endregion

		#region Helper Functions
		/*
		 * helper functions
		 */

		//must be null-terminated string
		private static string MarshalUTF8String(IntPtr strPtr)
		{
			var bytes = new List<byte>();
			int offset = 0;
			uint8_t chr = 0;

			do
			{
				if ((chr = Marshal.ReadByte(strPtr, offset++)) != 0)
					bytes.Add(chr);
			} while (chr != 0);

			return System.Text.Encoding.UTF8.GetString(bytes.ToArray());
		}
		#endregion
	}
}