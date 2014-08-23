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
	public static partial class libobs
	{
		[StructLayoutAttribute(LayoutKind.Sequential)]
		public struct gs_window
		{
			public IntPtr hwnd;

			/*public gs_window(IntPtr handle)
			{
				hwnd = handle;
			}*/

			//TODO: OS X / Linux specific handles?
			//NOTE: sizeof gs_window in libobs not portable: one pointer in windows, pointer + uint32 in linux
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
	}
}