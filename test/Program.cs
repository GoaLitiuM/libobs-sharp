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
using System.Windows.Forms;
using System.Diagnostics;
using OBS;

namespace test
{
	internal static class Program
	{
		// scene rendering resolution
		public static readonly int MainWidth = 1280;
		public static readonly int MainHeight = 720;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			//System.Diagnostics.Debug.Assert(Type.GetType("Mono.Runtime") != null, "Mono VM not detected.");

			InitObs();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new TestForm());
		}

		private static void InitObs()
		{
			try
			{
				Debug.WriteLine("libobs version: " + Obs.GetVersion());

				// forward OBS logging messages to debugger
				Obs.SetLogHandler((lvl, msg, p) =>
				{
					Debug.WriteLine(msg);
				});

				if (!Obs.Startup("en-US"))
					throw new ApplicationException("Startup failed.");

				// initialize video
				libobs.obs_video_info ovi = new libobs.obs_video_info
				{
					adapter = 0,
					base_width = (uint)MainWidth,
					base_height = (uint)MainHeight,
					fps_num = 30000,
					fps_den = 1001,
					graphics_module = "libobs-d3d11",
					output_format = libobs.video_format.VIDEO_FORMAT_RGBA,
					output_width = (uint)MainWidth,
					output_height = (uint)MainHeight,
				};

				if (!Obs.ResetVideo(ovi))
					throw new ApplicationException("ResetVideo failed.");

				// initialize audio
				libobs.obs_audio_info avi = new libobs.obs_audio_info
				{
					samples_per_sec = 44100,
					speakers = libobs.speaker_layout.SPEAKERS_STEREO,
					//buffer_ms = 1000
				};

				if (!Obs.ResetAudio(avi))
					throw new ApplicationException("ResetAudio failed.");

				// load all plugins and modules
				Obs.LoadAllModules();
			}
			catch (BadImageFormatException exp)
			{
				MessageBox.Show("Platform target mismatch: "
								+ (Environment.Is64BitProcess
									? "Loading 32-bit OBS with 64-bit executable is not supported."
									: "Loading 64-bit OBS with 32-bit executable is not supported.")
								+ "\n\n" + exp.Message,
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}
		}
	}
}