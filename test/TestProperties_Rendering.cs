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

using OBS;
using OBS.Graphics;
using System;
using System.Windows.Forms;

namespace test
{
	public partial class TestProperties : Form
	{
		private ObsDisplay _display;
		private libobs.draw_callback _RenderPreview;

		private void InitPreview(uint width, uint height, IntPtr handle)
		{
			//assign callbacks
			_RenderPreview = RenderPreview;

			libobs.gs_init_data initData = new libobs.gs_init_data
			{
				cx = width,
				cy = height,
				format = libobs.gs_color_format.GS_RGBA,
				window = new libobs.gs_window()
				{
					hwnd = previewPanel.Handle,
				},
			};

			_display = new ObsDisplay(initData);

			if (_display != null)
				Obs.AddDisplayDrawCallback(_display, _RenderPreview, this.Handle);
		}

		private void ClosePreview()
		{
			Obs.RemoveDisplayDrawCallback(_display, _RenderPreview, this.Handle);
			_display.Dispose();
		}

		private void ResizePreview(uint width, uint height)
		{
			Obs.DisplayResize(_display, width, height);
		}

		private static void RenderPreview(IntPtr data, UInt32 cx, UInt32 cy)
		{
			TestProperties window = Control.FromHandle(data) as TestProperties;

			if (window == null) return;

			int newW = (int)cx;
			int newH = (int)cy;
			int sourceWidth = (int)window.Source.Width;
			int sourceHeight = (int)window.Source.Height;
			float previewAspect = (float)cx / cy;
			float sourceAspect = (float)sourceWidth / sourceHeight;

			//calculate new width and height for source to make it fit inside the preview area
			if (previewAspect > sourceAspect)
				newW = (int)(cy * sourceAspect);
			else
				newH = (int)(cx / sourceAspect);

			int centerX = ((int)cx - newW) / 2;
			int centerY = ((int)cy - newH) / 2;

			GS.ViewportPush();
			GS.ProjectionPush();

			//setup orthographic projection of the source
			GS.Ortho(0.0f, sourceWidth, 0.0f, sourceHeight, -100.0f, 100.0f);
			GS.SetViewport(centerX, centerY, newW, newH);

			//render source content
			window.Source.Render();

			GS.ProjectionPop();
			GS.ViewportPop();

			GS.LoadVertexBuffer(null);
		}
	}
}