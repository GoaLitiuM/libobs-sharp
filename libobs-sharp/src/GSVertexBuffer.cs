﻿/***************************************************************************
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
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBS.Graphics
{
	public class GSVertexBuffer
	{
		internal IntPtr instance = IntPtr.Zero;    //pointer to unmanaged object

		public unsafe GSVertexBuffer(libobs.gs_vb_data data, UInt32 flags)
		{
			instance = libobs.gs_vertexbuffer_create(out data, flags);

			if (instance == IntPtr.Zero)
				throw new ApplicationException("gs_vertexbuffer_create failed");
		}

		public GSVertexBuffer(IntPtr instance)
		{
			this.instance = instance;
		}

		~GSVertexBuffer()
		{
			Release();
		}

		public void Release()
		{
			if (instance == IntPtr.Zero)
				return;

			libobs.gs_vertexbuffer_destroy(instance);
			instance = IntPtr.Zero;
		}

		public IntPtr GetPointer()
		{
			return instance;
		}

		public void Load(GSVertexBuffer vertexBuffer)
		{
			libobs.gs_load_vertexbuffer(vertexBuffer.GetPointer());
		}

		public void Flush(GSVertexBuffer vertexBuffer)
		{
			libobs.gs_vertexbuffer_flush(vertexBuffer.GetPointer());
		}
	}
}
