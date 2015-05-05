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
using System.Runtime.InteropServices;

namespace OBS
{
	public static partial class libobs
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void log_handler_t(log_error_level lvl, string msg, IntPtr args, IntPtr p);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void base_get_log_handler(out log_handler_t handler, out IntPtr param);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void base_set_log_handler([In] log_handler_t handler, IntPtr param);

		//EXPORT void base_set_crash_handler(void (*handler)(const char *, va_list, void *), void *param);

		[DllImport(importLibrary, CallingConvention = importCall, CharSet = importCharSet, EntryPoint = "blogva")]
		private static extern void import_blogva(int log_level, string format, IntPtr args);

		public static void blogva(int log_level, string format, params object[] args)
		{
			using (var va_list = new OBS.Utility.va_list(args))
				import_blogva(log_level, format, va_list.GetPointer());
		}

		//unsupported: EXPORT void blog(int log_level, const char *format, ...);
		//unsupported: EXPORT void bcrash(const char *format, ...);

		public enum log_error_level : int
		{
			LOG_ERROR   = 100, 
			LOG_WARNING = 200,
			LOG_INFO    = 300,
			LOG_DEBUG   = 400
		};
	}
}