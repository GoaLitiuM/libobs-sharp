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
using System.Drawing;

namespace OBS.Graphics
{
	//wraps all graphic subsystem functions (gs_)
	public static class GS
	{
		public static unsafe void EnterGraphics()
		{
			libobs.obs_enter_graphics();
		}

		public static unsafe void LeaveGraphics()
		{
			libobs.obs_leave_graphics();
		}

		public static unsafe void RenderStart(bool b_new)
		{
			libobs.gs_render_start(b_new);
		}

		public static unsafe void RenderStop(GSDrawMode mode)
		{
			libobs.gs_render_stop((libobs.gs_draw_mode)mode);
		}

		public static unsafe GSVertexBuffer RenderSave()
		{
			IntPtr ptr = libobs.gs_render_save();
			if (ptr == null)
				return null;

			GSVertexBuffer vb = new GSVertexBuffer(ptr);
			return vb;
		}

		public static unsafe void Vertex2f(float x, float y)
		{
			libobs.gs_vertex2f(x, y);
		}

		public static unsafe void Clear(GSClearFlags clear_flags, Vector4 color, float depth, Byte stencil)
		{
			libobs.gs_clear((UInt32)clear_flags, out color, depth, stencil);
		}

		public static unsafe void Clear(GSClearFlags clear_flags, Color color, float depth, Byte stencil)
		{
			Clear(clear_flags, new Vector4(color), depth, stencil);
		}

		public static unsafe void ViewportPush()
		{
			libobs.gs_viewport_push();
		}

		public static unsafe void ViewportPop()
		{
			libobs.gs_viewport_pop();
		}

		public static unsafe void ProjectionPush()
		{
			libobs.gs_projection_push();
		}

		public static unsafe void ProjectionPop()
		{
			libobs.gs_projection_pop();
		}

		public static unsafe void Ortho(float left, float right, float top, float bottom, float znear, float zfar)
		{
			libobs.gs_ortho(left, right, top, bottom, znear, zfar);
		}

		public static unsafe void Frustum(float left, float right, float top, float bottom, float znear, float zfar)
		{
			libobs.gs_frustum(left, right, top, bottom, znear, zfar);
		}

		public static unsafe void SetViewport(int x, int y, int width, int height)
		{
			libobs.gs_set_viewport(x, y, width, height);
		}

		public static unsafe Rectangle GetViewport(int x, int y, int width, int height)
		{
			libobs.gs_rect rect = new libobs.gs_rect();
			libobs.gs_get_viewport(out rect);
			return new Rectangle(rect.x, rect.y, rect.cx, rect.cy);
		}

		//sets the viewport to current swap chain size
		public static unsafe void ResetViewport()
		{
			libobs.gs_reset_viewport();
		}

		//sets default screen-sized orthographich mode
		public static unsafe void Set2DMode()
		{
			libobs.gs_set_2d_mode();
		}

		//sets default screen-sized perspective mode
		public static unsafe void Set3DMode(double fovy, double znear, double zvar)
		{
			libobs.gs_set_3d_mode(fovy, znear, zvar);
		}

		public static unsafe void LoadVertexBuffer(GSVertexBuffer vertexBuffer)
		{
			IntPtr ptr = IntPtr.Zero;

			if (vertexBuffer != null)
				ptr = vertexBuffer.GetPointer();

			libobs.gs_load_vertexbuffer(ptr);
		}

		public static unsafe void LoadIndexBuffer(GSIndexBuffer indexBuffer)
		{
			IntPtr ptr = IntPtr.Zero;

			if (indexBuffer != null)
				ptr = indexBuffer.GetPointer();

			libobs.gs_load_indexbuffer(ptr);
		}

		public static unsafe void LoadTexture(GSTexture texture, int unit)
		{
			IntPtr ptr = IntPtr.Zero;

			if (texture != null)
				ptr = texture.GetPointer();

			libobs.gs_load_texture(ptr, unit);
		}

		public static unsafe void LoadSamplerState(GSSamplerState samplerState, int unit)
		{
			IntPtr ptr = IntPtr.Zero;

			if (samplerState != null)
				ptr = samplerState.GetPointer();

			libobs.gs_load_samplerstate(ptr, unit);
		}

		public static unsafe void LoadVertexShader(GSShader shader)
		{
			IntPtr ptr = IntPtr.Zero;

			if (shader != null)
				ptr = shader.GetPointer();

			libobs.gs_load_vertexshader(ptr);
		}

		public static unsafe void LoadPixelShader(GSShader shader)
		{
			IntPtr ptr = IntPtr.Zero;

			if (shader != null)
				ptr = shader.GetPointer();

			libobs.gs_load_pixelshader(ptr);
		}

		public static unsafe void TechniqueBegin(GSEffectTechnique tech)
		{
			libobs.gs_technique_begin((IntPtr)tech.GetPointer());
		}

		public static unsafe void TechniqueEnd(GSEffectTechnique tech)
		{
			libobs.gs_technique_end((IntPtr)tech.GetPointer());
		}

