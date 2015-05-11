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
	public class ObsProperty : IDisposable
	{
		internal IntPtr instance;   //pointer to unmanaged object
		internal ObsProperties properties = null;

		public unsafe ObsProperty(IntPtr pointer, ObsProperties props)
		{
			instance = pointer;
			properties = props;
		}

		public unsafe void Dispose()
		{
			instance = IntPtr.Zero;

			if (properties == null)
				return;

			properties.Dispose();
			properties = null;
		}

		public IntPtr GetPointer()
		{
			return instance;
		}

		public unsafe String Name
		{
			get { return libobs.obs_property_name(instance); }
		}

		public unsafe String Description
		{
			get { return libobs.obs_property_description(instance); }
			set { libobs.obs_property_set_description(instance, value); }
		}

		public unsafe ObsPropertyType Type
		{
			get { return (ObsPropertyType)libobs.obs_property_get_type(instance); }
		}

		public unsafe bool Enabled
		{
			get { return libobs.obs_property_enabled(instance); }
			set { libobs.obs_property_set_enabled(instance, value); }
		}

		public unsafe bool Visible
		{
			get { return libobs.obs_property_visible(instance); }
			set { libobs.obs_property_set_visible(instance, value); }
		}

		public unsafe int IntMin
		{
			get { return libobs.obs_property_int_min(instance); }
		}

		public unsafe int IntMax
		{
			get { return libobs.obs_property_int_max(instance); }
		}

		public unsafe int IntStep
		{
			get { return libobs.obs_property_int_step(instance); }
		}

		public unsafe ObsNumberType IntType
		{
			get { return (ObsNumberType)libobs.obs_property_int_type(instance); }
		}

		public unsafe double FloatMin
		{
			get { return libobs.obs_property_float_min(instance); }
		}

		public unsafe double FloatMax
		{
			get { return libobs.obs_property_float_max(instance); }
		}

		public unsafe double FloatStep
		{
			get { return libobs.obs_property_float_step(instance); }
		}

		public unsafe ObsNumberType FloatType
		{
			get { return (ObsNumberType)libobs.obs_property_float_type(instance); }
		}

		public unsafe ObsTextType TextType
		{
			get { return (ObsTextType)libobs.obs_proprety_text_type(instance); }
		}

		public unsafe ObsPathType PathType
		{
			get { return (ObsPathType)libobs.obs_property_path_type(instance); }
		}

		public unsafe FileFilter PathFilter
		{
			get { return FileFilter.QtFilter(libobs.obs_property_path_filter(instance)); }
		}

		public unsafe string PathDefault
		{
			get { return libobs.obs_property_path_default_path(instance); }
		}

		public unsafe ObsComboType ListType
		{
			get { return (ObsComboType)libobs.obs_property_list_type(instance); }
		}

		public unsafe ObsComboFormat ListFormat
		{
			get { return (ObsComboFormat)libobs.obs_property_list_format(instance); }
		}

		public unsafe int ListItemCount
		{
			get { return (int)libobs.obs_property_list_item_count(instance); }
		}

		public unsafe bool Modified(ObsData settings)
		{
			return libobs.obs_property_modified(instance, settings.GetPointer());
		}

		public unsafe bool ButtonClicked(IntPtr obj)
		{
			return libobs.obs_property_button_clicked(instance, obj);
		}

		public unsafe bool ButtonClicked(libobs.obs_property_clicked_t clicked, ObsProperties properties, IntPtr obj)
		{
			return clicked(properties.GetPointer(), instance, obj);
		}

		public unsafe string[] GetListItemNames()
		{
			int count = ListItemCount;
			List<string> names = new List<string>();

			for (int i = 0; i < count; i++)
				names.Add(libobs.obs_property_list_item_name(instance, (UIntPtr)i));

			return names.ToArray();
		}

		public unsafe object[] GetListItemValues()
		{
			int count = ListItemCount;
			List<object> values = new List<object>();
			ObsComboFormat format = ListFormat;

			for (int i = 0; i < count; i++)
			{
				if (format == ObsComboFormat.Int)
					values.Add(libobs.obs_property_list_item_int(instance, (UIntPtr)i));
				else if (format == ObsComboFormat.Float)
					values.Add(libobs.obs_property_list_item_float(instance, (UIntPtr)i));
				else if (format == ObsComboFormat.String)
					values.Add(libobs.obs_property_list_item_string(instance, (UIntPtr)i));
			}

			return values.ToArray();
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

	public enum ObsComboFormat : int
	{
		Invalid,
		Int,
		Float,
		String,
	};

	public enum ObsComboType : int
	{
		Invalid,
		Editable,
		List,
	};

	public enum ObsPathType : int
	{
		File,
		Directory
	};

	public enum ObsTextType : int
	{
		Default,
		Password,
		Multiline,
	};

	public enum ObsNumberType : int
	{
		Scroller,
		Slider
	};
}