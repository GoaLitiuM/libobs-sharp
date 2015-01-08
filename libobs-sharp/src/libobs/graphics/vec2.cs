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
        //EXPORT void vec2_abs(struct vec2 *dst, const struct vec2 *v);
        //EXPORT void vec2_floor(struct vec2 *dst, const struct vec2 *v);
        //EXPORT void vec2_ceil(struct vec2 *dst, const struct vec2 *v);
        //EXPORT int vec2_close(const struct vec2 *v1, const struct vec2 *v2, float epsilon);
        //EXPORT void vec2_norm(struct vec2 *dst, const struct vec2 *v);


        [StructLayout(LayoutKind.Explicit, Size = 8)]
        public struct vec2
        {
            public vec2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            [FieldOffset(0)]
            public float x;

            [FieldOffset(4)]
            public float y;

            [FieldOffset(0)]
            public unsafe fixed float ptr[2];
        };

    }

}