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

namespace OBS
{
	public class ObsData
	{
		internal IntPtr instance;    //pointer to unmanaged object

		public ObsData(IntPtr ptr)
		{
			instance = ptr;

			libobs.obs_data_addref(instance);
		}

		~ObsData()
		{
			Release();
		}

		public void Release()
		{
			if (instance == IntPtr.Zero)
				return;

			libobs.obs_data_release(instance);

			instance = IntPtr.Zero;
		}

		public IntPtr GetPointer()
		{
			return instance;
		}

		//Getters

		public unsafe string GetString(string name)
		{
			return libobs.obs_data_get_string((IntPtr)instance, name);
		}

		public unsafe long GetInt(string name)
		{
			return libobs.obs_data_get_int((IntPtr)instance, name);
		}

		public unsafe double GetDouble(string name)
		{
			return libobs.obs_data_get_double((IntPtr)instance, name);
		}

		public unsafe bool GetBool(string name)
		{
			return libobs.obs_data_get_bool((IntPtr)instance, name);
		}

		public unsafe IntPtr GetObject(string name)
		{
			return libobs.obs_data_get_obj((IntPtr)instance, name);
		}

		public unsafe IntPtr GetArray(string name)
		{
			return libobs.obs_data_get_array((IntPtr)instance, name);
		}

		public unsafe string GetStringDefault(string name)
		{
			return libobs.obs_data_get_default_string((IntPtr)instance, name);
		}

		public unsafe long GetIntDefault(string name)
		{
			return libobs.obs_data_get_default_int((IntPtr)instance, name);
		}

		public unsafe double GetDoubleDefault(string name)
		{
			return libobs.obs_data_get_default_double((IntPtr)instance, name);
		}

		public unsafe bool GetBoolDefault(string name)
		{
			return libobs.obs_data_get_default_bool((IntPtr)instance, name);
		}

		public unsafe IntPtr GetObjectDefault(string name)
		{
			return libobs.obs_data_get_default_obj((IntPtr)instance, name);
		}

		public unsafe IntPtr GetArrayDefault(string name)
		{
			return libobs.obs_data_get_default_array((IntPtr)instance, name);
		}

		public unsafe string GetStringAutoselect(string name)
		{
			return libobs.obs_data_get_autoselect_string((IntPtr)instance, name);
		}

		public unsafe long GetIntAutoselect(string name)
		{
			return libobs.obs_data_get_autoselect_int((IntPtr)instance, name);
		}

		public unsafe double GetDoubleAutoselect(string name)
		{
			return libobs.obs_data_get_autoselect_double((IntPtr)instance, name);
		}

		public unsafe bool GetBoolAutoselect(string name)
		{
			return libobs.obs_data_get_autoselect_bool((IntPtr)instance, name);
		}

		public unsafe IntPtr GetObjectAutoselect(string name)
		{
			return libobs.obs_data_get_autoselect_obj((IntPtr)instance, name);
		}

		public unsafe IntPtr GetArrayAutoselect(string name)
		{
			return libobs.obs_data_get_autoselect_array((IntPtr)instance, name);
		}

		//Setters

		public unsafe void SetString(string name, string val)
		{
			libobs.obs_data_set_string((IntPtr)instance, name, val);
		}

		public unsafe void SetInt(string name, int val)
		{
			libobs.obs_data_set_int((IntPtr)instance, name, val);
		}

		public unsafe void SetDouble(string name, double val)
		{
			libobs.obs_data_set_double((IntPtr)instance, name, val);
		}

		public unsafe void SetBool(string name, bool val)
		{
			libobs.obs_data_set_bool((IntPtr)instance, name, val);
		}

		public unsafe void SetObject(string name, IntPtr val)
		{
			libobs.obs_data_set_obj((IntPtr)instance, name, val);
		}

		public unsafe void SetArray(string name, IntPtr val)
		{
			libobs.obs_data_set_array((IntPtr)instance, name, val);
		}

		public unsafe void SetStringDefault(string name, string val)
		{
			libobs.obs_data_set_default_string((IntPtr)instance, name, val);
		}

		public unsafe void SetIntDefault(string name, int val)
		{
			libobs.obs_data_set_default_int((IntPtr)instance, name, val);
		}

		public unsafe void SetDoubleDefault(string name, double val)
		{
			libobs.obs_data_set_default_double((IntPtr)instance, name, val);
		}

		public unsafe void SetBoolDefault(string name, bool val)
		{
			libobs.obs_data_set_default_bool((IntPtr)instance, name, val);
		}

		public unsafe void SetObjectDefault(string name, IntPtr val)
		{
			libobs.obs_data_set_default_obj((IntPtr)instance, name, val);
		}

		public unsafe void SetStringAutoselect(string name, string val)
		{
			libobs.obs_data_set_autoselect_string((IntPtr)instance, name, val);
		}

		public unsafe void SetIntAutoselect(string name, int val)
		{
			libobs.obs_data_set_autoselect_int((IntPtr)instance, name, val);
		}

		public unsafe void SetDoubleAutoselect(string name, double val)
		{
			libobs.obs_data_set_autoselect_double((IntPtr)instance, name, val);
		}

		public unsafe void SetBoolAutoselect(string name, bool val)
		{
			libobs.obs_data_set_autoselect_bool((IntPtr)instance, name, val);
		}

		public unsafe void SetObjectAutoselect(string name, IntPtr val)
		{
			libobs.obs_data_set_autoselect_obj((IntPtr)instance, name, val);
		}

		//Checkers

		public unsafe bool HasUserValue(string name)
		{
			return libobs.obs_data_has_user_value((IntPtr)instance, name);
		}

		public unsafe bool HasDefaultValue(string name)
		{
			return libobs.obs_data_has_user_value((IntPtr)instance, name);
		}

		public unsafe bool HasAutoselectValue(string name)
		{
			return libobs.obs_data_has_user_value((IntPtr)instance, name);
		}
	}
}