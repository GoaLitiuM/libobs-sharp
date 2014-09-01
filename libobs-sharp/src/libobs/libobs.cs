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
using System.Runtime.InteropServices;

namespace OBS
{
	using audio_line_t = IntPtr;
	using audio_resampler_t = IntPtr;
	using gs_effect_t = IntPtr;
	using gs_texrender_t = IntPtr;
	using gs_texture_t = IntPtr;
	using obs_audio_data_t = IntPtr;
	using obs_data_item_t = IntPtr;
	using obs_data_t = IntPtr;
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
	using size_t = IntPtr;
	using uint32_t = UInt32;
	using uint64_t = UInt64;
	using uint8_t = Byte;

	public static partial class libobs
	{
		public const string importLibrary = "obs";
		public const CallingConvention importCall = CallingConvention.Cdecl;
		public const CharSet importCharSet = CharSet.Ansi;

		//marshalling type changes: [c/c++ = c#]
		//bool = [MarshalAs(UnmanagedType.I1)] bool
		//size_t = IntPtr
		//long = int (32-bit env)
		//enums = int type

		/*
		 * startup
		 */

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
	

		/*
		 * rendering
		 */

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_render_main_view();

		[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
		public delegate void draw_callback(IntPtr param, uint32_t cx, uint32_t cy);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_add_draw_callback([MarshalAs(UnmanagedType.FunctionPtr)] draw_callback draw, IntPtr param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_resize(uint32_t cx, uint32_t cy);

		/*
		 * source
		 */

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


		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_filter_add(obs_source_t source, obs_source_t filter);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_source_t obs_get_source_by_name(string name);

		

		

		/*
		 * scene
		 */

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


		/*
		 * scene_item
		 */

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_addref(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_release(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_remove(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_sceneitem_get_source(obs_sceneitem_t scene);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void  obs_sceneitem_get_pos(obs_sceneitem_t item, out vec2 pos);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern float obs_sceneitem_get_rot(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_get_scale(obs_sceneitem_t item, out vec2 scale);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern uint32_t obs_sceneitem_get_alignment(obs_sceneitem_t item);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_set_pos(obs_sceneitem_t item, out vec2 pos);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_set_rot(obs_sceneitem_t item, float rot_deg);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_set_scale(obs_sceneitem_t item, out vec2 scale);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_sceneitem_set_alignment(obs_sceneitem_t item, uint32_t alignment);

		/*
		 * properties
		 */

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_properties_t obs_get_source_properties(obs_source_type type, string id);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_properties_t obs_source_properties(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_property_t obs_properties_first(obs_properties_t props);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_property_next(out obs_property_t p);


		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern string obs_property_name(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern string obs_property_description(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_property_type obs_property_get_type(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_property_enabled(obs_property_t p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_property_visible(obs_property_t p);

		/*
		 * misc/uncategorized
		 */

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern IntPtr obs_data_create();

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

		/*
		 * structures
		 */

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
		public class obs_data
		{
			public int refs;
			public IntPtr json;
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

			/*
			 * audio/video timestamp synchronization reference counter
			 *
			 * if audio goes outside of expected timing bounds, this number will
			 * be deremented.
			 *
			 * if video goes outside of expecting timing bounds, this number will
			 * be incremented.
			 *
			 * when this reference counter is at 0, it means ths audio is
			 * synchronized with the video and it is safe to play.  when it's not
			 * 0, it means that audio and video are desynchronized, and thus not
			 * safe to play.  this just generally ensures synchronization between
			 * audio/video when timing somehow becomes 'reset'.
			 *
			 * XXX: may be an overly cautious check
			 */
			public int av_sync_ref;

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

		[StructLayout(LayoutKind.Explicit, Size = 16)]
		public struct vec4
		{
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
		public struct matrix4
		{
			public vec4 x, y, z, t;
		};

		/*
		 * enums
		 */

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
	}
}