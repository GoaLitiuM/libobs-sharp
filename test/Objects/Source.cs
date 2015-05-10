/***************************************************************************
	Copyright (C) 2014-2015 by Nick Thijssen <lamah83@gmaill.com>

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

using OBS;

namespace test
{
	public class Source : ObsSource
	{
		private readonly ObsSource _instance;

		public Source(ObsSourceType type, string id, string name)
			: base(type, id, name)
		{
			_instance = GetBase();
		}

		public Source(ObsSourceType type, string id, string name, ObsData settings)
			: base(type, id, name, settings)
		{
			_instance = GetBase();
		}

		public Source(IntPtr instance)
			: base(instance)
		{
			_instance = GetBase();
		}

		/// <summary>
		/// The base class which this is inherited from
		/// </summary>
		/// <returns>The base ObsSource of this Source</returns>
		public ObsSource GetInstance()
		{
			return _instance;
		}
	}
}