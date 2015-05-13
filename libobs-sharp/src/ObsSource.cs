﻿/***************************************************************************
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
	public class ObsSource : IObsContextData, IDisposable
	{
		internal IntPtr instance;    //pointer to unmanaged object

		private string _id;
		private ObsSourceType _type;

		public unsafe ObsSource(ObsSourceType type, string id, string name)
		{
			instance = libobs.obs_source_create((libobs.obs_source_type)type, id, name, IntPtr.Zero, IntPtr.Zero);

			if (instance == null)
				throw new ApplicationException("obs_source_create failed");

			_id = id;
			_type = type;
		}

		public unsafe ObsSource(ObsSourceType type, string id, string name, ObsData settings)
		{
			instance = libobs.obs_source_create((libobs.obs_source_type)type, id, name, settings.GetPointer(), IntPtr.Zero);

			if (instance == null)
				throw new ApplicationException("obs_source_create failed");

			_id = id;
			_type = type;
		}

		public unsafe ObsSource(IntPtr instance)
		{
			this.instance = instance;
			libobs.obs_source_addref(instance);
		}

		public unsafe void Dispose()
		{
			if (instance == IntPtr.Zero)
				return;

			libobs.obs_source_release(instance);

			instance = IntPtr.Zero;
		}

		public unsafe IntPtr GetPointer()
		{
			return instance;
		}

		/// <summary>
		/// Removes all references to this source.
		/// </summary>
		public unsafe void Remove()
		{
			libobs.obs_source_remove(instance);
		}

		public unsafe string Name
		{
			get { return libobs.obs_source_get_name(instance); }
			set { libobs.obs_source_set_name(instance, value); }
		}

		public unsafe uint Width
		{
			get
			{
				return libobs.obs_source_get_width(instance);
			}
		}

		public unsafe uint Height
		{
			get
			{
				return libobs.obs_source_get_height(instance);
			}
		}

		/// <summary> Returns the underlying scene of this source if it's a scene. </summary>
		/// <returns> null if source is not a scene. </returns>
		public unsafe ObsScene GetScene()
		{
			IntPtr ptr = libobs.obs_scene_from_source(instance);
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsScene(ptr);
		}

		public unsafe ObsData GetSettings()
		{
			IntPtr ptr = libobs.obs_source_get_settings(instance);
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsData(ptr);
		}

		public unsafe static ObsProperties GetProperties(ObsSourceType type, string id)
		{
			IntPtr ptr = libobs.obs_get_source_properties((libobs.obs_source_type)type, id);
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsProperties(ptr);
		}

		public unsafe ObsProperties GetProperties()
		{
			IntPtr ptr = libobs.obs_source_properties(instance);
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsProperties(ptr);
		}

		public unsafe void AddFilter(ObsSource filter)
		{
			libobs.obs_source_filter_add(instance, filter.GetPointer());
		}

		public unsafe void RemoveFilter(ObsSource filter)
		{
			libobs.obs_source_filter_remove(instance, filter.GetPointer());
		}

		public unsafe ObsSource GetFilterParent(ObsSource filter)
		{
			IntPtr ptr = libobs.obs_filter_get_parent(filter.GetPointer());
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsSource(ptr);
		}

		public unsafe ObsSource GetFilterTarget(ObsSource filter)
		{
			IntPtr ptr = libobs.obs_filter_get_target(filter.GetPointer());
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsSource(ptr);
		}

		public unsafe void Update()
		{
			libobs.obs_source_update(instance, libobs.obs_source_get_settings(instance));
		}

		public bool Enabled
		{
			get { return libobs.obs_source_enabled(instance); }
			set { libobs.obs_source_set_enabled(instance, value); }
		}

		public bool Muted
		{
			get { return libobs.obs_source_muted(instance); }
			set { libobs.obs_source_set_muted(instance, value); }
		}

		public unsafe void Update(ObsData settings)
		{
			libobs.obs_source_update(instance, settings.GetPointer());
		}

		public unsafe void Render()
		{
			libobs.obs_source_video_render(instance);
		}

		public ObsData GetDefaults()
		{
			var ptr = libobs.obs_get_source_defaults((libobs.obs_source_type)_type, _id);
			if (ptr == IntPtr.Zero)
				return null;

			return new ObsData(ptr);
		}
	}

	public enum ObsSourceType : int
	{
		Input,
		Filter,
		Transition,
	};
}