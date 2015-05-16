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
	using obs_source_t = IntPtr;

	using uint32_t = UInt32;

	public static partial class libobs
	{
		/* ------------------------------------------------------------------------- */
		/* Sources */

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_source_get_display_name(obs_source_type type,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string id);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_source_t obs_source_create(obs_source_type type,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string id,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name, obs_data_t settings,
			obs_data_t hotkey_data);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_addref(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_release(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_remove(obs_source_t source);

		//EXPORT bool obs_source_removed(const obs_source_t *source);
		//EXPORT uint32_t obs_source_get_output_flags(const obs_source_t *source);
		//EXPORT uint32_t obs_get_source_output_flags(enum obs_source_type type, const char *id);
		
		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_data_t obs_get_source_defaults(obs_source_type type, string id);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern obs_properties_t obs_get_source_properties(obs_source_type type,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string id);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_properties_t obs_source_properties(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_update(obs_source_t source, obs_data_t settings);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_video_render(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern uint32_t obs_source_get_width(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern uint32_t obs_source_get_height(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_filter_get_parent(obs_source_t filter);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_t obs_filter_get_target(obs_source_t filter);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_filter_add(obs_source_t source, obs_source_t filter);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_filter_remove(obs_source_t source, obs_source_t filter);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_filter_set_order(obs_source_t source, obs_source_t filter, obs_order_movement movement);
		
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_data_t obs_source_get_settings(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_source_get_name(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		public static extern void obs_source_set_name(obs_source_t source,
			[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))] string name);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_source_type obs_source_get_type(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8StringMarshaler))]
		public static extern string obs_source_get_id(obs_source_t source);

		//EXPORT signal_handler_t *obs_source_get_signal_handler(const obs_source_t *source);
		//EXPORT proc_handler_t *obs_source_get_proc_handler(const obs_source_t *source);
		//EXPORT void obs_source_set_volume(obs_source_t *source, float volume);
		//EXPORT void obs_source_set_present_volume(obs_source_t *source, float volume);
		//EXPORT float obs_source_get_volume(const obs_source_t *source);
		//EXPORT float obs_source_get_present_volume(const obs_source_t *source);
		//EXPORT void obs_source_set_sync_offset(obs_source_t *source, int64_t offset);
		//EXPORT int64_t obs_source_get_sync_offset(const obs_source_t *source);
		//EXPORT void obs_source_enum_sources(obs_source_t *source, obs_source_enum_proc_t enum_callback, void *param);
		//EXPORT void obs_source_enum_tree(obs_source_t *source, obs_source_enum_proc_t enum_callback, void *param);
		//EXPORT bool obs_source_active(const obs_source_t *source);
		//EXPORT bool obs_source_showing(const obs_source_t *source);
		//EXPORT void obs_source_save(obs_source_t *source);
		//EXPORT void obs_source_load(obs_source_t *source);
		//EXPORT void obs_source_set_flags(obs_source_t *source, uint32_t flags);
		//EXPORT uint32_t obs_source_get_flags(const obs_source_t *source);
		//EXPORT void obs_source_set_audio_mixers(obs_source_t *source, uint32_t mixers);
		//EXPORT uint32_t obs_source_get_audio_mixers(const obs_source_t *source);
		//EXPORT void obs_source_inc_showing(obs_source_t *source);
		//EXPORT void obs_source_dec_showing(obs_source_t *source);
		//EXPORT void obs_source_enum_filters(obs_source_t *source, obs_source_enum_proc_t callback, void *param);
		//EXPORT obs_source_t *obs_source_get_filter_by_name(obs_source_t *source, const char *name);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_source_enabled(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_set_enabled(obs_source_t source, bool enabled);

		[DllImport(importLibrary, CallingConvention = importCall)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool obs_source_muted(obs_source_t source);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_source_set_muted(obs_source_t source, bool muted);

		//EXPORT bool obs_source_push_to_mute_enabled(obs_source_t* source);
		//EXPORT void obs_source_enable_push_to_mute(obs_source_t* source, bool enabled);

		//EXPORT uint64_t obs_source_get_push_to_mute_delay(obs_source_t* source);
		//EXPORT void obs_source_set_push_to_mute_delay(obs_source_t* source, uint64_t delay);

		//EXPORT bool obs_source_push_to_talk_enabled(obs_source_t* source);
		//EXPORT void obs_source_enable_push_to_talk(obs_source_t* source, bool enabled);

		//EXPORT uint64_t obs_source_get_push_to_talk_delay(obs_source_t* source);
		//EXPORT void obs_source_set_push_to_talk_delay(obs_source_t* source, uint64_t delay);

		/* ------------------------------------------------------------------------- */
		/* Functions used by sources */

		//EXPORT void obs_source_draw_set_color_matrix(const struct matrix4 *color_matrix, const struct vec3 *color_range_min, const struct vec3 *color_range_max);
		//EXPORT void obs_source_draw(gs_texture_t *image, int x, int y, const struct matrix4 *color_matrix, const struct vec3 *color_range_min, const struct vec3 *color_range_max);
		//EXPORT void obs_source_output_video(obs_source_t *source, const struct obs_source_frame *frame);
		//EXPORT void obs_source_output_audio(obs_source_t *source, const struct obs_source_audio *audio);
		//EXPORT void obs_source_update_properties(obs_source_t *source);
		//EXPORT struct obs_source_frame *obs_source_get_frame(obs_source_t *source);
		//EXPORT void obs_source_release_frame(obs_source_t *source, struct obs_source_frame *frame);

		//EXPORT void obs_source_process_filter_begin(obs_source_t *filter, enum gs_color_format format, enum obs_allow_direct_render allow_direct);
		//EXPORT void obs_source_process_filter_end(obs_source_t *filter, gs_effect_t *effect, uint32_t width, uint32_t height);
		//EXPORT void obs_source_skip_video_filter(obs_source_t *filter);

		//EXPORT bool obs_source_add_child(obs_source_t *parent, obs_source_t *child);
		//EXPORT void obs_source_remove_child(obs_source_t *parent, obs_source_t *child);
		//EXPORT void obs_source_send_mouse_click(obs_source_t *source, const struct obs_mouse_event *event, int32_t type, bool mouse_up, uint32_t click_count);
		//EXPORT void obs_source_send_mouse_move(obs_source_t *source, const struct obs_mouse_event *event, bool mouse_leave);
		//EXPORT void obs_source_send_mouse_wheel(obs_source_t *source, const struct obs_mouse_event *event, int x_delta, int y_delta);
		//EXPORT void obs_source_send_focus(obs_source_t *source, bool focus);
		//EXPORT void obs_source_send_key_click(obs_source_t *source, const struct obs_key_event *event, bool key_up);
		//EXPORT void obs_source_set_default_flags(obs_source_t *source, uint32_t flags);
		//EXPORT uint32_t obs_source_get_base_width(obs_source_t *source);
		//EXPORT uint32_t obs_source_get_base_height(obs_source_t *source);
		//EXPORT void obs_source_reset_video_cache(obs_source_t *source);

		/* ------------------------------------------------------------------------- */
		/* Source frame allocation functions */

		//EXPORT void obs_source_frame_init(struct obs_source_frame *frame, enum video_format format, uint32_t width, uint32_t height);

		public enum obs_source_type : int
		{
			OBS_SOURCE_TYPE_INPUT,
			OBS_SOURCE_TYPE_FILTER,
			OBS_SOURCE_TYPE_TRANSITION,
		};
	}
}