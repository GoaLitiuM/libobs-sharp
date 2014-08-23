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
    using pthread_mutex_t = IntPtr;

    using signal_info_t = IntPtr;

    public static partial class libobs
    {
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct signal_handler
        {
            private signal_info first;
            private pthread_mutex_t mutex;
        };

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct signal_info
        {
            private decl_info func;
            private darray /*signal_callback*/   callbacks;
            private pthread_mutex_t mutex;

            [MarshalAs(UnmanagedType.I1)]
            private bool signalling;

            private signal_info_t next;
        };

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct decl_info
        {
            public string name;
            public string decl_string;
            private darray /*decl_param*/   _params;
        };

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct proc_handler
        {
            private darray /*proc_info*/ procs;
        };
    }
}