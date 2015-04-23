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

using System.Drawing;

namespace test.Utility
{
	public static class ColorHelper
	{
		public static Color TryColorFromHtml(string htmlColor)
		{
			return FromHtml(Color.Black, htmlColor);
		}

		public static Color FromHtml(this Color color, string htmlColor)
		{
			if (!htmlColor.StartsWith("#"))
				htmlColor = htmlColor.Insert(0, "#");

			try
			{
				color = ColorTranslator.FromHtml(htmlColor);
			}
			catch
			{
			}

			return color;
		}

		public static string ToHtml(this Color color)
		{
			//Microsoft's implementation of ColorTranslator.ToHtml may return
			//translated names of commonly known colors and not the color code.

			return "#" +
					color.R.ToString("X2") +
					color.G.ToString("X2") +
					color.B.ToString("X2");
		}
	}
}