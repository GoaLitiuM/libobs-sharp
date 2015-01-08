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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OBS
{

    public static partial class libobs
    {
        //EXPORT void vec3_from_vec4(struct vec3 *dst, const struct vec4 *v);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern float vec3_plane_dist(out vec3 v, out plane p);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void vec3_transform(out vec3 dst, out vec3 v, out matrix4 m);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void vec3_rotate(out vec3 dst, out vec3 v, out matrix3 m);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void vec3_transform3x4(out vec3 dst, out vec3 v, out matrix3 m);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void vec3_mirror(out vec3 dst, out vec3 v, out plane p);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void vec3_mirrorv(out vec3 dst, out vec3 v, out vec3 vec);

        [DllImport(importLibrary, CallingConvention = importCall)]
        public static extern void vec3_rand(out vec3 dst, int positive_only);


        [StructLayout(LayoutKind.Explicit, Size = 12)]
        public struct vec3
        {
            public vec3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            [FieldOffset(0)]
            public float x;

            [FieldOffset(4)]
            public float y;

            [FieldOffset(8)]
            public float z;

            [FieldOffset(0)]
            public unsafe fixed float ptr[3];
        };

    }

}