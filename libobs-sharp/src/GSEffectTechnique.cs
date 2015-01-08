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
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBS.Graphics
{
	public class GSEffectTechnique
	{
        internal IntPtr instance;   //pointer to unmanaged object

		public GSEffectTechnique(IntPtr ptr)
		{
            instance = ptr;
		}

		~GSEffectTechnique()
		{
			Release();
		}

		public void Release()
		{
            if (instance == IntPtr.Zero)
				return;

			instance = IntPtr.Zero;
		}

		public IntPtr GetPointer()
		{
			return instance;
		}
	}
}
