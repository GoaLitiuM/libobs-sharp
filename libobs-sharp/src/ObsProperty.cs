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
		internal ObsProperties properties = null;

		public unsafe ObsProperty(libobs.obs_property* pointer, ObsProperties props)
		{
			instance = pointer;
			properties = props;
			properties.AddRef();
		}

		unsafe ~ObsProperty()
		{
			instance = null;

			if (properties == null)
				return;

			properties.Release();
			properties = null;
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

	public class ObsProperties
	{
		internal unsafe libobs.obs_properties* instance;    //pointer to unmanaged object
		internal int refs = 0;

		public unsafe ObsProperties(libobs.obs_properties* pointer)
		{
			instance = pointer;
		}

		unsafe ~ObsProperties()
		{
			Release();
		}

		public unsafe void AddRef()
		{
			if (instance == null)
				return;

			refs++;
		}

		public unsafe void Release()
		{
			refs--;

			if (refs > 0 || instance == null)
				return;

			libobs.obs_properties_destroy((IntPtr)instance);
			instance = null;
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
