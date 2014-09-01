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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBS
{
	public class ObsProperty
	{
		internal unsafe libobs.obs_property* instance;    //pointer to unmanaged object

		public unsafe ObsProperty(libobs.obs_property* pointer)
		{
			instance = pointer;
		}

		unsafe ~ObsProperty()
		{
			instance = null;
		}


		public unsafe String Name
		{
			get
			{
				return libobs.obs_property_name((IntPtr)instance);
			}
		}

		public unsafe String Description
		{
			get
			{
				return libobs.obs_property_description((IntPtr)instance);
			}
		}

		public unsafe ObsPropertyType Type
		{
			get
			{
				return (ObsPropertyType)libobs.obs_property_get_type((IntPtr)instance);
			}
		}

		public unsafe bool Enabled
		{
			get
			{
				return libobs.obs_property_enabled((IntPtr)instance);
			}
		}

		public unsafe bool Visible
		{
			get
			{
				return libobs.obs_property_visible((IntPtr)instance);
			}
		}

	}

	public enum ObsPropertyType : int
	{
		Invalid,
		Bool,
		Int,
		Float,
		Text,
		Path,
		List,
		Color,
		Button,
		Font,
	};
}
