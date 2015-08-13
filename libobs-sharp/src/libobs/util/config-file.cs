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
	using config_t = IntPtr;

	using size_t = UIntPtr;
	using int64_t = Int64;
	using uint64_t = UInt64;

	public static partial class libobs
	{
		// Generic ini-style config file functions

		//EXPORT config_t *config_create(const char *file);
		//EXPORT int config_open(config_t **config, const char *file, enum config_open_type open_type);
		//EXPORT int config_open_string(config_t **config, const char *str);
		//EXPORT int config_save(config_t *config);
		//EXPORT void config_close(config_t *config);

		//EXPORT size_t config_num_sections(config_t *config);
		//EXPORT const char *config_get_section(config_t *config, size_t idx);

		//EXPORT void config_set_string(config_t *config, const char *section, const char *name, const char *value);
		//EXPORT void config_set_int(config_t *config, const char *section, const char *name, int64_t value);
		//EXPORT void config_set_uint(config_t *config, const char *section, const char *name, uint64_t value);
		//EXPORT void config_set_bool(config_t *config, const char *section, const char *name, bool value);
		//EXPORT void config_set_double(config_t *config, const char *section, const char *name, double value);

		//EXPORT const char *config_get_string(const config_t *config, const char *section, const char *name);
		//EXPORT int64_t config_get_int(const config_t *config, const char *section, const char *name);
		//EXPORT uint64_t config_get_uint(const config_t *config, const char *section, const char *name);
		//EXPORT bool config_get_bool(const config_t *config, const char *section, const char *name);
		//EXPORT double config_get_double(const config_t *config, const char *section, const char *name);
		//EXPORT bool config_remove_value(config_t* config, const char* section, const char* name);

		/*
		 * DEFAULT VALUES
		 *
		 * The following functions are used to set what values will return if they do
		 * not exist.  Call these functions *once* for each known value before reading
		 * any of them anywhere else.
		 *
		 * These do *not* actually set any values, they only set what values will be
		 * returned for config_get_* if the specified variable does not exist.
		 *
		 * You can initialize the defaults programmitically using config_set_default_*
		 * functions (recommended for most cases), or you can initialize it via a file
		 * with config_open_defaults.
		 */

		//EXPORT int config_open_defaults(config_t *config, const char *file);

		//EXPORT void config_set_default_string(config_t *config, const char *section, const char *name, const char *value);
		//EXPORT void config_set_default_int(config_t *config, const char *section, const char *name, int64_t value);
		//EXPORT void config_set_default_uint(config_t *config, const char *section, const char *name, uint64_t value);
		//EXPORT void config_set_default_bool(config_t *config, const char *section, const char *name, bool value);
		//EXPORT void config_set_default_double(config_t *config, const char *section, const char *name, double value);

		/* These functions allow you to get the current default values rather than get
		 * the actual values.  Probably almost never really needed */

		//EXPORT const char *config_get_default_string(const config_t *config, const char *section, const char *name);
		//EXPORT int64_t config_get_default_int(const config_t *config, const char *section, const char *name);
		//EXPORT uint64_t config_get_default_uint(const config_t *config, const char *section, const char *name);
		//EXPORT bool config_get_default_bool(const config_t *config, const char *section, const char *name);
		//EXPORT double config_get_default_double(const config_t *config, const char *section, const char *name);

		//EXPORT bool config_has_user_value(const config_t *config, const char *section, const char *name);
		//EXPORT bool config_has_default_value(const config_t *config, const char *section, const char *name);

		public enum config_result : int
		{
			CONFIG_SUCCESS = 0,
			CONFIG_FILENOTFOUND = -1,
			CONFIG_ERROR = -2,
		}

		public enum config_open_type
		{
			CONFIG_OPEN_EXISTING,
			CONFIG_OPEN_ALWAYS,
		}
	}
}
