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

namespace OBS
{
	using uint32_t = UInt32;
	using uint64_t = UInt64;

	public static partial class libobs
	{
		private const int MAX_AV_PLANES = 8;

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct resample_info
		{
			private uint32_t samples_per_sec;
			private audio_format format;
			private speaker_layout speakers;
		};

		[StructLayoutAttribute(LayoutKind.Sequential)]
		public unsafe struct audio_output_info
		{
			public string name;

			public uint32_t samples_per_sec;
			public audio_format format;
			public speaker_layout speakers;
			public uint64_t buffer_ms;
		};

		public enum video_format : int
		{
			VIDEO_FORMAT_NONE,

			//planar 420 format
			VIDEO_FORMAT_I420, //three-plane
			VIDEO_FORMAT_NV12, //two-plane, luma and packed chroma

			//packed 422 formats
			VIDEO_FORMAT_YVYU,
			VIDEO_FORMAT_YUY2, //YUYV
			VIDEO_FORMAT_UYVY,

			//packed uncompressed formats
			VIDEO_FORMAT_RGBA,
			VIDEO_FORMAT_BGRA,
			VIDEO_FORMAT_BGRX,
		};

		public enum audio_format : int
		{
			AUDIO_FORMAT_UNKNOWN,

			AUDIO_FORMAT_U8BIT,
			AUDIO_FORMAT_16BIT,
			AUDIO_FORMAT_32BIT,
			AUDIO_FORMAT_FLOAT,

			AUDIO_FORMAT_U8BIT_PLANAR,
			AUDIO_FORMAT_16BIT_PLANAR,
			AUDIO_FORMAT_32BIT_PLANAR,
			AUDIO_FORMAT_FLOAT_PLANAR,
		};

		public enum speaker_layout : int
		{
			SPEAKERS_UNKNOWN,
			SPEAKERS_MONO,
			SPEAKERS_STEREO,
			SPEAKERS_2POINT1,
			SPEAKERS_QUAD,
			SPEAKERS_4POINT1,
			SPEAKERS_5POINT1,
			SPEAKERS_5POINT1_SURROUND,
			SPEAKERS_7POINT1,
			SPEAKERS_7POINT1_SURROUND,
			SPEAKERS_SURROUND,
		};

        public enum video_colorspace : int
        {
            VIDEO_CS_DEFAULT,
            VIDEO_CS_601,
            VIDEO_CS_709,
        };

        public enum video_range_type : int
        {
            VIDEO_RANGE_DEFAULT,
            VIDEO_RANGE_PARTIAL,
            VIDEO_RANGE_FULL
        };

	}
}