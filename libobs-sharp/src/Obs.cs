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

namespace OBS
{
	public static class Obs
	{
		public static bool Startup(string locale)
		{
			return libobs.obs_startup(locale);
		}

		public static void Shutdown()
		{
			libobs.obs_shutdown();
		}

		public static void LoadAllModules()
		{
			libobs.obs_load_all_modules();
		}

		public static int ResetVideo(libobs.obs_video_info ovi)
		{
			return libobs.obs_reset_video(ref ovi);
		}

		public static unsafe void SetOutputSource(UInt32 channel, ObsSource source)
		{
			libobs.obs_set_output_source(channel, (IntPtr)source.GetPointer());
		}

		public static void RenderMainView()
		{
			libobs.obs_render_main_view();
		}

		public static void ResizeMainView(int width, int height)
		{
			libobs.obs_resize((UInt32)width, (UInt32)height);
		}


        public static string GetSourceTypeDisplayName(ObsSourceType type, string id)
        {
            return libobs.obs_source_get_display_name((libobs.obs_source_type)type, id);
        }

        public static string[] GetSourceInputTypes()
        {
            int idx = 0;
            string id = null;
            List<string> idList = new List<string>();

            while (libobs.obs_enum_input_types(idx++, out id))
                idList.Add(id);

            return idList.ToArray();
        }

        public static string[] GetSourceFilterTypes()
        {
            int idx = 0;
            string id = null;
            List<string> idList = new List<string>();

            while (libobs.obs_enum_filter_types(idx++, out id))
                idList.Add(id);

            return idList.ToArray();
        }

        public static string[] GetSourceTransitionTypes()
        {
            int idx = 0;
            string id = null;
            List<string> idList = new List<string>();

            while (libobs.obs_enum_transition_types(idx++, out id))
                idList.Add(id);

            return idList.ToArray();
        }
	}
}