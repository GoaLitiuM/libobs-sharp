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
    public class ObsData
    {
        internal unsafe libobs.obs_data* instance;    //pointer to unmanaged object

		public unsafe ObsData(IntPtr ptr)
		{
            instance = (libobs.obs_data*)ptr;

            libobs.obs_data_addref((IntPtr)instance);
		}

        ~ObsData()
		{
			Release();
		}

		public unsafe void Release()
		{
			if (instance == null)
				return;

            libobs.obs_data_release((IntPtr)instance);

			instance = null;
		}

        public unsafe libobs.obs_data* GetPointer()
		{
			return instance;
		}


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

        public unsafe string GetDefaultString(string name)
        {
            return libobs.obs_data_get_default_string((IntPtr)instance, name);
        }

        public unsafe long GetDefaultInt(string name)
        {
            return libobs.obs_data_get_default_int((IntPtr)instance, name);
        }

        public unsafe double GetDefaultDouble(string name)
        {
            return libobs.obs_data_get_default_double((IntPtr)instance, name);
        }

        public unsafe bool GetDefaultBool(string name)
        {
            return libobs.obs_data_get_default_bool((IntPtr)instance, name);
        }

        public unsafe IntPtr GetDefaultObject(string name)
        {
            return libobs.obs_data_get_default_obj((IntPtr)instance, name);
        }

        public unsafe IntPtr GetDefaultArray(string name)
        {
            return libobs.obs_data_get_default_array((IntPtr)instance, name);
        }

        public unsafe string GetAutoselectString(string name)
        {
            return libobs.obs_data_get_autoselect_string((IntPtr)instance, name);
        }

        public unsafe long GetAutoselectInt(string name)
        {
            return libobs.obs_data_get_autoselect_int((IntPtr)instance, name);
        }

        public unsafe double GetAutoselectDouble(string name)
        {
            return libobs.obs_data_get_autoselect_double((IntPtr)instance, name);
        }

        public unsafe bool GetAutoselectBool(string name)
        {
            return libobs.obs_data_get_autoselect_bool((IntPtr)instance, name);
        }

        public unsafe IntPtr GetAutoselectObject(string name)
        {
            return libobs.obs_data_get_autoselect_obj((IntPtr)instance, name);
        }

        public unsafe IntPtr GetAutoselectArray(string name)
        {
            return libobs.obs_data_get_autoselect_array((IntPtr)instance, name);
        }

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
