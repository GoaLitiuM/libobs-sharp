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

namespace OBS
{
    public static class Obs
    {
        public static unsafe void SetOutputSource(UInt32 channel, ObsSource source)
        {
            libobs.obs_set_output_source(channel, (IntPtr)source.GetPointer());
        }

        public static unsafe bool Startup(string locale)
        {
            return libobs.obs_startup(locale);
        }

        public static unsafe int ResetVideo(libobs.obs_video_info ovi)
        {
            return libobs.obs_reset_video(ref ovi);
        }

        public static unsafe void LoadAllModules()
        {
            libobs.obs_load_all_modules();
        }

        public static unsafe void Shutdown()
        {
            libobs.obs_shutdown();
        }

        public static unsafe void RenderMainView()
        {
            libobs.obs_render_main_view();
        }
    }
}