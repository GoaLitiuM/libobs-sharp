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

using OBS.Utility;
using System;
using System.Runtime.InteropServices;

namespace OBS
{
	public static partial class Obs
	{
		// prevent garbage collector from freeing the delegate 
		private static libobs.log_handler_t wrapperHandler = LogHandlerWrapper;
		private static LogHandlerDelegate realHandler;

		// wrapped log handler, message arguments are automatically formatted 
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void LogHandlerDelegate(LogErrorLevel level, string message, IntPtr ptr);

		/// <summary> Get current OBS log handler. </summary>
		public static void GetLogHandler(out libobs.log_handler_t handler, out IntPtr param)
		{
			libobs.base_get_log_handler(out handler, out param);
		}

		/// <summary>
		/// Get current OBS log handler. Handler is set to null if non-wrapped handler is being used.
		/// </summary>
		public static void GetLogHandler(out LogHandlerDelegate handler, out IntPtr param)
		{
			libobs.log_handler_t currentHandler = null;
			libobs.base_get_log_handler(out currentHandler, out param);

			if (currentHandler == wrapperHandler)
				handler = realHandler;
			else
				handler = null;
		}

		/// <summary> Sets OBS current log handler and automatically handles message formatting. </summary>
		public static void SetLogHandler(LogHandlerDelegate handler)
		{
			realHandler = handler;
			libobs.base_set_log_handler(wrapperHandler, IntPtr.Zero);
		}

		/// <summary> Sets OBS current log handler and automatically handles message formatting. </summary>
		public static void SetLogHandler(LogHandlerDelegate handler, IntPtr param)
		{
			realHandler = handler;
			libobs.base_set_log_handler(wrapperHandler, param);
		}

		/// <summary> Sets OBS current log handler. </summary>
		public static void SetLogHandler(libobs.log_handler_t handler)
		{
			realHandler = null;
			libobs.base_set_log_handler(handler, IntPtr.Zero);
		}

		/// <summary> Sets OBS current log handler. </summary>
		public static void SetLogHandler(libobs.log_handler_t handler, IntPtr param)
		{
			realHandler = null;
			libobs.base_set_log_handler(handler, param);
		}

		/// <summary> Logs a message. </summary>
		/// <param name="level"> Error level of the message. </param>
		/// <param name="message"> Message. </param>
		public static void Log(LogErrorLevel level, string message)
		{
			// escape '%'s to prevent formatting
			libobs.blogva((int)level, message.Replace("%", "%%"));
		}

		/// <summary> Logs a message using composite string formatting. </summary>
		/// <param name="level"> Error level of the message. </param>
		/// <param name="format"> Format string. </param>
		/// <param name="args"> Any number of parameters used in formatting. </param>
		public static void Log(LogErrorLevel level, string format, params object[] args)
		{
			// escape '%'s to prevent formatting
			string formattedMsg = String.Format(format, args).Replace("%", "%%");
			libobs.blogva((int)level, formattedMsg);
		}

		/// <summary> Logs a message using C-style printf formatting. </summary>
		/// <param name="level"> Error level of the message. </param>
		/// <param name="format"> Format string. </param>
		/// <param name="args"> Any number of parameters used in formatting. </param>
		public static void Logf(LogErrorLevel level, string format, params object[] args)
		{
			// let libobs handle the formatting 
			libobs.blogva((int)level, format, args);
		}

		/// <summary> Wraps, parses and formats the variable arguments of the real delegate. </summary>
		private static void LogHandlerWrapper(libobs.log_error_level lvl, string format, IntPtr args, IntPtr p)
		{
			using (va_list arglist = new va_list(args))
			{
				object[] objs = arglist.GetObjectsByFormat(format);
				string formattedMsg = Printf.sprintf(format, objs);

				realHandler((LogErrorLevel)lvl, formattedMsg, p);
			}
		}
	}

	public enum LogErrorLevel : int
	{
		Error = 100,
		Warning = 200,
		Info = 300,
		Debug = 400
	};
}