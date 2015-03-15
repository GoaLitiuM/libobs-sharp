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

using System.Runtime.InteropServices;

namespace OBS
{
	using axisang = libobs.vec4;

	using quat = libobs.vec4;

	public static partial class libobs
	{
		//EXPORT void matrix4_from_matrix3(struct matrix4 *dst, const struct matrix3 *m);
		//EXPORT void matrix4_from_quat(struct matrix4 *dst, const struct quat *q);
		//EXPORT void matrix4_from_axisang(struct matrix4 *dst, const struct axisang *aa);
		//EXPORT void matrix4_mul(struct matrix4 *dst, const struct matrix4 *m1, const struct matrix4 *m2);

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

		//EXPORT void matrix4_translate3v_i(struct matrix4 *dst, const struct vec3 *v, const struct matrix4 *m);
		//EXPORT void matrix4_translate4v_i(struct matrix4 *dst, const struct vec4 *v, const struct matrix4 *m);
		//EXPORT void matrix4_rotate_i(struct matrix4 *dst, const struct quat *q, const struct matrix4 *m);
		//EXPORT void matrix4_rotate_aa_i(struct matrix4 *dst, const struct axisang *aa, const struct matrix4 *m);
		//EXPORT void matrix4_scale_i(struct matrix4 *dst, const struct vec3 *v, const struct matrix4 *m);

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct matrix4
		{
			public vec4 x, y, z, t;
		};
	}
}