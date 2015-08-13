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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OBS;

namespace test.Controls
{
	public partial class DisplayPanel : Panel
	{
		private ObsDisplay display;

		public DisplayCreatedDelegate displayCreated;
		public DisplayResizedDelegate displayResized;

		public delegate void DisplayCreatedDelegate();
		public delegate void DisplayResizedDelegate();

		public ObsDisplay Display
		{
			get { return display; }
		}

		public DisplayPanel()
		{
			InitializeComponent();

			Layout += DisplayPanel_Layout;
			VisibleChanged += DisplayPanel_VisibleChanged;
		}

		protected override void Dispose(bool disposing)
		{
			if (display != null)
				display.Dispose();
			
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void InitDisplay()
		{
			uint width = Width >= 0 ? (uint)Width : 0;
			uint height = Height >= 0 ? (uint)Height : 0;
			libobs.gs_init_data initData = new libobs.gs_init_data
			{
				cx = width,
				cy = height,
				format = libobs.gs_color_format.GS_RGBA,
				zsformat = libobs.gs_zstencil_format.GS_ZS_NONE,
				window = new libobs.gs_window
				{
					hwnd = Handle
				},
			};

			display = new ObsDisplay(initData);

			if (displayCreated != null)
				displayCreated();
		}

		private void DisplayPanel_Layout(object sender, LayoutEventArgs e)
		{
			if (display == null)
				InitDisplay();

			uint width = Width >= 0 ? (uint)Width : 0;
			uint height = Height >= 0 ? (uint)Height : 0;

			display.Resize(width, height);

			if (displayResized != null)
				displayResized();
		}

		private void DisplayPanel_VisibleChanged(object sender, EventArgs e)
		{
			if (!Visible)
				return;

			if (display != null)
			{
				uint width = Width >= 0 ? (uint)Width : 0;
				uint height = Height >= 0 ? (uint)Height : 0;

				display.Resize(width, height);
			}
			else
				InitDisplay();
		}
	}
}
