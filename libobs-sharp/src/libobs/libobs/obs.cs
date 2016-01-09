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
	using profiler_name_store_t = IntPtr;

	using size_t = UIntPtr;
	using uint32_t = UInt32;

	public static partial class libobs
	{
		/* ------------------------------------------------------------------------- */
		/* OBS context */

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_startup(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string locale,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string module_config_path,
			profiler_name_store_t store);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_shutdown();

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_initialized();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern uint32_t obs_get_version();

		//EXPORT void obs_set_locale(const char *locale);
		//EXPORT const char *obs_get_locale(void);
		//EXPORT profiler_name_store_t *obs_get_profiler_name_store(void);

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
		//EXPORT char *obs_module_get_config_path(obs_module_t *module, const char *file);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_enum_input_types(size_t idx,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] out string id);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_enum_filter_types(size_t idx,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] out string id);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_enum_transition_types(size_t idx,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] out string id);
		
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
		public static extern void obs_set_output_source(uint32_t channel, obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_get_output_source(uint32_t channel);

		//EXPORT void obs_enum_sources(bool (*enum_proc)(void*, obs_source_t*), void *param);
		//EXPORT void obs_enum_outputs(bool (*enum_proc)(void*, obs_output_t*), void *param);
		//EXPORT void obs_enum_encoders(bool (*enum_proc)(void*, obs_encoder_t*), void *param);
		//EXPORT void obs_enum_services(bool (*enum_proc)(void*, obs_service_t*), void *param);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_source_t obs_get_source_by_name(
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		//EXPORT obs_output_t *obs_get_output_by_name(const char *name);
		//EXPORT obs_encoder_t *obs_get_encoder_by_name(const char *name);
		//EXPORT obs_service_t *obs_get_service_by_name(const char *name);

		[Obsolete("Deprecated")]
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_default_rect_effect();
		
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_effect_t obs_get_base_effect(obs_base_effect effect);
		
		//EXPORT signal_handler_t *obs_get_signal_handler(void);
		//EXPORT proc_handler_t *obs_get_proc_handler(void);

		[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
		public delegate void draw_callback(IntPtr param, uint32_t cx, uint32_t cy);

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

		[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.I1)]
		public delegate bool obs_save_source_filter_cb(IntPtr data, obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_array_t obs_save_sources_filtered(obs_save_source_filter_cb cb, IntPtr data);

		[StructLayoutAttribute(LayoutKind.Sequential, CharSet = importCharSet)]
		public struct obs_video_info
		{
			public string graphics_module; //Marshal.PtrToStringAnsi

			public uint32_t fps_num;       //Output FPS numerator
			public uint32_t fps_den;       //Output FPS denominator

			public uint32_t base_width;    //Base compositing width
			public uint32_t base_height;   //Base compositing height

			public uint32_t output_width;  //Output width
			public uint32_t output_height; //Output height
			public video_format output_format; // Output format

			//Video adapter index to use (NOTE: avoid for optimus laptops)
			public uint32_t adapter;

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
		
		public enum obs_base_effect : int
		{
			OBS_EFFECT_DEFAULT,            /**< RGB/YUV */
			OBS_EFFECT_DEFAULT_RECT,       /**< RGB/YUV (using texture_rect) */
			OBS_EFFECT_OPAQUE,             /**< RGB/YUV (alpha set to 1.0) */
			OBS_EFFECT_SOLID,              /**< RGB/YUV (solid color only) */
			OBS_EFFECT_BICUBIC,            /**< Bicubic downscale */
			OBS_EFFECT_LANCZOS,            /**< Lanczos downscale */
			OBS_EFFECT_BILINEAR_LOWRES,    /**< Bilinear low resolution downscale */
		};
	}
}