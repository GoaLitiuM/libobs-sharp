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
    using uint8_t = Byte;

	public static partial class libobs
	{
		public const string importLibrary = "obs";	//extension is handled automatically
		public const CallingConvention importCall = CallingConvention.Cdecl;
		public const CharSet importCharSet = CharSet.Ansi;

		/*
		 * helper functions
		 */

		//must be a null-terminated string
		private static string MarshalUTF8String(IntPtr strPtr)
		{
			var bytes = new List<byte>();
			int offset = 0;
			uint8_t chr = 0;

			do
			{
				if ((chr = Marshal.ReadByte(strPtr, offset++)) != 0)
					bytes.Add(chr);
			} while (chr != 0);

			return System.Text.Encoding.UTF8.GetString(bytes.ToArray());
		}
	}
}