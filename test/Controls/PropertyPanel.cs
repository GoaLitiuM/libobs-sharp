/***************************************************************************
	Copyright (C) 2014 by Nick Thijssen <lamah83@gmail.com>
	
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

using System.Collections.Generic;
using System.Windows.Forms;

namespace test.Controls
{
	public partial class PropertyPanel : UserControl
	{
		public PropertyPanel(string name, IEnumerable<Control> controls)
		{
			InitializeComponent();
			nameLabel.Text = name;
			foreach (var control in controls)
			{
				controlPanel.Controls.Add(control);				
			}

			foreach (Control control in controlPanel.Controls)
			{
				int topmargin = (controlPanel.Height - control.Height) / 2;
				Padding oldmargin = control.Margin;
				oldmargin.Top = topmargin;
				oldmargin.Bottom = topmargin;
				control.Margin = oldmargin;
			}
		}
	}
}
