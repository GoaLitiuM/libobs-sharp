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
	using gs_eparam_t = IntPtr;
	using graphics_t = IntPtr;
	using gs_shader_t = IntPtr;
	using gs_effect_param_t = IntPtr;
	using gs_sparam_t = IntPtr;

	using int64_t = Int64;
	using size_t = IntPtr;
	using uint32_t = UInt32;
	using uint64_t = UInt64;
	using uint8_t = Byte;

	public static partial class libobs
	{

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
		public unsafe struct gs_tvertarray
		{
			size_t width;
			void* array;
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
			public gs_tvertarray *tvarray;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_effect
		{
			public bool processing;
			public char* effect_path;
			public char* effect_dir;

			public darray params_; //gs_effect_param
			public darray techniques; //gs_effect_technique

			public gs_effect_technique* cur_technique;
			public gs_effect_pass* cur_pass;

			public gs_eparam_t view_proj, world, scale;
			public graphics_t graphics;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_effect_technique
		{
			public char* name;
			public effect_section section;
			public gs_effect* effect;

			public darray passes; //gs_effect_pass
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct pass_shaderparam
		{
			public gs_effect_param_t eparam;
			public gs_sparam_t sparam;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_effect_pass
		{
			public char* name;
			public effect_section section;

			public gs_shader_t vertshader;
			public gs_shader_t pixelshader;
			public darray vertshader_params; //pass_shaderparam
			public darray pixelshader_params; //pass_shaderparam
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct gs_effect_param
		{
			public char* name;
			public effect_section section;

			public gs_shader_param_type type;

			public bool changed;
			public darray cur_val; //uint8_t
			public darray default_val; //uint8_t

			public gs_effect* effect;
		};

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

		public enum effect_section : int
		{
			EFFECT_PARAM,
			EFFECT_TECHNIQUE,
			EFFECT_SAMPLER,
			EFFECT_PASS
		};

		public enum gs_shader_param_type : int
		{
			GS_SHADER_PARAM_UNKNOWN,
			GS_SHADER_PARAM_BOOL,
			GS_SHADER_PARAM_FLOAT,
			GS_SHADER_PARAM_INT,
			GS_SHADER_PARAM_STRING,
			GS_SHADER_PARAM_VEC2,
			GS_SHADER_PARAM_VEC3,
			GS_SHADER_PARAM_VEC4,
			GS_SHADER_PARAM_MATRIX4X4,
			GS_SHADER_PARAM_TEXTURE,
		};
	}
}