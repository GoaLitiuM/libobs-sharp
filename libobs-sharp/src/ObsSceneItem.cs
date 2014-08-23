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

        unsafe ~ObsSceneItem()
        {
            libobs.obs_sceneitem_release((IntPtr)instance);
        }

        public unsafe void Scale(libobs.vec2 scale)
        {
            libobs.obs_sceneitem_set_scale((IntPtr)instance, out scale);
        }
    }
}
