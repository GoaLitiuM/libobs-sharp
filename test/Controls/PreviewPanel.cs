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
using OBS;
using OBS.Graphics;
using System.Drawing;

namespace test.Controls
{
	public partial class PreviewPanel : DisplayPanel
	{
		private Scene scene;

		private GSVertexBuffer boxPrimitive;
		private GSVertexBuffer circlePrimitive;
		private float previewScale = 1;

		private const float HANDLE_RADIUS = 5.0f;
		private const float HANDLE_SEL_RADIUS = HANDLE_RADIUS * 1.5f;
		private const float CLAMP_DISTANCE = 10.0f;

		private bool dragging = false;
		Point dragLastPosition;

		public PreviewPanel()
		{
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		protected override void DisplayCreated() 
		{
			base.DisplayCreated();

			InitPrimitives();

			Display.AddDrawCallback(RenderPreview);
		}

		protected override void DisplayDestroyed() 
		{
			base.DisplayCreated();

			if (boxPrimitive != null)
				boxPrimitive.Dispose();
			if (circlePrimitive != null)
				circlePrimitive.Dispose();

			Display.RemoveDrawCallback(RenderPreview);
		}

		public void SetScene(Scene scene)
		{
			this.scene = scene;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!dragging)
				return;

			Point mousePosition = GetMousePositionInScene(e.Location);
			Point dragOffset = new Point(mousePosition.X - dragLastPosition.X, mousePosition.Y - dragLastPosition.Y);

			// move all the selected items

			foreach (ObsSceneItem item in scene.Items)
			{
				if (!item.Selected)
					continue;

				Vector2 newPosition = item.Position;
				newPosition.x += dragOffset.X;
				newPosition.y += dragOffset.Y;
				item.Position = newPosition;
			}

			dragLastPosition = mousePosition;
		}

		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			Point mousePosition = GetMousePositionInScene(e.Location);

			if (e.Button == MouseButtons.Left)
			{
				foreach (ObsSceneItem item in scene.Items)
				{
					// unselect all items
					item.Selected = false;

					Vector2 itemPosition = item.Position;
					Vector2 itemBounds = item.Bounds;

					bool isInside = mousePosition.X >= itemPosition.x &&
						mousePosition.X < itemPosition.x + itemBounds.x &&
						mousePosition.Y >= itemPosition.y &&
						mousePosition.Y < itemPosition.y + itemBounds.y;

					if (isInside && !dragging)
					{
						// test if dragging near edges

						dragging = true;
						dragLastPosition = mousePosition;
						item.Selected = true;
					}
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (e.Button == MouseButtons.Left)
				dragging = false;
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
		}

		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			// Event is triggered when input gets suddenly cancelled by other
			// actions or events, basically when program focus changes.

			base.OnMouseCaptureChanged(e);
		}

		private Point GetMousePositionInScene(System.Drawing.Point mousePosition)
		{
			libobs.obs_video_info ovi = Obs.GetVideoInfo();
			int baseWidth = (int)ovi.base_width;
			int baseHeight = (int)ovi.base_height;
			int scaledWidth = (int)(Width / previewScale);
			int scaledHeight = (int)(Height / previewScale);

			int left = (scaledWidth - baseWidth) / 2;
			int top = (scaledHeight - baseHeight) / 2;

			return new Point((int)((mousePosition.X / previewScale) - left), (int)((mousePosition.Y / previewScale) - top));
		}

		private void InitPrimitives()
		{
			using (GS.GraphicsContext())
			{
				// box from vertices
				boxPrimitive = new GSVertexBuffer();
				using (GS.RenderVertexBuffer(boxPrimitive))
				{
					GS.Vertex2f(0.0f, 0.0f);
					GS.Vertex2f(0.0f, 1.0f);
					GS.Vertex2f(1.0f, 1.0f);
					GS.Vertex2f(1.0f, 0.0f);
					GS.Vertex2f(0.0f, 0.0f);
				}

				// circle from vertices
				circlePrimitive = new GSVertexBuffer();
				using (GS.RenderVertexBuffer(circlePrimitive))
				{
					for (int i = 0; i <= 360; i += 360 / 20)
					{
						double pos = Math.PI * (double)i / 180.0;
						GS.Vertex2f((float)Math.Cos(pos), (float)Math.Sin(pos));
					}
				}
			}
		}

		private void RenderPreview(IntPtr data, uint cx, uint cy)
		{
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

			previewScale = (float)newW / baseWidth;

			GS.ViewportPush();
			GS.ProjectionPush();

			//setup orthographic projection of the whole scene to be presented on viewport
			GS.Ortho(0.0f, baseWidth, 0.0f, baseHeight, -100.0f, 100.0f);
			GS.SetViewport(centerX, centerY, newW, newH);

			//draw scene background
			ClearBackground(baseWidth, baseHeight);

			//render all visible sources
			Obs.RenderMainView();

			//calculate bottom-right corner on scene space
			int right = (int)cx - centerX;
			int bottom = (int)cy - centerY;

			//ortho for the outer area which would normally not appear on scene
			GS.Ortho(-centerX, right, -centerY, bottom, -100.0f, 100.0f);
			GS.ResetViewport();

			//render editing overlays
			RenderSceneEditing(data);

			GS.ProjectionPop();
			GS.ViewportPop();

			GS.LoadVertexBuffer(null);
		}

		private void ClearBackground(uint cx, uint cy)
		{
			GSEffect solid = Obs.GetSolidEffect();
			solid.SetParameterValue("color", new libobs.vec4(0.0f, 0.0f, 0.0f, 1.0f));

			GSEffectTechnique tech = solid.GetTechnique("Solid");

			GS.TechniqueBegin(tech);
			GS.TechniqueBeginPass(tech, 0);

			GS.MatrixPush();
			GS.MatrixIdentity();
			GS.MatrixScale3f((float)cx, (float)cy, 1.0f);

			GS.LoadVertexBuffer(boxPrimitive);

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

			// enumerate every sceneitem in scene
			if (scene != null)
				scene.EnumItems(DrawSelectedItem, data);

			GS.TechniqueEndPass(tech);
			GS.TechniqueEnd(tech);

			GS.LoadVertexBuffer(null);
		}

		private bool DrawSelectedItem(ObsScene scene, ObsSceneItem item, IntPtr data)
		{
			if (!item.Selected)
				return true;

			GS.LoadVertexBuffer(circlePrimitive);

			libobs.matrix4 boxTransform;
			libobs.obs_sceneitem_get_box_transform(item.GetPointer(), out boxTransform);

			//render the tiny circles on corners
			DrawPrimitive(0.0f, 0.0f, boxTransform, previewScale);
			DrawPrimitive(0.0f, 1.0f, boxTransform, previewScale);
			DrawPrimitive(1.0f, 1.0f, boxTransform, previewScale);
			DrawPrimitive(1.0f, 0.0f, boxTransform, previewScale);

			//render the main selection rectangle

			GS.LoadVertexBuffer(boxPrimitive);

			GS.MatrixPush();
			GS.MatrixScale3f(previewScale, previewScale, 1.0f);
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
