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
	[Flags]
	enum ResizeEdge
	{
		None = 0,
		Left = 1,
		Top = 2,
		Right = 4,
		Bottom = 8,
	};

	public partial class PreviewPanel : DisplayPanel
	{
		private Scene scene;

		private GSVertexBuffer boxPrimitive;
		private GSVertexBuffer circlePrimitive;
		private float previewScale = 1;

		private bool dragging = false;
		private ResizeEdge resizeEdge = ResizeEdge.None;
		private Vector2 dragLastPosition;
		private Vector2 dragStartPosition;

		private Vector2 itemLastPosition;
		private Vector2 itemLastBounds;
		
		private ObsSceneItem hoveredItem = null;

		public PreviewPanel()
		{
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			SetHoveredItem(null);
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

		private void SetHoveredItem(ObsSceneItem item)
		{
			if (hoveredItem != null)
				hoveredItem.Dispose();

			hoveredItem = item;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			Vector2 mousePosition = GetPositionInScene(new Vector2(e.Location));
			Vector2 dragOffset = mousePosition - dragLastPosition;

			if (dragging)
			{
				// move all the selected items
				foreach (ObsSceneItem item in scene.Items)
				{
					if (!item.Selected)
						continue;

					item.Position += dragOffset;
				}

				dragLastPosition = mousePosition;
			}
			else if (resizeEdge != ResizeEdge.None && (dragOffset.x != 0.0f || dragOffset.y != 0.0f))
			{
				// move all the selected items
				foreach (ObsSceneItem item in scene.Items)
				{
					if (!item.Selected)
						continue;

					Vector2 newPos = item.Position;
					Vector2 newSize = item.Bounds;
					Vector2 offset = new Vector2(0.0f, 0.0f);
					ObsAlignment alignment = item.Alignment;

					if (resizeEdge.HasFlag(ResizeEdge.Left) || resizeEdge.HasFlag(ResizeEdge.Right))
						offset.x = dragOffset.x;
					if (resizeEdge.HasFlag(ResizeEdge.Top) || resizeEdge.HasFlag(ResizeEdge.Bottom))
						offset.y = dragOffset.y;

					if (resizeEdge.HasFlag(ResizeEdge.Left))
					{
						newSize.x -= offset.x;
						newPos.x += offset.x;
					}
					if (resizeEdge.HasFlag(ResizeEdge.Top))
					{
						newSize.y -= offset.y;
						newPos.y += offset.y;
					}
					if (resizeEdge.HasFlag(ResizeEdge.Right))
						newSize.x += offset.x;
					if (resizeEdge.HasFlag(ResizeEdge.Bottom))
						newSize.y += offset.y;

					Vector2 adjust = newPos-item.Position;

					if (alignment == ObsAlignment.Left)
						adjust.x = offset.x;
					else if (alignment == ObsAlignment.Right)
						adjust.x = offset.x;
					else
						adjust.x = offset.x / 2.0f;

					if (alignment == ObsAlignment.Top)
						adjust.y = offset.y;
					else if (alignment == ObsAlignment.Bottom)
						adjust.y = offset.y;
					else
						adjust.y = offset.y / 2.0f;

					if (resizeEdge.HasFlag(ResizeEdge.Left))
						adjust.x = -adjust.x;
					if (resizeEdge.HasFlag(ResizeEdge.Top))
						adjust.y = -adjust.y;

					newPos.x += adjust.x;
					newPos.y += adjust.y;

					item.Position = newPos;
					item.Bounds = newSize;
				}

				dragLastPosition = mousePosition;
			}

			// mouse hover over scene items
			SetHoveredItem(GetItemAtPosition(mousePosition));
			if (hoveredItem != null && hoveredItem.Selected)
				SetHoveredItem(null);
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

			if (e.Button == MouseButtons.Left)
				OnMouseLeftDown(e);
		}

		protected void OnMouseLeftDown(MouseEventArgs e)
		{
			Vector2 mousePosition = GetPositionInScene(new Vector2(e.Location));

			// unselect all items
			foreach (ObsSceneItem item in scene.Items)
				item.Selected = false;

			const float edgeSize = 0.01f;
			// select item under the mouse cursor
			using (ObsSceneItem mouseItem = GetItemAtPosition(mousePosition, edgeSize))
			{
				if (mouseItem == null || dragging)
					return;

				mouseItem.Selected = true;

				Vector3 transformedPos = Vector3.GetTransform(
					new Vector3(mousePosition), mouseItem.BoxTransform.GetInverse());

				bool edgeTop = transformedPos.y <= edgeSize;
				bool edgeBottom = transformedPos.y >= 1.0f-edgeSize;
				bool edgeLeft = transformedPos.x <= edgeSize;
				bool edgeRight = transformedPos.x >= 1.0f-edgeSize;
				bool center = !edgeTop && !edgeBottom && !edgeLeft && !edgeRight;

				if (center)
				{
					resizeEdge = ResizeEdge.None;
					Cursor.Current = Cursors.Hand;

					dragging = true;
					dragLastPosition = mousePosition;
					dragStartPosition = mousePosition;
				}
				else
				{
					dragLastPosition = mousePosition;
					dragStartPosition = mousePosition;
					itemLastPosition = mouseItem.Position;
					itemLastBounds = mouseItem.Bounds;
					resizeEdge = ResizeEdge.None;

					if (edgeTop)
					{
						Cursor.Current = Cursors.SizeNS;
						resizeEdge |= ResizeEdge.Top;
					}
					else if (edgeBottom)
					{
						Cursor.Current = Cursors.SizeNS;
						resizeEdge |= ResizeEdge.Bottom;
					}

					if (edgeLeft)
					{
						Cursor.Current = Cursors.SizeWE;
						resizeEdge |= ResizeEdge.Left;
					}
					else if (edgeRight)
					{
						Cursor.Current = Cursors.SizeWE;
						resizeEdge |= ResizeEdge.Right;
					}
				}

				if (mouseItem == hoveredItem)
					SetHoveredItem(null);
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (e.Button == MouseButtons.Left)
				OnMouseLeftUp(e);
		}

		protected void OnMouseLeftUp(MouseEventArgs e)
		{
			dragging = false;
			resizeEdge = ResizeEdge.None;
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

		private Vector2 GetPositionInScene(Vector2 position)
		{
			libobs.obs_video_info ovi = Obs.GetVideoInfo();
			int baseWidth = (int)ovi.base_width;
			int baseHeight = (int)ovi.base_height;
			int scaledWidth = (int)(Width / previewScale);
			int scaledHeight = (int)(Height / previewScale);

			int left = (scaledWidth - baseWidth) / 2;
			int top = (scaledHeight - baseHeight) / 2;

			return new Vector2((int)((position.x / previewScale) - left), (int)((position.y / previewScale) - top));
		}

		private ObsSceneItem GetItemAtPosition(Vector2 position, float extend = 0.0f)
		{
			foreach (ObsSceneItem item in scene.Items)
			{
				// test if the position is inside the rectangle
				Vector3 transformedPos = Vector3.GetTransform(
					new Vector3(position), item.BoxTransform.GetInverse());

				bool isInside = transformedPos.x >= 0.0f-extend &&
					transformedPos.x < 1.0f+extend &&
					transformedPos.y >= 0.0f-extend &&
					transformedPos.y < 1.0f+extend;

				if (isInside)
					return new ObsSceneItem(item);
			}

			return null;
			
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
			GSEffect solid = Obs.GetBaseEffect(ObsBaseEffect.Solid);
			solid.SetParameterValue("color", new Vector4(0.0f, 0.0f, 0.0f, 1.0f));

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
			if (scene == null)
				return;

			// draw selection outlines

			GSEffect solid = Obs.GetBaseEffect(ObsBaseEffect.Solid);
			solid.SetParameterValue("color", new Vector4(1.0f, 1.0f, 1.0f, 0.6f));

			GSEffectTechnique tech = solid.GetTechnique("Solid");

			GS.TechniqueBegin(tech);
			GS.TechniqueBeginPass(tech, 0);

			scene.EnumItems(DrawSelectedItem, data);

			GS.TechniqueEndPass(tech);
			GS.TechniqueEnd(tech);

			// hover outlines

			solid.SetParameterValue("color", new Vector4(1.0f, 1.0f, 1.0f, 0.40f));
			tech = solid.GetTechnique("Solid");

			GS.TechniqueBegin(tech);
			GS.TechniqueBeginPass(tech, 0);

			if (hoveredItem != null)
				DrawHoveredItem(scene, hoveredItem, data);

			GS.TechniqueEndPass(tech);
			GS.TechniqueEnd(tech);

			GS.LoadVertexBuffer(null);
		}

		private bool DrawSelectedItem(ObsScene scene, ObsSceneItem item, IntPtr data)
		{
			if (!item.Selected)
				return true;

			GS.LoadVertexBuffer(boxPrimitive);
			libobs.matrix4 boxTransform = item.BoxTransform;

			DrawOutline(boxTransform, item.Width, item.Height, 5.0f);

			return true;
		}

		private bool DrawHoveredItem(ObsScene scene, ObsSceneItem item, IntPtr data)
		{
			GS.LoadVertexBuffer(boxPrimitive);
			libobs.matrix4 boxTransform = item.BoxTransform;

			DrawOutline(boxTransform, item.Width, item.Height, 3.0f);

			return true;
		}

		private void DrawOutline(libobs.matrix4 matrix, float width, float height, float outlineThickness)
		{
			Vector3 scale = new Vector3(previewScale, previewScale, previewScale);
			libobs.matrix4_scale(out matrix, out matrix, out scale);

			GS.MatrixPush();
			GS.MatrixIdentity();
				
			GS.MatrixMul(matrix);

			float thickW = outlineThickness/width/previewScale;
			float thickH = outlineThickness/height/previewScale;

			GS.MatrixPush();
			GS.MatrixScale3f(1.0f-thickW, thickH, 1.0f);
			GS.Draw(GSDrawMode.TrisStrip, 0, 0);
			GS.MatrixPop();

			GS.MatrixPush();
			GS.MatrixTranslate(new Vector3(1.0f, 0.0f, 0.0f));		
			GS.MatrixScale3f(-thickW, 1.0f-thickH, 1.0f);
			GS.Draw(GSDrawMode.TrisStrip, 0, 0);
			GS.MatrixPop();

			GS.MatrixPush();
			GS.MatrixTranslate(new Vector3(1.0f, 1.0f, 0.0f));
			GS.MatrixScale3f(-1.0f+thickW, -thickH, 1.0f);
			GS.Draw(GSDrawMode.TrisStrip, 0, 0);
			GS.MatrixPop();

			GS.MatrixPush();
			GS.MatrixTranslate(new Vector3(0.0f, 1.0f, 0.0f));
			GS.MatrixScale3f(thickW, -1.0f+thickH, 1.0f);
			GS.Draw(GSDrawMode.TrisStrip, 0, 0);
			GS.MatrixPop();

			GS.MatrixPop();
		}
	}
}
