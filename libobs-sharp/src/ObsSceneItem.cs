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

namespace OBS
{
	public class ObsSceneItem : IDisposable
	{
		internal IntPtr instance;    //pointer to unmanaged object
		internal ObsSource source;

		public unsafe ObsSceneItem(IntPtr sceneItem)
		{
			instance = sceneItem;

			source = new ObsSource(libobs.obs_sceneitem_get_source(instance));
		}

		public unsafe void Dispose()
		{
			if (instance == IntPtr.Zero)
				return;

			source.Dispose();

			libobs.obs_sceneitem_remove(instance);	//remove also removes it from sources
			instance = IntPtr.Zero;
		}

		public unsafe IntPtr GetPointer()
		{
			return instance;
		}

		public unsafe ObsSource GetSource()
		{
			return source;
		}

		public float X
		{
			get { return Position.x; }
		}

		public float Y
		{
			get { return Position.y; }
		}

		/// <summary>
		/// Boundary width of scene item
		/// </summary>
		public float Width
		{
			get { return Bounds.x; }
		}

		/// <summary>
		/// Boundary heigth of scene item
		/// </summary>
		public float Height
		{
			get { return Bounds.y; }
		}

		public string Name()
		{
			return source.Name;
		}

		public unsafe bool Selected
		{
			get { return libobs.obs_sceneitem_selected(instance); }
			set { libobs.obs_sceneitem_select(instance, value); }
		}

		public unsafe bool Visible
		{
			get { return libobs.obs_sceneitem_visible(instance); }
			set { libobs.obs_sceneitem_set_visible(instance, value); }
		}

		public Vector2 Position
		{
			get
			{
				Vector2 pos;
				libobs.obs_sceneitem_get_pos(instance, out pos);
				return pos;
			}
			set { libobs.obs_sceneitem_set_pos(instance, out value); }
		}

		/// <summary>
		/// Sets scale of layer
		/// </summary>
		public Vector2 Scale
		{
			get
			{
				Vector2 scale;
				libobs.obs_sceneitem_get_scale(instance, out scale);
				return scale;
			}
			set { libobs.obs_sceneitem_set_scale(instance, out value); }
		}

		public float Rotation
		{
			get { return libobs.obs_sceneitem_get_rot(instance); }
			set { libobs.obs_sceneitem_set_rot(instance, value); }
		}

		public unsafe Vector2 Bounds
		{
			get
			{
				Vector2 bounds;
				libobs.obs_sceneitem_get_bounds(instance, out bounds);
				return bounds;
			}
			set { libobs.obs_sceneitem_set_bounds(instance, out value); }
		}

		public unsafe ObsBoundsType BoundsType
		{
			get { return (ObsBoundsType)libobs.obs_sceneitem_get_bounds_type(instance); }
			set { libobs.obs_sceneitem_set_bounds_type(instance, (libobs.obs_bounds_type)value); }
		}

		public unsafe ObsAlignment BoundsAlignment
		{
			get { return (ObsAlignment)libobs.obs_sceneitem_get_bounds_alignment(instance); }
			set { libobs.obs_sceneitem_set_bounds_alignment(instance, (uint)value); }
		}

		public ObsAlignment Alignment
		{
			get { return (ObsAlignment)libobs.obs_sceneitem_get_alignment(instance); }
			set { libobs.obs_sceneitem_set_alignment(instance, (uint)value); }
		}

		public unsafe void SetBounds(Vector2 bounds, ObsBoundsType type)
		{
			libobs.obs_sceneitem_set_bounds(instance, out bounds);
			libobs.obs_sceneitem_set_bounds_type(instance, (libobs.obs_bounds_type)type);
		}

		public unsafe void SetBounds(Vector2 bounds, ObsAlignment alignment)
		{
			libobs.obs_sceneitem_set_bounds(instance, out bounds);
			libobs.obs_sceneitem_set_bounds_alignment(instance, (uint)alignment);
		}

		public unsafe void SetBounds(Vector2 bounds, ObsBoundsType type, ObsAlignment alignment)
		{
			libobs.obs_sceneitem_set_bounds(instance, out bounds);
			libobs.obs_sceneitem_set_bounds_type(instance, (libobs.obs_bounds_type)type);
			libobs.obs_sceneitem_set_bounds_alignment(instance, (uint)alignment);
		}
	}

	public enum ObsBoundsType : int
	{
		None,
		Stretch,
		ScaleInner,
		ScaleOuter,
		ScaleToWidth,
		ScaleToHeight,
		MaxOnly,
	};
}