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
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
	public partial class TestForm : Form
	{
		private GSVertexBuffer _boxPrimitive;
		private GSVertexBuffer _circlePrimitive;
		public float MainViewWidth = 1;
		public float MainViewHeight = 1;

		const float HANDLE_RADIUS = 5.0f;
		const float HANDLE_SEL_RADIUS = HANDLE_RADIUS * 1.5f;
		const float CLAMP_DISTANCE = 10.0f;

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
			GS.ViewportPush();
			GS.ProjectionPush();

			TestForm window = Control.FromHandle(data) as TestForm;
			libobs.obs_video_info ovi = Obs.GetVideoInfo();

			int previewCX = window.mainViewPanel.Width;
			int previewCY = window.mainViewPanel.Height;
			double previewAspect = (double)previewCX / previewCY;
			double baseAspect = (double)ovi.base_width / (double)ovi.base_height;

			//adjust either width or height to match base aspect ratio
			if (previewAspect < baseAspect)
				previewCY = (int)((double)previewCX / baseAspect);
			else
				previewCX = (int)((double)previewCY * baseAspect);

			//calculate viewport top-left corner in panel to place the scene in center of it
			int previewX = (int)(((double)window.mainViewPanel.Width - previewCX) / 2);
			int previewY = (int)(((double)window.mainViewPanel.Height - previewCY) / 2);
			//window._previewScale = (float)previewCX / ovi.base_width;
			window.MainViewWidth = previewCX;
			window.MainViewHeight = previewCY;

			//setup orthographic projection of the whole scene to be presented on viewport
			GS.Ortho(0.0f, (float)ovi.base_width, 0.0f, (float)ovi.base_height, -100.0f, 100.0f);
			GS.SetViewport(previewX, previewY, previewCX, previewCY);

			//draw scene background
			window.ClearBackground(ovi.base_width, ovi.base_height);

			//render main view including all visible sources
			Obs.RenderMainView();

			//calculate bottom-right corner on scene space
			int right = window.mainViewPanel.Width - previewX;
			int bottom = window.mainViewPanel.Height - previewY;

			//ortho for the outer area which would normally not appear on scene
			GS.Ortho(-previewX, right, -previewY, bottom, -100.0f, 100.0f);
			GS.ResetViewport();

			//TODO: render everything related to scene editing here like source outlines
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

			//enum every sceneitem in scene
			ObsScene scene = _scenes[_selectedScene];
			if (scene != null)
				scene.EnumItems(_EnumSceneItem, data);

			GS.TechniqueEndPass(tech);
			GS.TechniqueEnd(tech);

			GS.LoadVertexBuffer(null);
		}

		private bool EnumSceneItem(IntPtr scene, IntPtr item, IntPtr data)
		{
			if (!libobs.obs_sceneitem_selected(item))
				return true;

			TestForm window = Control.FromHandle(data) as TestForm;
			float previewScale = MainViewWidth / MainWidth;

			GS.LoadVertexBuffer(_circlePrimitive);

			libobs.matrix4 boxTransform;
			libobs.obs_sceneitem_get_box_transform(item, out boxTransform);

			//render the tiny circles on corners
			DrawPrimitive(0.0f, 0.0f, boxTransform, previewScale);
			DrawPrimitive(0.0f, 1.0f, boxTransform, previewScale);
			DrawPrimitive(1.0f, 1.0f, boxTransform, previewScale);
			DrawPrimitive(1.0f, 0.0f, boxTransform, previewScale);

			//render the main selection rectangle

			GS.LoadVertexBuffer(_boxPrimitive);

			GS.MatrixPush();
			GS.MatrixScale3f(previewScale, previewScale, 1.0f);
			GS.MatrixMul(boxTransform);
			GS.Draw(GSDrawMode.LineStrip, 0, 0);
			GS.MatrixPop();

			return true;
		}

		static void DrawPrimitive(float x, float y, libobs.matrix4 matrix, float previewScale)
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

		public float GetPreviewScale()
		{
			return (float)MainViewWidth / MainWidth;
		}
	}
}
