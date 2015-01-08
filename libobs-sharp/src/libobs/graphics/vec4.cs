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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OBS
{

    public static partial class libobs
    {
        //EXPORT void vec4_from_vec3(struct vec4 *dst, const struct vec3 *v);
        //EXPORT void vec4_transform(struct vec4 *dst, const struct vec4 *v, const struct matrix4 *m);


        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public struct vec4
        {
            public vec4(float x, float y, float z, float w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }

            [FieldOffset(0)]
            public float x;

            [FieldOffset(4)]
            public float y;

            [FieldOffset(8)]
            public float z;

            [FieldOffset(12)]
            public float w;

            [FieldOffset(0)]
            public unsafe fixed float ptr[4];
        };

    }
}