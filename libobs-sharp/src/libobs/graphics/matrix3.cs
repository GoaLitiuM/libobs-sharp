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
	using axisang = Vector4;
	using quat = Vector4;

	public static partial class libobs
	{
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
		public static extern void matrix3_scale(out matrix3 dst, out matrix3 m, out Vector3 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_transpose(out matrix3 dst, out matrix3 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_inv(out matrix3 dst, out matrix3 m);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_mirror(out matrix3 dst, out matrix3 m, out plane p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void matrix3_mirrorv(out matrix3 dst, out matrix3 m, out Vector3 v);

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct matrix3
		{
			public Vector3 x;
			public Vector3 y;
			public Vector3 z;
			public Vector3 t;
		};
	}
}