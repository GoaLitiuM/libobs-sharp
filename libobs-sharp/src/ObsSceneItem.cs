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
using System.Drawing;

namespace OBS
{
	public class ObsSceneItem
	{
        internal IntPtr instance;    //pointer to unmanaged object

        public unsafe ObsSceneItem(IntPtr sceneItem)
		{
			instance = sceneItem;
			libobs.obs_sceneitem_addref(instance);
		}

		~ObsSceneItem()
		{
			Release();
		}

		#region Geometry
		public int X
		{
			get { return Position.X; }
		}

		public int Y
		{
			get { return Position.Y; }
		}

		/// <summary>
		/// Absolute width of scene item
		/// </summary>
		public int Width
		{
			get { return Size.Width; }
		}

		/// <summary>
		/// Absolute heigth of scene item
		/// </summary>
		public int Height
		{
			get { return Size.Height; }
		}

		public Point Position
		{
			get
			{
				libobs.vec2 pos = GetPosition();
				return new Point((int)pos.x, (int)pos.y);
			}
		}

		/// <summary>
		/// Absolute Size of scene item
		/// </summary>
		public Size Size
		{
			get
			{
				var source = GetSource();
				var scale = GetScale();
				return new Size((int) (source.Width * scale.x),(int) (source.Height * scale.y));
			}
		}
		/// <summary>
		/// Scene item bounds
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(Position, Size);
			}
		}
		#endregion

		public string Name()
		{
			return GetSource().Name;
		}

		public unsafe void Release()
		{
            if (instance == IntPtr.Zero)
				return;

			ObsSource source = GetSource();
			source.Release();

			libobs.obs_sceneitem_remove(instance);	//remove also removes it from sources
            instance = IntPtr.Zero;
		}
        public unsafe IntPtr GetPointer()
		{
			return instance;
		}

		public unsafe ObsSource GetSource()
		{
            IntPtr source = libobs.obs_sceneitem_get_source(instance);
			return new ObsSource(source);
		}
		public unsafe bool Selected
		{
			get
			{
				return libobs.obs_sceneitem_selected(instance);
			}
			set
			{
				libobs.obs_sceneitem_select(instance, value);
			}
		}

		public unsafe libobs.vec2 GetPosition()
		{
			libobs.vec2 position;
			libobs.obs_sceneitem_get_pos(instance, out position);
			return position;
		}

		public unsafe float GetRotation()
		{
			return libobs.obs_sceneitem_get_rot(instance);
		}

		public unsafe libobs.vec2 GetScale()
		{
			libobs.vec2 scale;
			libobs.obs_sceneitem_get_scale(instance, out scale);
			return scale;
		}

		public unsafe ObsAlignment GetAlignment()
		{
			return (ObsAlignment)libobs.obs_sceneitem_get_alignment(instance);
		}


		public unsafe void SetPosition(libobs.vec2 position)
		{
			libobs.obs_sceneitem_set_pos(instance, out position);
		}

		public unsafe void SetRotation(float rotation)
		{
			libobs.obs_sceneitem_set_rot(instance, rotation);
		}

		/// <summary>
		/// Sets scale of layer
		/// </summary>
		/// <param name="scale">1f = original size</param>
		public unsafe void SetScale(libobs.vec2 scale)
		{
			libobs.obs_sceneitem_set_scale(instance, out scale);
		}

		public unsafe void SetAlignment(ObsAlignment alignment)
		{
			libobs.obs_sceneitem_set_alignment(instance, (uint)alignment);
		}
	}
}
