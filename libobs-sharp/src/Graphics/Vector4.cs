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
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct Vector4
	{
		[FieldOffset(0)]
		public unsafe fixed float ptr[4];

		[FieldOffset(0)]
		public float x;

		[FieldOffset(4)]
		public float y;

		[FieldOffset(8)]
		public float z;

		[FieldOffset(12)]
		public float w;

		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public Vector4(Vector3 vector3)
		{
			libobs.vec4_from_vec3(out this, out vector3);
		}

		public Vector4(System.Drawing.Color color)
			: this((float)color.R / 255, (float)color.G / 255, (float)color.B / 255, (float)color.A / 255)
		{
		}

		public void Transform(Vector4 other, libobs.matrix4 matrix4)
		{
			libobs.vec4_transform(out this, out other, out matrix4);
		}

		public void Transform(libobs.matrix4 matrix4)
		{
			libobs.vec4_transform(out this, out this, out matrix4);
		}

		// TODO: use SSE intrinsics?

		public static Vector4 operator +(Vector4 v1, Vector4 v2)
		{
			return new Vector4(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
		}

		public static Vector4 operator -(Vector4 v1, Vector4 v2)
		{
			return new Vector4(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
		}

		public static Vector4 operator *(Vector4 v1, float f)
		{
			return new Vector4(v1.x * f, v1.y * f, v1.z * f, v1.w * f);
		}

		public static Vector4 operator *(float f, Vector4 v1)
		{
			return new Vector4(v1.x * f, v1.y * f, v1.z * f, v1.w * f);
		}

		public System.Drawing.Color ToColor()
		{
			return System.Drawing.Color.FromArgb((int)(w * 255), (int)(x * 255), (int)(y * 255), (int)(z * 255));
		}
	};
}