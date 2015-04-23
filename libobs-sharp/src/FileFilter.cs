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
using System.Text.RegularExpressions;

namespace OBS
{
	// File Filter encoding
	//
	// Qt encoding examples
	// "Image Files (*.BMP *.JPG *.GIF);;All files (*.*)"
	// "Image Files (*.BMP;*.JPG;*.GIF);;*.*"
	//
	// .NET encoding example
	// "Image Files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"

	// Simple wrapper for Qt file filters
	public class FileFilter
	{
		private string filterNet;
		private string filterQt;

		public static FileFilter NetFilter(string filter)
		{
			return new FileFilter
			{
				filterNet = filter,
				filterQt = ConvertToQtFilter(filter)
			};
		}

		public static FileFilter QtFilter(string filter)
		{
			return new FileFilter
			{
				filterNet = ConvertToNetFilter(filter),
				filterQt = filter
			};
		}

		private static string ConvertToQtFilter(string filter)
		{
			//TODO: .NET to Qt file filter conversion
			throw new NotImplementedException();
		}

		private static string ConvertToNetFilter(string filter)
		{
			string filterNet = "";
			string[] groups = filter.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string group in groups)
			{
				//captures description and pattern from Qt-encoded file filter group
				Match match = new Regex(@"^([^\(]+)\(([^)]+)").Match(group);
				if (match.Success)
				{
					string pattern = match.Groups[2].Value.Replace(' ', ';');
					filterNet += String.Format("{0}|{1}|", group, pattern);
				}
				else
				{
					//group itself is the pattern
					filterNet += String.Format("{0}|{0}|", group);
				}
			}
			filterNet = filterNet.Remove(filterNet.LastIndexOf('|'));

			return filterNet;
		}

		// Returns .NET encoded file filter list
		public override string ToString()
		{
			return GetNetFilter();
		}

		public string GetNetFilter()
		{
			return filterNet;
		}

		public string GetQtFilter()
		{
			return filterQt;
		}
	}
}