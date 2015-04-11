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

		private void InitPreview(int width, int height, IntPtr handle)
		{
			//assign callbacks
			_RenderPreview = RenderPreview;

			libobs.gs_init_data initData = new libobs.gs_init_data
			{
				cx = (uint)width,
				cy = (uint)height,
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

		private static void RenderPreview(IntPtr data, UInt32 cx, UInt32 cy)
		{
			TestProperties window = Control.FromHandle(data) as TestProperties;

			if (window == null) return;

			//TODO: proper source size handling
			int panelWidth = window.previewPanel.Width;
			int panelHeight = window.previewPanel.Height;

			int previewCX = panelWidth;
			int previewCY = panelHeight;

			int previewX = 0;
			int previewY = 0;

			GS.ViewportPush();
			GS.ProjectionPush();

			//setup orthographic projection of the source
			GS.Ortho(0.0f, previewCX, 0.0f, previewCY, -100.0f, 100.0f);
			GS.SetViewport(previewX, previewY, previewCX, previewCY);

			//render source content
			window.Source.Render();

			GS.ProjectionPop();
			GS.ViewportPop();

			GS.LoadVertexBuffer(null);
		}
	}
}