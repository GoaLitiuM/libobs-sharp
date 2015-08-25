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
	public static partial class libobs
	{
		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec2_abs(out Vector2 dst, out Vector2 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec2_floor(out Vector2 dst, out Vector2 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec2_ceil(out Vector2 dst, out Vector2 v);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern int vec2_close(out Vector2 v1, out Vector2 v2, float epsilon);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void vec2_norm(out Vector2 dst, out Vector2 v);
	}
}