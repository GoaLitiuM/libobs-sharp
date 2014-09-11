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
	public class ObsSceneItem
	{
		internal unsafe libobs.obs_scene_item* instance;    //pointer to unmanaged object

		public unsafe ObsSceneItem(libobs.obs_scene_item* instance)
		{
			this.instance = instance;
			libobs.obs_sceneitem_addref((IntPtr)instance);
		}

		~ObsSceneItem()
		{
			Release();
		}

		public string Name()
		{
			return GetSource().Name;
		}

		public unsafe void Release()
		{
			if (instance == null)
				return;

			ObsSource source = GetSource();
			source.Release();

			libobs.obs_sceneitem_remove((IntPtr)instance);	//remove also removes it from sources
			instance = null;
		}

		public unsafe libobs.obs_scene_item* GetPointer()
		{
			return instance;
		}

		public unsafe ObsSource GetSource()
		{
			libobs.obs_source* source = (libobs.obs_source*)libobs.obs_sceneitem_get_source((IntPtr)instance);
			return new ObsSource(source);
		}


		public unsafe bool Selected
		{
			get
			{
				return libobs.obs_sceneitem_selected((IntPtr)instance);
			}
			set
			{
				libobs.obs_sceneitem_select((IntPtr)instance, value);
			}
		}

		public unsafe libobs.vec2 GetPosition()
		{
			libobs.vec2 position = new libobs.vec2();
			libobs.obs_sceneitem_get_pos((IntPtr)instance, out position);
			return position;
		}

		public unsafe float GetRotation()
		{
			return libobs.obs_sceneitem_get_rot((IntPtr)instance);
		}

		public unsafe libobs.vec2 GetScale()
		{
			libobs.vec2 scale = new libobs.vec2();
			libobs.obs_sceneitem_get_scale((IntPtr)instance, out scale);
			return scale;
		}

		public unsafe ObsAlignment GetAlignment()
		{
			return (ObsAlignment)libobs.obs_sceneitem_get_alignment((IntPtr)instance);
		}


		public unsafe void SetPosition(libobs.vec2 position)
		{
			libobs.obs_sceneitem_set_pos((IntPtr)instance, out position);
		}

		public unsafe void SetRotation(float rotation)
		{
			libobs.obs_sceneitem_set_rot((IntPtr)instance, rotation);
		}

		/// <summary>
		/// Sets scale of layer
		/// </summary>
		/// <param name="scale">1f = original size</param>
		public unsafe void SetScale(libobs.vec2 scale)
		{
			libobs.obs_sceneitem_set_scale((IntPtr)instance, out scale);
		}

		public unsafe void SetAlignment(ObsAlignment alignment)
		{
			libobs.obs_sceneitem_set_alignment((IntPtr) instance, (uint)alignment);
		}
	}
}
