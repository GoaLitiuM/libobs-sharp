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
	public partial class TestForm : Form
	{
		private GSVertexBuffer _boxPrimitive;
		private GSVertexBuffer _circlePrimitive;
		public float PreviewScale = 1;

		private const float HANDLE_RADIUS = 5.0f;
		private const float HANDLE_SEL_RADIUS = HANDLE_RADIUS * 1.5f;
		private const float CLAMP_DISTANCE = 10.0f;

		private void InitPrimitives()
		{
			GS.EnterGraphics();

			//box from vertices
			GS.RenderStart(true);
			GS.Vertex2f(0.0f, 0.0f);
			GS.Vertex2f(0.0f, 1.0f);
			GS.Vertex2f(1.0f, 1.0f);
			GS.Vertex2f(1.0f, 0.0f);
			GS.Vertex2f(0.0f, 0.0f);
			_boxPrimitive = GS.RenderSave();

			//circle from vertices
			GS.RenderStart(true);
			for (int i = 0; i <= 360; i += 360 / 20)
			{
				double pos = Math.PI * (double)i / 180.0;
				GS.Vertex2f((float)Math.Cos(pos), (float)Math.Sin(pos));
			}
			_circlePrimitive = GS.RenderSave();

			GS.LeaveGraphics();
		}

		private static void RenderMain(IntPtr data, UInt32 cx, UInt32 cy)
		{
			TestForm window = Control.FromHandle(data) as TestForm;
			if (window == null)
				return;

			libobs.obs_video_info ovi = Obs.GetVideoInfo();

			int newW = (int)cx;
			int newH = (int)cy;
			uint baseWidth = ovi.base_width;
			uint baseHeight = ovi.base_height;
			float previewAspect = (float)cx / cy;
			float baseAspect = (float)baseWidth / baseHeight;

			//calculate new width and height for source to make it fit inside the preview area
			if (previewAspect > baseAspect)
				newW = (int)(cy * baseAspect);
			else
				newH = (int)(cx / baseAspect);

			int centerX = ((int)cx - newW) / 2;
			int centerY = ((int)cy - newH) / 2;

			window.PreviewScale = (float)newW / newH;

			GS.ViewportPush();
			GS.ProjectionPush();

			//setup orthographic projection of the whole scene to be presented on viewport
			GS.Ortho(0.0f, baseWidth, 0.0f, baseHeight, -100.0f, 100.0f);
			GS.SetViewport(centerX, centerY, newW, newH);

			//draw scene background
			window.ClearBackground(baseWidth, baseHeight);

			//render all visible sources
			Obs.RenderMainView();

			//calculate bottom-right corner on scene space
			int right = newW - centerX;
			int bottom = newH - centerY;

			//ortho for the outer area which would normally not appear on scene
			GS.Ortho(-centerX, right, -centerY, bottom, -100.0f, 100.0f);
			GS.ResetViewport();

			//render editing overlays
			window.RenderSceneEditing(data);

			GS.ProjectionPop();
			GS.ViewportPop();

			GS.LoadVertexBuffer(null);
		}

		private void ClearBackground(UInt32 cx, UInt32 cy)
		{
			GSEffect solid = Obs.GetSolidEffect();
			solid.SetParameterValue("color", new libobs.vec4(0.0f, 0.0f, 0.0f, 1.0f));

			GSEffectTechnique tech = solid.GetTechnique("Solid");

			GS.TechniqueBegin(tech);
			GS.TechniqueBeginPass(tech, 0);

			GS.MatrixPush();
			GS.MatrixIdentity();
			GS.MatrixScale3f((float)cx, (float)cy, 1.0f);

			GS.LoadVertexBuffer(_boxPrimitive);

			//draw solid black color over the scene
			GS.Draw(GSDrawMode.TrisStrip, 0, 0);

			GS.MatrixPop();

			GS.TechniqueEndPass(tech);
			GS.TechniqueEnd(tech);

			GS.LoadVertexBuffer(null);
		}

		private void RenderSceneEditing(IntPtr data)
		{
			GSEffect solid = Obs.GetSolidEffect();
			solid.SetParameterValue("color", new libobs.vec4(1.0f, 1.0f, 1.0f, 0.75f));

			GSEffectTechnique tech = solid.GetTechnique("Solid");

			GS.TechniqueBegin(tech);
			GS.TechniqueBeginPass(tech, 0);

			// enum every sceneitem in scene
			if (_scenes[_renderSceneIndex] != null)
				_scenes[_renderSceneIndex].EnumItems(_enumSceneItem, data);

			GS.TechniqueEndPass(tech);
			GS.TechniqueEnd(tech);

			GS.LoadVertexBuffer(null);
		}

		private bool EnumSceneItem(IntPtr scene, IntPtr item, IntPtr data)
		{
			if (!libobs.obs_sceneitem_selected(item))
				return true;

			TestForm window = Control.FromHandle(data) as TestForm;

			GS.LoadVertexBuffer(_circlePrimitive);

			libobs.matrix4 boxTransform;
			libobs.obs_sceneitem_get_box_transform(item, out boxTransform);

			//render the tiny circles on corners
			DrawPrimitive(0.0f, 0.0f, boxTransform, PreviewScale);
			DrawPrimitive(0.0f, 1.0f, boxTransform, PreviewScale);
			DrawPrimitive(1.0f, 1.0f, boxTransform, PreviewScale);
			DrawPrimitive(1.0f, 0.0f, boxTransform, PreviewScale);

			//render the main selection rectangle

			GS.LoadVertexBuffer(_boxPrimitive);

			GS.MatrixPush();
			GS.MatrixScale3f(PreviewScale, PreviewScale, 1.0f);
			GS.MatrixMul(boxTransform);
			GS.Draw(GSDrawMode.LineStrip, 0, 0);
			GS.MatrixPop();

			return true;
		}

		private static void DrawPrimitive(float x, float y, libobs.matrix4 matrix, float previewScale)
		{
			libobs.vec3 pos = new libobs.vec3(x, y, 0.0f);
			libobs.vec3_transform(out pos, out pos, out matrix);

			pos.x *= previewScale;
			pos.y *= previewScale;
			pos.z *= previewScale;

			GS.MatrixPush();
			GS.MatrixTranslate(pos);
			GS.MatrixScale3f(HANDLE_RADIUS, HANDLE_RADIUS, 1.0f);
			GS.Draw(GSDrawMode.LineStrip, 0, 0);
			GS.MatrixPop();
		}
	}
}