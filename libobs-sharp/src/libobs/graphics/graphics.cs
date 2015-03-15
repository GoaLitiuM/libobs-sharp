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
	using axisang = libobs.vec4;
	using gs_effect_t = IntPtr;

	using gs_eparam_t = IntPtr;
	using gs_indexbuffer_t = IntPtr;
	using gs_samplerstate_t = IntPtr;
	using gs_shader_t = IntPtr;
	using gs_technique_t = IntPtr;
	using gs_texture_t = IntPtr;
	using gs_vertbuffer_t = IntPtr;
	using quat = libobs.vec4;
	using size_t = IntPtr;	//UIntPtr?

	using uint32_t = UInt32;
	using uint8_t = Byte;

	public static partial class libobs
	{
		/* ---------------------------------------------------
		 * shader functions
		 * --------------------------------------------------- */

		//EXPORT void gs_shader_destroy(gs_shader_t *shader);
		//EXPORT int gs_shader_get_num_params(const gs_shader_t *shader);
		//EXPORT gs_sparam_t *gs_shader_get_param_by_idx(gs_shader_t *shader, uint32_t param);
		//EXPORT gs_sparam_t *gs_shader_get_param_by_name(gs_shader_t *shader, const char *name);
		//EXPORT gs_sparam_t *gs_shader_get_viewproj_matrix(const gs_shader_t *shader);
		//EXPORT gs_sparam_t *gs_shader_get_world_matrix(const gs_shader_t *shader);
		//EXPORT void gs_shader_get_param_info(const gs_sparam_t *param, struct gs_shader_param_info *info);
		//EXPORT void gs_shader_set_bool(gs_sparam_t *param, bool val);
		//EXPORT void gs_shader_set_float(gs_sparam_t *param, float val);
		//EXPORT void gs_shader_set_int(gs_sparam_t *param, int val);
		//EXPORT void gs_shader_setmatrix3(gs_sparam_t *param, const struct matrix3 *val);
		//EXPORT void gs_shader_set_matrix4(gs_sparam_t *param, const struct matrix4 *val);
		//EXPORT void gs_shader_set_vec2(gs_sparam_t *param, const struct vec2 *val);
		//EXPORT void gs_shader_set_vec3(gs_sparam_t *param, const struct vec3 *val);
		//EXPORT void gs_shader_set_vec4(gs_sparam_t *param, const struct vec4 *val);
		//EXPORT void gs_shader_set_texture(gs_sparam_t *param, gs_texture_t *val);
		//EXPORT void gs_shader_set_val(gs_sparam_t *param, const void *val, size_t size);
		//EXPORT void gs_shader_set_default(gs_sparam_t *param);

		/* ---------------------------------------------------
		 * effect functions
		 * --------------------------------------------------- */

		//EXPORT void gs_effect_destroy(gs_effect_t *effect);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_technique_t gs_effect_get_technique(gs_effect_t effect, string name);

		//EXPORT gs_technique_t *gs_effect_get_current_technique( const gs_effect_t *effect);

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
		public static extern size_t gs_effect_get_num_params(gs_effect_t effect);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_eparam_t gs_effect_get_param_by_idx(gs_effect_t effect, size_t param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_eparam_t gs_effect_get_param_by_name(gs_effect_t effect, string name);

		//EXPORT bool gs_effect_loop(gs_effect_t *effect, const char *name);
		//EXPORT void gs_effect_update_params(gs_effect_t *effect);
		//EXPORT gs_eparam_t *gs_effect_get_viewproj_matrix(const gs_effect_t *effect);
		//EXPORT gs_eparam_t *gs_effect_get_world_matrix(const gs_effect_t *effect);
		//EXPORT void gs_effect_get_param_info(const gs_eparam_t *param, struct gs_effect_param_info *info);

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

		/* ---------------------------------------------------
		 * texture render helper functions
		 * --------------------------------------------------- */

		//EXPORT gs_texrender_t *gs_texrender_create(enum gs_color_format format, enum gs_zstencil_format zsformat);
		//EXPORT void gs_texrender_destroy(gs_texrender_t *texrender);
		//EXPORT bool gs_texrender_begin(gs_texrender_t *texrender, uint32_t cx, uint32_t cy);
		//EXPORT void gs_texrender_end(gs_texrender_t *texrender);
		//EXPORT void gs_texrender_reset(gs_texrender_t *texrender);
		//EXPORT gs_texture_t *gs_texrender_get_texture(const gs_texrender_t *texrender);

		/* ---------------------------------------------------
		 * graphics subsystem
		 * --------------------------------------------------- */

		//EXPORT const char *gs_get_device_name(void);
		//EXPORT int gs_get_device_type(void);
		//EXPORT void gs_enum_adapters(bool (*callback)(void *param, const char *name, uint32_t id), void *param);

		//EXPORT int gs_create(graphics_t **graphics, const char *module, const struct gs_init_data *data);
		//EXPORT void gs_destroy(graphics_t *graphics);

		//EXPORT void gs_enter_context(graphics_t *graphics);
		//EXPORT void gs_leave_context(void);
		//EXPORT graphics_t *gs_get_context(void);

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

		//EXPORT input_t *gs_get_input(void);
		//EXPORT gs_effect_t *gs_get_effect(void);

		//EXPORT gs_effect_t *gs_effect_create_from_file(const char *file, char **error_string);
		//EXPORT gs_effect_t *gs_effect_create(const char *effect_string, const char *filename, char **error_string);

		//EXPORT gs_shader_t *gs_vertexshader_create_from_file(const char *file, char **error_string);
		//EXPORT gs_shader_t *gs_pixelshader_create_from_file(const char *file, char **error_string);

		//EXPORT gs_texture_t *gs_texture_create_from_file(const char *file);

		//EXPORT void gs_draw_sprite(gs_texture_t *tex, uint32_t flip, uint32_t width, uint32_t height);
		//EXPORT void gs_draw_cube_backdrop(gs_texture_t *cubetex, const struct quat *rot, float left, float right, float top, float bottom, float znear);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_reset_viewport();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_set_2d_mode();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_set_3d_mode(double fovy, double znear, double zvar);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_viewport_push();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_viewport_pop();

		//EXPORT void gs_texture_set_image(gs_texture_t *tex, const uint8_t *data, uint32_t linesize, bool invert);
		//EXPORT void gs_cubetexture_set_image(gs_texture_t *cubetex, uint32_t side, const void *data, uint32_t linesize, bool invert);

		//EXPORT void gs_perspective(float fovy, float aspect, float znear, float zfar);

		//EXPORT void gs_blend_state_push(void);
		//EXPORT void gs_blend_state_pop(void);
		//EXPORT void gs_reset_blend_state(void);

		//EXPORT gs_swapchain_t *gs_swapchain_create(const struct gs_init_data *data);
		//EXPORT void gs_resize(uint32_t x, uint32_t y);
		//EXPORT void gs_get_size(uint32_t *x, uint32_t *y);
		//EXPORT uint32_t gs_get_width(void);
		//EXPORT uint32_t gs_get_height(void);

		//EXPORT gs_texture_t *gs_texture_create(uint32_t width, uint32_t height, enum gs_color_format color_format, uint32_t levels, const uint8_t **data, uint32_t flags);
		//EXPORT gs_texture_t *gs_cubetexture_create(uint32_t size, enum gs_color_format color_format, uint32_t levels, const uint8_t **data, uint32_t flags);
		//EXPORT gs_texture_t *gs_voltexture_create(uint32_t width, uint32_t height, uint32_t depth, enum gs_color_format color_format, uint32_t levels, const uint8_t **data, uint32_t flags);

		//EXPORT gs_zstencil_t *gs_zstencil_create(uint32_t width, uint32_t height, enum gs_zstencil_format format);
		//EXPORT gs_stagesurf_t *gs_stagesurface_create(uint32_t width, uint32_t height, enum gs_color_format color_format);
		//EXPORT gs_samplerstate_t *gs_samplerstate_create( const struct gs_sampler_info *info);

		//EXPORT gs_shader_t *gs_vertexshader_create(const char *shader, const char *file, char **error_string);
		//EXPORT gs_shader_t *gs_pixelshader_create(const char *shader, const char *file, char **error_string);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern gs_vertbuffer_t gs_vertexbuffer_create(out gs_vb_data data, uint32_t flags);

		//EXPORT gs_indexbuffer_t *gs_indexbuffer_create(enum gs_index_type type, void *indices, size_t num, uint32_t flags);

		//EXPORT enum gs_texture_type gs_get_texture_type(const gs_texture_t *texture);

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

		//EXPORT void gs_load_default_samplerstate(bool b_3d, int unit);

		//EXPORT gs_shader_t *gs_get_vertex_shader(void);
		//EXPORT gs_shader_t *gs_get_pixel_shader(void);

		//EXPORT gs_texture_t  *gs_get_render_target(void);
		//EXPORT gs_zstencil_t *gs_get_zstencil_target(void);

		//EXPORT void gs_set_render_target(gs_texture_t *tex, gs_zstencil_t *zstencil);
		//EXPORT void gs_set_cube_render_target(gs_texture_t *cubetex, int side, gs_zstencil_t *zstencil);

		//EXPORT void gs_copy_texture(gs_texture_t *dst, gs_texture_t *src);
		//EXPORT void gs_copy_texture_region( gs_texture_t *dst, uint32_t dst_x, uint32_t dst_y, gs_texture_t *src, uint32_t src_x, uint32_t src_y, uint32_t src_w, uint32_t src_h);
		//EXPORT void gs_stage_texture(gs_stagesurf_t *dst, gs_texture_t *src);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_begin_scene();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_draw(gs_draw_mode draw_mode, uint32_t start_vert, uint32_t num_verts);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_end_scene();

		//EXPORT void gs_load_swapchain(gs_swapchain_t *swapchain);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_clear(uint32_t clear_flags, out vec4 color, float depth, uint8_t stencil);

		//EXPORT void gs_present(void);
		//EXPORT void gs_flush(void);

		//EXPORT void gs_set_cull_mode(enum gs_cull_mode mode);
		//EXPORT enum gs_cull_mode gs_get_cull_mode(void);

		//EXPORT void gs_enable_blending(bool enable);
		//EXPORT void gs_enable_depth_test(bool enable);
		//EXPORT void gs_enable_stencil_test(bool enable);
		//EXPORT void gs_enable_stencil_write(bool enable);
		//EXPORT void gs_enable_color(bool red, bool green, bool blue, bool alpha);

		//EXPORT void gs_blend_function(enum gs_blend_type src, enum gs_blend_type dest);
		//EXPORT void gs_depth_function(enum gs_depth_test test);

		//EXPORT void gs_stencil_function(enum gs_stencil_side side, enum gs_depth_test test);
		//EXPORT void gs_stencil_op(enum gs_stencil_side side, enum gs_stencil_op_type fail, enum gs_stencil_op_type zfail, enum gs_stencil_op_type zpass);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_set_viewport(int x, int y, int width, int height);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_get_viewport(out gs_rect rect);

		//EXPORT void gs_set_scissor_rect(const struct gs_rect *rect);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_ortho(float left, float right, float top, float bottom, float znear, float zfar);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_frustum(float left, float right, float top, float bottom, float znear, float zfar);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_projection_push();

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_projection_pop();

		//EXPORT void     gs_swapchain_destroy(gs_swapchain_t *swapchain);
		//EXPORT void     gs_texture_destroy(gs_texture_t *tex);
		//EXPORT uint32_t gs_texture_get_width(const gs_texture_t *tex);
		//EXPORT uint32_t gs_texture_get_height(const gs_texture_t *tex);
		//EXPORT enum gs_color_format gs_texture_get_color_format( const gs_texture_t *tex);
		//EXPORT bool     gs_texture_map(gs_texture_t *tex, uint8_t **ptr, uint32_t *linesize);
		//EXPORT void     gs_texture_unmap(gs_texture_t *tex);

		//EXPORT bool     gs_texture_is_rect(const gs_texture_t *tex);
		//EXPORT void    *gs_texture_get_obj(gs_texture_t *tex);

		//EXPORT void     gs_cubetexture_destroy(gs_texture_t *cubetex);
		//EXPORT uint32_t gs_cubetexture_get_size(const gs_texture_t *cubetex);
		//EXPORT enum gs_color_format gs_cubetexture_get_color_format( const gs_texture_t *cubetex);

		//EXPORT void     gs_voltexture_destroy(gs_texture_t *voltex);
		//EXPORT uint32_t gs_voltexture_get_width(const gs_texture_t *voltex);
		//EXPORT uint32_t gs_voltexture_get_height(const gs_texture_t *voltex);
		//EXPORT uint32_t gs_voltexture_getdepth(const gs_texture_t *voltex);
		//EXPORT enum gs_color_format gs_voltexture_get_color_format( const gs_texture_t *voltex);

		//EXPORT void     gs_stagesurface_destroy(gs_stagesurf_t *stagesurf);
		//EXPORT uint32_t gs_stagesurface_get_width(const gs_stagesurf_t *stagesurf);
		//EXPORT uint32_t gs_stagesurface_get_height(const gs_stagesurf_t *stagesurf);
		//EXPORT enum gs_color_format gs_stagesurface_get_color_format( const gs_stagesurf_t *stagesurf);
		//EXPORT bool     gs_stagesurface_map(gs_stagesurf_t *stagesurf, uint8_t **data, uint32_t *linesize);
		//EXPORT void     gs_stagesurface_unmap(gs_stagesurf_t *stagesurf);
		//EXPORT void     gs_zstencil_destroy(gs_zstencil_t *zstencil);

		//EXPORT void     gs_samplerstate_destroy(gs_samplerstate_t *samplerstate);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_vertexbuffer_destroy(gs_vertbuffer_t vertbuffer);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void gs_vertexbuffer_flush(gs_vertbuffer_t vertbuffer);

		//EXPORT struct gs_vb_data *gs_vertexbuffer_get_data( const gs_vertbuffer_t *vertbuffer);

		//EXPORT void     gs_indexbuffer_destroy(gs_indexbuffer_t *indexbuffer);
		//EXPORT void     gs_indexbuffer_flush(gs_indexbuffer_t *indexbuffer);
		//EXPORT void     *gs_indexbuffer_get_data(const gs_indexbuffer_t *indexbuffer);
		//EXPORT size_t   gs_indexbuffer_get_num_indices( const gs_indexbuffer_t *indexbuffer);
		//EXPORT enum gs_index_type gs_indexbuffer_get_type( const gs_indexbuffer_t *indexbuffer);

		//EXPORT gs_texture_t *gs_texture_create_from_iosurface(void *iosurf);
		//EXPORT bool     gs_texture_rebind_iosurface(gs_texture_t *texture, void *iosurf);

		//EXPORT bool gs_gdi_texture_available(void);
		//EXPORT bool gs_shared_texture_available(void);

		//EXPORT bool gs_get_duplicator_monitor_info(int monitor_idx, struct gs_monitor_info *monitor_info);

		//EXPORT gs_duplicator_t *gs_duplicator_create(int monitor_idx);
		//EXPORT void gs_duplicator_destroy(gs_duplicator_t *duplicator);

		//EXPORT bool gs_duplicator_update_frame(gs_duplicator_t *duplicator);
		//EXPORT gs_texture_t *gs_duplicator_get_texture(gs_duplicator_t *duplicator);

		//EXPORT gs_texture_t *gs_texture_create_gdi(uint32_t width, uint32_t height);

		//EXPORT void *gs_texture_get_dc(gs_texture_t *gdi_tex);
		//EXPORT void gs_texture_release_dc(gs_texture_t *gdi_tex);

		//EXPORT gs_texture_t *gs_texture_open_shared(uint32_t handle);

		public enum gs_draw_mode : int
		{
			GS_POINTS,
			GS_LINES,
			GS_LINESTRIP,
			GS_TRIS,
			GS_TRISTRIP
		};

		public enum gs_color_format : int
		{
			GS_UNKNOWN,
			GS_A8,
			GS_R8,
			GS_RGBA,
			GS_BGRX,
			GS_BGRA,
			GS_R10G10B10A2,
			GS_RGBA16,
			GS_R16,
			GS_RGBA16F,
			GS_RGBA32F,
			GS_RG16F,
			GS_RG32F,
			GS_R16F,
			GS_R32F,
			GS_DXT1,
			GS_DXT3,
			GS_DXT5
		};

		public enum gs_zstencil_format : int
		{
			GS_ZS_NONE,
			GS_Z16,
			GS_Z24_S8,
			GS_Z32F,
			GS_Z32F_S8X24
		};

		public enum gs_index_type : int
		{
			GS_UNSIGNED_SHORT,
			GS_UNSIGNED_LONG
		};

		public enum gs_cull_mode : int
		{
			GS_BACK,
			GS_FRONT,
			GS_NEITHER
		};

		public enum gs_blend_type : int
		{
			GS_BLEND_ZERO,
			GS_BLEND_ONE,
			GS_BLEND_SRCCOLOR,
			GS_BLEND_INVSRCCOLOR,
			GS_BLEND_SRCALPHA,
			GS_BLEND_INVSRCALPHA,
			GS_BLEND_DSTCOLOR,
			GS_BLEND_INVDSTCOLOR,
			GS_BLEND_DSTALPHA,
			GS_BLEND_INVDSTALPHA,
			GS_BLEND_SRCALPHASAT
		};

		public enum gs_depth_test : int
		{
			GS_NEVER,
			GS_LESS,
			GS_LEQUAL,
			GS_EQUAL,
			GS_GEQUAL,
			GS_GREATER,
			GS_NOTEQUAL,
			GS_ALWAYS
		};

		public enum gs_stencil_side : int
		{
			GS_STENCIL_FRONT = 1,
			GS_STENCIL_BACK,
			GS_STENCIL_BOTH
		};

		public enum gs_stencil_op_type : int
		{
			GS_KEEP,
			GS_ZERO,
			GS_REPLACE,
			GS_INCR,
			GS_DECR,
			GS_INVERT
		};

		public enum gs_cube_sides : int
		{
			GS_POSITIVE_X,
			GS_NEGATIVE_X,
			GS_POSITIVE_Y,
			GS_NEGATIVE_Y,
			GS_POSITIVE_Z,
			GS_NEGATIVE_Z
		};

		public enum gs_sample_filter : int
		{
			GS_FILTER_POINT,
			GS_FILTER_LINEAR,
			GS_FILTER_ANISOTROPIC,
			GS_FILTER_MIN_MAG_POINT_MIP_LINEAR,
			GS_FILTER_MIN_POINT_MAG_LINEAR_MIP_POINT,
			GS_FILTER_MIN_POINT_MAG_MIP_LINEAR,
			GS_FILTER_MIN_LINEAR_MAG_MIP_POINT,
			GS_FILTER_MIN_LINEAR_MAG_POINT_MIP_LINEAR,
			GS_FILTER_MIN_MAG_LINEAR_MIP_POINT,
		};

		public enum gs_address_mode : int
		{
			GS_ADDRESS_CLAMP,
			GS_ADDRESS_WRAP,
			GS_ADDRESS_MIRROR,
			GS_ADDRESS_BORDER,
			GS_ADDRESS_MIRRORONCE
		};

		public enum gs_texture_type : int
		{
			GS_TEXTURE_2D,
			GS_TEXTURE_3D,
			GS_TEXTURE_CUBE
		};

		/*
		 * wrapper structures
		 */

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_monitor_info
		{
			public int rotation_degrees;
			public int x;
			public int y;
			public int cx;
			public int cy;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_tvertarray
		{
			public size_t width;
			public void* array;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_vb_data
		{
			public size_t num;
			public vec3* points;
			public vec3* normals;
			public vec3* tangents;
			public uint32_t* colors;

			public size_t num_tex;
			public gs_tvertarray* tvarray;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_sampler_info
		{
			public gs_sample_filter filter;
			public gs_address_mode address_u;
			public gs_address_mode address_v;
			public gs_address_mode address_w;
			public int max_anisotropy;
			public uint32_t border_color;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_display_mode
		{
			public uint32_t width;
			public uint32_t height;
			public uint32_t bits;
			public uint32_t freq;
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

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct gs_window
		{
			public IntPtr hwnd;

			//TODO: OS X / Linux specific handles?
			//Windows: Handle refers to HWND
			//OS X: Handle refers to HIView
			//Linux: Handle refers to Window or GdkWindow* (GTK+)
			//NOTE: sizeof gs_window in libobs not portable: one pointer in windows, pointer + uint32 in linux
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_init_data
		{
			public gs_window window;
			public uint32_t cx, cy;
			public uint32_t num_backbuffers;
			public gs_color_format format;
			public gs_zstencil_format zsformat;
			public uint32_t adapter;
		};
	}
}