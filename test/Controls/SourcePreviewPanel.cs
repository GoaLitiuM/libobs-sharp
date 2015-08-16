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
using OBS;
using OBS.Graphics;

namespace test.Controls
{
	class SourcePreviewPanel : DisplayPanel
	{
		private ObsSource source;

		public SourcePreviewPanel(ObsSource source)
		{
			this.source = source;

			if (source != null)
			{
				displayCreated += () => Display.AddDrawCallback(RenderSource);
				displayDestroyed += () => Display.RemoveDrawCallback(RenderSource);
			}
		}

		private void RenderSource(IntPtr data, uint cx, uint cy)
		{
			int newW = (int)cx;
			int newH = (int)cy;
			int sourceWidth = (int)source.Width;
			int sourceHeight = (int)source.Height;
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
			source.Render();

			GS.ProjectionPop();
			GS.ViewportPop();

			GS.LoadVertexBuffer(null);
		}
	}
}
