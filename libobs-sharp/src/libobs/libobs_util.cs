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

namespace OBS
{
	public static partial class libobs
	{
		//TODO: requires handling of va_list

		//UNSUPPORTED
		//set callback function for logging events
		//[DllImport(importLibrary, CallingConvention = importCall)]
		//public static extern void base_set_log_handler([MarshalAs(UnmanagedType.FunctionPtr)] log_handler_t handler, Object param);

		//UNSUPPORTED
		//[UnmanagedFunctionPointer(importCall, CharSet = importCharSet)]
		//public delegate void log_handler_t(int lvl, StringBuilder msg, IntPtr args, IntPtr p);
	}
}