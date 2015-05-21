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
using System.Runtime.InteropServices;

namespace OBS
{
	using obs_properties_t = IntPtr;
	using obs_property_t = IntPtr;

	using uint32_t = UInt32;

	public static partial class libobs
	{
		//EXPORT obs_properties_t *obs_properties_create(void);
		//EXPORT obs_properties_t *obs_properties_create_param(void *paramvoid (*destroy)(void *param));

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_properties_destroy(obs_properties_t props);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern void obs_properties_set_flags(obs_properties_t props, uint32_t flags);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern uint32_t obs_properties_get_flags(obs_properties_t props);

		//EXPORT void obs_properties_set_param(obs_properties_t *propsvoid *param, void (*destroy)(void *param));
		//EXPORT void *obs_properties_get_param(obs_properties_t *props);

		[DllImport(importLibrary, CallingConvention = importCall)]
		public static extern obs_property_t obs_properties_first(obs_properties_t props);

		//EXPORT obs_property_t *obs_properties_get(obs_properties_t *props, const char *property);
		//EXPORT void obs_properties_apply_settings(obs_properties_t *props, obs_data_t *settings);

		/* ------------------------------------------------------------------------- */

		//EXPORT obs_property_t *obs_properties_add_bool(obs_properties_t *propsconst char *name, const char *description);
		//EXPORT obs_property_t *obs_properties_add_int(obs_properties_t *propsconst char *name, const char *descriptionint min, int max, int step);
		//EXPORT obs_property_t *obs_properties_add_float(obs_properties_t *propsconst char *name, const char *descriptiondouble min, double max, double step);
		//EXPORT obs_property_t *obs_properties_add_int_slider(obs_properties_t *props, const char *name, const char *description, int min, int max, int step);
		//EXPORT obs_property_t *obs_properties_add_float_slider(obs_properties_t *props, const char *name, const char *description, double min, double max, double step);
		//EXPORT obs_property_t *obs_properties_add_text(obs_properties_t *propsconst char *name, const char *descriptionenum obs_text_type type);
		//EXPORT obs_property_t *obs_properties_add_path(obs_properties_t *propsconst char *name, const char *descriptionenum obs_path_type type, const char *filterconst char *default_path);
		//EXPORT obs_property_t *obs_properties_add_list(obs_properties_t *propsconst char *name, const char *descriptionenum obs_combo_type type, enum obs_combo_format format);
		//EXPORT obs_property_t *obs_properties_add_color(obs_properties_t *propsconst char *name, const char *description);
		//EXPORT obs_property_t *obs_properties_add_button(obs_properties_t *propsconst char *name, const char *textobs_property_clicked_t callback);
		//EXPORT obs_property_t *obs_properties_add_font(obs_properties_t *propsconst char *name, const char *description);
		//EXPORT obs_property_t *obs_properties_add_editable_list(obs_properties_t *props, const char *name, const char *description, bool allow_files, const char *filter, const char *default_path);
	}
}