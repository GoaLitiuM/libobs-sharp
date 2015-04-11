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
	using gs_effect_t = IntPtr;
	using obs_data_array_t = IntPtr;
	using obs_data_t = IntPtr;
	using obs_source_t = IntPtr;

	using size_t = IntPtr;	//UIntPtr?
	using uint32_t = UInt32;

	public static partial class libobs
	{
		/* ------------------------------------------------------------------------- */
		/* OBS context */

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_startup(string locale);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_shutdown();

		//EXPORT bool obs_initialized(void);
		//EXPORT uint32_t obs_get_version(void);
		//EXPORT void obs_set_locale(const char *locale);
		//EXPORT const char *obs_get_locale(void);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern int obs_reset_video(ref obs_video_info ovi);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_reset_audio(ref obs_audio_info ai);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_get_video_info(ref obs_video_info ovi);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_get_audio_info(ref obs_audio_info ai);

		//EXPORT int obs_open_module(obs_module_t **module, const char *path, const char *data_path);
		//EXPORT bool obs_init_module(obs_module_t *module);
		//EXPORT const char *obs_get_module_file_name(obs_module_t *module);
		//EXPORT const char *obs_get_module_name(obs_module_t *module);
		//EXPORT const char *obs_get_module_author(obs_module_t *module);
		//EXPORT const char *obs_get_module_description(obs_module_t *module);
		//EXPORT const char *obs_get_module_binary_path(obs_module_t *module);
		//EXPORT const char *obs_get_module_data_path(obs_module_t *module);
		//EXPORT void obs_add_module_path(const char *bin, const char *data);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_load_all_modules();

		//EXPORT void obs_find_modules(obs_find_module_callback_t callback, void *param);
		//EXPORT void obs_enum_modules(obs_enum_module_callback_t callback, void *param);
		//EXPORT lookup_t *obs_module_load_locale(obs_module_t *module, const char *default_locale, const char *locale);
		//EXPORT char *obs_find_module_file(obs_module_t *module, const char *file);

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

		//EXPORT bool obs_enum_output_types(size_t idx, const char **id);
		//EXPORT bool obs_enum_encoder_types(size_t idx, const char **id);
		//EXPORT bool obs_enum_service_types(size_t idx, const char **id);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_enter_graphics();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_leave_graphics();

		//EXPORT audio_t *obs_get_audio(void);
		//EXPORT video_t *obs_get_video(void);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_add_source(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_set_output_source(uint32_t channel, obs_source_t source);

		//EXPORT obs_source_t *obs_get_output_source(uint32_t channel);
		//EXPORT void obs_enum_sources(bool (*enum_proc)(void*, obs_source_t*), void *param);
		//EXPORT void obs_enum_outputs(bool (*enum_proc)(void*, obs_output_t*), void *param);
		//EXPORT void obs_enum_encoders(bool (*enum_proc)(void*, obs_encoder_t*), void *param);
		//EXPORT void obs_enum_services(bool (*enum_proc)(void*, obs_service_t*), void *param);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_source_t obs_get_source_by_name(string name);

		//EXPORT obs_output_t *obs_get_output_by_name(const char *name);
		//EXPORT obs_encoder_t *obs_get_encoder_by_name(const char *name);
		//EXPORT obs_service_t *obs_get_service_by_name(const char *name);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_default_effect();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_default_rect_effect();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_opaque_effect();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_solid_effect();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_bicubic_effect();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_lanczos_effect();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_bilinear_lowres_effect();

		//EXPORT signal_handler_t *obs_get_signal_handler(void);
		//EXPORT proc_handler_t *obs_get_proc_handler(void);

		[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
		public delegate void draw_callback(IntPtr param, uint32_t cx, uint32_t cy);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_add_draw_callback([MarshalAs(UnmanagedType.FunctionPtr)] draw_callback draw, IntPtr param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_remove_draw_callback([MarshalAs(UnmanagedType.FunctionPtr)] draw_callback draw, IntPtr param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_resize(uint32_t cx, uint32_t cy);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_render_main_view();

		//EXPORT void obs_set_master_volume(float volume);
		//EXPORT void obs_set_present_volume(float volume);
		//EXPORT float obs_get_master_volume(void);
		//EXPORT float obs_get_present_volume(void);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_t obs_save_source(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_load_source(obs_data_t data);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_load_sources(obs_data_array_t array);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_array_t obs_save_sources();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_preview_set_enabled([MarshalAs(UnmanagedType.I1)] bool enable);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_preview_enabled();

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public struct obs_video_info
		{
			public string graphics_module; //Marshal.PtrToStringAnsi

			public uint32_t fps_num;       //Output FPS numerator
			public uint32_t fps_den;       //Output FPS denominator

			public uint32_t window_width;  //Window width
			public uint32_t window_height; //Window height

			public uint32_t base_width;    //Base compositing width
			public uint32_t base_height;   //Base compositing height

			public uint32_t output_width;  //Output width
			public uint32_t output_height; //Output height
			public video_format output_format; // Output format

			//Video adapter index to use (NOTE: avoid for optimus laptops)
			public uint32_t adapter;

			public gs_window window;        //Window to render to

			//Use shaders to convert to different color formats

			[MarshalAs(UnmanagedType.I1)]
			public bool gpu_conversion;

			public video_colorspace colorspace;  //YUV type (if YUV)
			public video_range_type range;       //YUV range (if YUV)

			public obs_scale_type scale_type;    //How to scale if scaling
		};

		public enum obs_scale_type : int
		{
			OBS_SCALE_BICUBIC,
			OBS_SCALE_BILINEAR,
			OBS_SCALE_LANCZOS,
		};
	}
}