		public static unsafe void TechniqueBeginPass(GSEffectTechnique tech, uint pass)
		{
			libobs.gs_technique_begin_pass((IntPtr)tech.GetPointer(), (UIntPtr)pass);
		}

		public static unsafe void TechniqueEndPass(GSEffectTechnique tech)
		{
			libobs.gs_technique_end_pass((IntPtr)tech.GetPointer());
		}

		public static unsafe void MatrixPush()
		{
			libobs.gs_matrix_push();
		}

		public static unsafe void MatrixPop()
		{
			libobs.gs_matrix_pop();
		}

		public static unsafe void MatrixIdentity()
		{
			libobs.gs_matrix_identity();
		}

		public static unsafe void MatrixTranslate(Vector3 vec)
		{
			libobs.gs_matrix_translate(out vec);
		}

		public static unsafe void MatrixTranslate3f(float x, float y, float z)
		{
			libobs.gs_matrix_translate3f(x, y, z);
		}

		public static unsafe void MatrixScale(Vector3 vec)
		{
			libobs.gs_matrix_scale(out vec);
		}

		public static unsafe void MatrixScale3f(float x, float y, float z)
		{
			libobs.gs_matrix_scale3f(x, y, z);
		}

		public static unsafe void MatrixRotate(Vector4 vec)
		{
			libobs.gs_matrix_rotaa(out vec);
		}

		public static unsafe void MatrixRotate4f(float x, float y, float z, float angle)
		{
			libobs.gs_matrix_rotaa4f(x, y, z, angle);
		}

		public static unsafe void MatrixMul(libobs.matrix4 matrix)
		{
			libobs.gs_matrix_mul(out matrix);
		}

		public static unsafe void Draw(GSDrawMode drawMode, uint startVert, uint numVerts)
		{
			libobs.gs_draw((libobs.gs_draw_mode)drawMode, startVert, numVerts);
		}

		/// <summary>
		/// Handles entering and leaving OBS graphics context, used with using pattern (RAII).
		/// Calls EnterGraphics, and LeaveGraphics at the end of the using block.
		/// </summary>
		///	<example>
		/// using (GS.GraphicsContext())
		/// {
		///		...
		/// }
		/// </example>
		public static GraphicsContextHelper GraphicsContext()
		{
			return new GraphicsContextHelper();
		}

		// helper class for entering OBS graphics context with using pattern
		public class GraphicsContextHelper : IDisposable
		{
			public GraphicsContextHelper()
			{
				GS.EnterGraphics();
			}

			public void Dispose()
			{
				GS.LeaveGraphics();
			}
		}

		/// <summary>
		/// Render data to vertex buffer, used with using pattern (RAII).
		/// Calls RenderStart, and RenderSave at the end of the using block.
		/// </summary>
		///	<example>
		/// GSVertexBuffer vb = new GSVertexBuffer();
		/// using (GS.RenderVertexBuffer(vb))
		/// {
		///		...
		/// }
		/// </example>
		public static RenderVertexBufferHelper RenderVertexBuffer(GSVertexBuffer vb)
		{
			return new RenderVertexBufferHelper(vb);
		}

		// helper class for rendering into VB with using pattern
		public class RenderVertexBufferHelper : IDisposable
		{
			GSVertexBuffer vb;

			public RenderVertexBufferHelper(GSVertexBuffer vb)
			{
				this.vb = vb;
				if (vb == null)
					return;

				GS.RenderStart(true);
			}

			public void Dispose()
			{
				if (vb == null)
					return;

				GSVertexBuffer newvb = GS.RenderSave();
				
				// transfer the internal pointer to old object
				vb.Dispose();
				vb.instance = newvb.instance;

				newvb.instance = IntPtr.Zero;
				newvb.Dispose();
			}
		}
	}

	public enum GSClearFlags : uint
	{
		Color = (1 << 0),
		Depth = (1 << 1),
		Stencil = (1 << 2),
		All = Color | Depth | Stencil,
	}

	public enum GSDrawMode : int
	{
		Points,
		Lines,
		LineStrip,
		Tris,
		TrisStrip,
	};

	public class GSIndexBuffer
	{
		internal IntPtr instance = IntPtr.Zero;    //pointer to unmanaged object

		public IntPtr GetPointer()
		{
			return instance;
		}
	}

	public class GSTexture
	{
		internal IntPtr instance = IntPtr.Zero;    //pointer to unmanaged object

		public IntPtr GetPointer()
		{
			return instance;
		}
	}

	public class GSSamplerState
	{
		internal IntPtr instance = IntPtr.Zero;    //pointer to unmanaged object

		public IntPtr GetPointer()
		{
			return instance;
		}
	}

	public class GSShader
	{
		internal IntPtr instance = IntPtr.Zero;    //pointer to unmanaged object

		public IntPtr GetPointer()
		{
			return instance;
		}
	}
}