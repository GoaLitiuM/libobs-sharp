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
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	public struct Vector3
	{
		[FieldOffset(0)]
		public unsafe fixed float ptr[3];

		[FieldOffset(0)]
		public float x;

		[FieldOffset(4)]
		public float y;

		[FieldOffset(8)]
		public float z;

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3(Vector4 vector4)
		{
			libobs.vec3_from_vec4(out this, out vector4);
		}

		public Vector3(Vector2 vector2)
			: this(vector2.x, vector2.y, 0.0f)
		{
		}

		public Vector3(System.Drawing.Point point)
		{
			x = point.X;
			y = point.Y;
			z = 0.0f;
		}

		public float DistanceTo(libobs.plane plane)
		{
			return libobs.vec3_plane_dist(out this, out plane);
		}

		public void Transform(Vector3 other, libobs.matrix4 matrix4)
		{
			libobs.vec3_transform(out this, out other, out matrix4);
		}

		public void Transform(libobs.matrix4 matrix4)
		{
			libobs.vec3_transform(out this, out this, out matrix4);
		}

		public void Transform(Vector3 other, libobs.matrix3 matrix3)
		{
			libobs.vec3_transform3x4(out this, out other, out matrix3);
		}

		public void Transform(libobs.matrix3 matrix3)
		{
			libobs.vec3_transform3x4(out this, out this, out matrix3);
		}

		public void Rotate(Vector3 other, libobs.matrix3 matrix3)
		{
			libobs.vec3_rotate(out this, out other, out matrix3);
		}

		public void Rotate(libobs.matrix3 matrix3)
		{
			libobs.vec3_rotate(out this, out this, out matrix3);
		}

		public void Mirror(Vector3 other, libobs.plane plane)
		{
			libobs.vec3_mirror(out this, out other, out plane);
		}

		public void Mirror(libobs.plane plane)
		{
			libobs.vec3_mirror(out this, out this, out plane);
		}

		public void Mirror(Vector3 other, Vector3 other2)
		{
			libobs.vec3_mirrorv(out this, out other, out other2);
		}

		public void Randomize(bool positiveOnly)
		{
			libobs.vec3_rand(out this, positiveOnly ? 1 : 0);
		}

		public static Vector3 GetTransform(Vector3 other, libobs.matrix4 matrix4)
		{
			Vector3 vec;
			libobs.vec3_transform(out vec, out other, out matrix4);
			return vec;
		}

		// TODO: use SSE intrinsics?

		public static Vector3 operator +(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}

		public static Vector3 operator -(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}

		public static Vector3 operator *(Vector3 v1, float f)
		{
			return new Vector3(v1.x * f, v1.y * f, v1.z * f);
		}

		public static Vector3 operator *(float f, Vector3 v1)
		{
			return new Vector3(v1.x * f, v1.y * f, v1.z * f);
		}

		public System.Drawing.Point ToPoint()
		{
			return new System.Drawing.Point((int)x, (int)y);
		}

		public override string ToString()
		{
			return "{ " + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + " }";
		}
	};
}