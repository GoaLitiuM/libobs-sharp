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
using System.Text;

namespace OBS
{
	public class ObsScene
	{
        internal IntPtr instance;    //pointer to unmanaged object

		public unsafe ObsScene(string name)
		{
			instance = libobs.obs_scene_create(name);

			if (instance == null)
				throw new ApplicationException("obs_scene_create failed");

			libobs.obs_scene_addref(instance);
		}

		~ObsScene()
		{
			Release();
		}

		public unsafe void Release()
		{
            if (instance == IntPtr.Zero)
				return;

            IntPtr source = libobs.obs_scene_get_source(instance);
            libobs.obs_source_release(source);
			//ObsSource source = GetSource();
			//source.Release();

			libobs.obs_scene_release(instance);
            instance = IntPtr.Zero;
		}

        public unsafe IntPtr GetPointer()
		{
			return instance;
		}


		public unsafe String Name
		{
			get
			{
				return libobs.obs_source_get_name(libobs.obs_scene_get_source(instance));
			}
		}

		public unsafe ObsSource GetSource()
		{
            IntPtr source = libobs.obs_scene_get_source(instance);
			return new ObsSource(source);
		}

		public unsafe ObsSceneItem Add(ObsSource source)
		{
            IntPtr sceneItem = libobs.obs_scene_add(instance, source.GetPointer());
			return new ObsSceneItem(sceneItem);
		}

		
		public unsafe void EnumItems(libobs.sceneitem_enum_callback callback, IntPtr param)
		{
			libobs.obs_scene_enum_items(instance, callback, param);
		}


	}
}