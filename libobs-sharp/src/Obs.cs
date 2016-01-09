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

using OBS.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OBS
{
	public static partial class Obs
	{
		public static bool Startup(string locale, string moduleConfigPath = null)
		{
			return libobs.obs_startup(locale, moduleConfigPath, IntPtr.Zero);
		}

		public static void Shutdown()
		{
			libobs.obs_shutdown();
		}

		public static void LoadAllModules()
		{
			libobs.obs_load_all_modules();
		}

		public static bool ResetVideo(libobs.obs_video_info ovi)
		{
			return (libobs.obs_reset_video(ref ovi) == (int)ObsVideoErrorCode.Success);
		}

		public static bool ResetAudio(libobs.obs_audio_info ai)
		{
			return libobs.obs_reset_audio(ref ai);
		}

		public static libobs.obs_video_info GetVideoInfo()
		{
			libobs.obs_video_info ovi = new libobs.obs_video_info();
			bool ret = libobs.obs_get_video_info(ref ovi);

			if (!ret)
				throw new ApplicationException("obs_get_video_info failed");

			return ovi;
		}

		public static libobs.obs_audio_info GetAudioInfo()
		{
			libobs.obs_audio_info ai = new libobs.obs_audio_info();
			bool ret = libobs.obs_get_audio_info(ref ai);

			if (!ret)
				throw new ApplicationException("obs_get_audio_info failed");

			return ai;
		}

		public static bool IsInitialized()
		{
			return libobs.obs_initialized();
		}

		public static Obs.Version GetVersion()
		{
			return new Obs.Version { Full = libobs.obs_get_version() };
		}

		/// <summary> Sets primary output to source for a channel. </summary>
		public static unsafe void SetOutputSource(UInt32 channel, ObsSource source)
		{
			libobs.obs_set_output_source(channel, (IntPtr)source.GetPointer());
		}

		/// <summary> Sets primary output to scene for a channel. </summary>
		public static unsafe void SetOutputScene(UInt32 channel, ObsScene scene)
		{
			using (ObsSource source = scene.GetSource())
				SetOutputSource(channel, source);
		}

		/// <summary> Gets output source for a channel. </summary>
		/// <returns> null if current output is not set. </returns>
		public static unsafe ObsSource GetOutputSource(UInt32 channel)
		{
			IntPtr ptr = libobs.obs_get_output_source(channel);

			// obs_view_get_source increments the reference counter already
			// so let's undo that and takeover the handling of the ref counting
			libobs.obs_source_release(ptr);

			return new ObsSource(ptr);
		}

		/// <summary> Gets output scene for a channel. </summary>
		/// <returns> null if current output is not set or is not a scene. </returns>
		public static unsafe ObsScene GetOutputScene(UInt32 channel)
		{
			using (ObsSource source = GetOutputSource(channel))
				return source.GetScene();
		}

		public static void RenderMainView()
		{
			libobs.obs_render_main_view();
		}

		public static string GetSourceTypeDisplayName(ObsSourceType type, string id)
		{
			return libobs.obs_source_get_display_name((libobs.obs_source_type)type, id);
		}

		public static string[] GetSourceInputTypes()
		{
			uint idx = 0;
			string id = null;
			List<string> idList = new List<string>();

			while (libobs.obs_enum_input_types((UIntPtr)idx++, out id))
				if (id != "scene")
					idList.Add(id);

			return idList.ToArray();
		}

		public static string[] GetSourceFilterTypes()
		{
			uint idx = 0;
			string id = null;
			List<string> idList = new List<string>();

			while (libobs.obs_enum_filter_types((UIntPtr)idx++, out id))
				idList.Add(id);

			return idList.ToArray();
		}

		public static string[] GetSourceTransitionTypes()
		{
			uint idx = 0;
			string id = null;
			List<string> idList = new List<string>();

			while (libobs.obs_enum_transition_types((UIntPtr)idx++, out id))
				idList.Add(id);

			return idList.ToArray();
		}

		public static unsafe GSEffect GetBaseEffect(ObsBaseEffect effect)
		{
			IntPtr ptr = libobs.obs_get_base_effect((libobs.obs_base_effect)effect);
			if (ptr == IntPtr.Zero)
				return null;

			GSEffect baseEffect = new GSEffect(ptr);
			return baseEffect;
		}
		
		[StructLayout(LayoutKind.Explicit)]
		public struct Version
		{
			[FieldOffset(0)]
			public UInt32 Full;

			[FieldOffset(3)]
			public byte Major;

			[FieldOffset(2)]
			public byte Minor;

			[FieldOffset(0)]
			public UInt16 Patch;

			public override string ToString()
			{
				return Major.ToString() + "." + Minor.ToString() + "." + Patch.ToString();
			}
		}
	}

	[Flags]
	public enum ObsAlignment : uint
	{
		Center = 0,
		Left = (1 << 0),
		Right = (1 << 1),
		Top = (1 << 2),
		Bottom = (1 << 3),
	};

	public enum ObsModuleErrorCode : int
	{
		Success = 0,
		Error = -1,
		FileNotFound = -2,
		MissingExports = -3,
		IncompatibleVersion = -4,
	};

	public enum ObsOutputErrorCode : int
	{
		Success = 0,
		BadPath = -1,
		ConnectFailed = -2,
		InvalidStream = -3,
		Error = -4,
		Disconnected = -5,
	};

	public enum ObsVideoErrorCode : int
	{
		Success = 0,
		Fail = -1,
		NotSupported = -2,
		InvalidParam = -3,
		CurrentlyActive = -4,
		ModuleNotFound = -5,
	};

	public enum ObsBaseEffect : int
	{
		Default,
		DefaultRect,
		Opaque,
		Solid,
		Bicubic,
		Lanczos,
		BilinearLowres,
	};
}