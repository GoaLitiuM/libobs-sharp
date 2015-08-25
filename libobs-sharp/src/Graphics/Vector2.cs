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
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct Vector2
	{
		[FieldOffset(0)]
		public unsafe fixed float ptr[2];

		[FieldOffset(0)]
		public float x;

		[FieldOffset(4)]
		public float y;

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public void Abs()
		{
			libobs.vec2_abs(out this, out this);
		}

		public void Floor()
		{
			libobs.vec2_floor(out this, out this);
		}

		public void Ceil()
		{
			libobs.vec2_ceil(out this, out this);
		}

		public bool Close(out Vector2 v2, float epsilon)
		{
			return (libobs.vec2_close(out this, out v2, epsilon) != 0);
		}

		public void Normalize()
		{
			libobs.vec2_norm(out this, out this);
		}

		// TODO: use SSE intrinsics?

		public static Vector2 operator +(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x + v2.x, v1.y + v2.y);
		}

		public static Vector2 operator -(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x - v2.x, v1.y - v2.y);
		}

		public static Vector2 operator *(Vector2 v1, float f)
		{
			return new Vector2(v1.x * f, v1.y * f);
		}

		public static Vector2 operator *(float f, Vector2 v1)
		{
			return new Vector2(v1.x * f, v1.y * f);
		}

		public System.Drawing.Point ToPoint()
		{
			return new System.Drawing.Point((int)x, (int)y);
		}
	};
}