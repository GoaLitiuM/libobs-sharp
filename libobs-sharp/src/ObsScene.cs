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
		internal unsafe libobs.obs_scene* instance;    //pointer to unmanaged object

		public unsafe ObsScene(string name)
		{
			instance = (libobs.obs_scene*)libobs.obs_scene_create(name);

			if (instance == null)
				throw new ApplicationException("obs_scene_create failed");

			libobs.obs_scene_addref((IntPtr)instance);
		}

		~ObsScene()
		{
			Release();
		}

		public unsafe void Release()
		{
			if (instance == null)
				return;

			ObsSource source = GetSource();
			source.Release();

			libobs.obs_scene_release((IntPtr)instance);
			instance = null;
		}

		public unsafe libobs.obs_scene* GetPointer()
		{
			return instance;
		}


		public unsafe String Name
		{
			get
			{
				return libobs.obs_source_get_name((IntPtr)(libobs.obs_source*)libobs.obs_scene_get_source((IntPtr)instance));
			}
		}

		public unsafe ObsSource GetSource()
		{
			libobs.obs_source* source = (libobs.obs_source*)libobs.obs_scene_get_source((IntPtr)instance);
			return new ObsSource(source);
		}

		public unsafe ObsSceneItem Add(ObsSource source)
		{
			libobs.obs_scene_item* sceneItem = (libobs.obs_scene_item*)libobs.obs_scene_add((IntPtr)instance, (IntPtr)source.GetPointer());
			return new ObsSceneItem(sceneItem);
		}

		
		public unsafe void EnumItems(libobs.sceneitem_enum_callback callback, IntPtr param)
		{
			libobs.obs_scene_enum_items((IntPtr)instance, callback, param);
		}


	}
}