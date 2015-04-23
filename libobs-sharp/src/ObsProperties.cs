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
using System.Collections.Generic;

namespace OBS
{
	public class ObsProperties : IDisposable
	{
		internal IntPtr instance;    //pointer to unmanaged object

		public unsafe ObsProperties(IntPtr pointer)
		{
			instance = pointer;
		}

		public unsafe void Dispose()
		{
			if (instance == IntPtr.Zero)
				return;

			libobs.obs_properties_destroy((IntPtr)instance);
			instance = IntPtr.Zero;
		}

		public IntPtr GetPointer()
		{
			return instance;
		}

		public unsafe ObsPropertiesFlags Flags
		{
			get { return (ObsPropertiesFlags)libobs.obs_properties_get_flags(instance); }
			set { libobs.obs_properties_set_flags(instance, (uint)value); }
		}

		public unsafe ObsProperty[] GetPropertyList()
		{
			List<ObsProperty> propertyList = new List<ObsProperty>();

			IntPtr property = libobs.obs_properties_first(instance);

			while (property != IntPtr.Zero)
			{
				propertyList.Add(new ObsProperty(property, this));

				IntPtr next = (IntPtr)property;
				libobs.obs_property_next(out next);
				property = next;
			}

			return propertyList.ToArray();
		}
	}

	[Flags]
	public enum ObsPropertiesFlags : uint
	{
		DeferUpdate = (1 << 0),
	};

	[Flags]
	public enum ObsFontFlags : uint
	{
		Bold = (1 << 0),
		Italic = (1 << 1),
		Underline = (1 << 2),
		Strikeout = (1 << 3),
	};
}