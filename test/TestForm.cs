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

using OBS;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace test
{
	public partial class TestForm : Form
	{
		public TestForm()
		{
			InitializeComponent();
		}

		~TestForm()
		{
			Obs.Shutdown();
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
			try
			{
				Rectangle rc = new Rectangle(0, 0, Width, Height);

				libobs.obs_video_info ovi = new libobs.obs_video_info
				{
					adapter = 0,
					base_width = (uint)rc.Right,
					base_height = (uint)rc.Bottom,
					fps_num = 30000,
					fps_den = 1001,
					graphics_module = "libobs-d3d11",
					window_width = (uint)rc.Right,
					window_height = (uint)rc.Bottom,
					output_format = libobs.video_format.VIDEO_FORMAT_RGBA,
					output_width = (uint)rc.Right,
					output_height = (uint)rc.Bottom,
					window = new libobs.gs_window()
					{
						hwnd = Handle
					}
				};

				if (!Obs.Startup("en-US"))
				{
					MessageBox.Show("Startup failed.", "Error", MessageBoxButtons.OK);
					Close();
					return;
				}

				if (Obs.ResetVideo(ovi) != 0)
				{
					MessageBox.Show("ResetVideo failed.", "Error", MessageBoxButtons.OK);
					Close();
					return;
				}

				Obs.LoadAllModules();

				ObsSource source = new ObsSource(ObsSourceType.Input, "random", "some randon source");
				ObsSource filter = new ObsSource(ObsSourceType.Filter, "test_filter", "a nice green filter");

				source.AddFilter(filter);

				ObsScene scene = new ObsScene("test scene");

				libobs.vec2 scale = new libobs.vec2(20.0f, 20.0f);
				ObsSceneItem item = scene.Add(source);
				item.Scale(scale);

				Obs.SetOutputSource(0, scene.GetSource());

				libobs.obs_add_draw_callback(new libobs.draw_callback(RenderWindow), IntPtr.Zero);
			}
			catch (Exception exp)
			{
				MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OK);
				Close();
			}
		}

		private void RenderWindow(IntPtr data, UInt32 cx, UInt32 cy)
		{
			Obs.RenderMainView();
		}
	}
}