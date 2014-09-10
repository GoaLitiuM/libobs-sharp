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

#region Usings

using System;
using System.Linq;
using System.Windows.Forms;
using OBS;

#endregion

namespace test
{
	public partial class AlignmentBox : UserControl
	{
		private libobs.obs_align_type _align;

		public delegate void ClickHandler(libobs.obs_align_type e);

		/// <summary>
		/// Fired when one of the alignment buttons is clicked
		/// e == alignment
		/// </summary>
		public new event ClickHandler Click;

		public AlignmentBox(libobs.obs_align_type current)
		{
			InitializeComponent();
			
			foreach (
				RadioButton btn in panel.Controls.Cast<object>().Where(control => control.GetType() == (typeof(RadioButton))).Cast<RadioButton>())
			{
				btn.Click += btn_Click;
			}

			_align = current;
			if ((_align & libobs.obs_align_type.OBS_ALIGN_TOP) != 0)
			{
				if ((_align & libobs.obs_align_type.OBS_ALIGN_LEFT) != 0)
				{
					topleft.Checked = true;
				}
				else if ((_align & libobs.obs_align_type.OBS_ALIGN_CENTER) != 0)
				{
					topcenter.Checked = true;
				}
				else
				{
					topright.Checked = true;
				}
			}
			else if ((_align & libobs.obs_align_type.OBS_ALIGN_CENTER) != 0)
			{
				if ((_align & libobs.obs_align_type.OBS_ALIGN_LEFT) != 0)
				{
					centerleft.Checked = true;
				}
				else if ((_align & libobs.obs_align_type.OBS_ALIGN_CENTER) != 0)
				{
					center.Checked = true;
				}
				else
				{
					centerright.Checked = true;
				}
			}
			else
			{
				if ((_align & libobs.obs_align_type.OBS_ALIGN_LEFT) != 0)
				{
					bottomleft.Checked = true;
				}
				else if ((_align & libobs.obs_align_type.OBS_ALIGN_CENTER) != 0)
				{
					bottomcenter.Checked = true;
				}
				else
				{
					bottomright.Checked = true;
				}
			}
		}

		private void btn_Click(object sender, EventArgs e)
		{
			// TODO: surely this can be done cleaner x)

			RadioButton btn = (RadioButton)sender;
			string name = btn.Name;
			if (name.StartsWith("top"))
			{
				_align = libobs.obs_align_type.OBS_ALIGN_TOP;
				if (name.EndsWith("left"))
				{
					_align = _align | libobs.obs_align_type.OBS_ALIGN_LEFT;
				}
				else if (name.EndsWith("center"))
				{
					_align = _align | libobs.obs_align_type.OBS_ALIGN_CENTER;
				}
				else
				{
					_align = _align | libobs.obs_align_type.OBS_ALIGN_RIGHT;
				}
			}
			else if (name.StartsWith("center"))
			{
				_align = libobs.obs_align_type.OBS_ALIGN_CENTER;
				if (name.EndsWith("left"))
				{
					_align = _align | libobs.obs_align_type.OBS_ALIGN_LEFT;
				}
				else if (name.EndsWith("center"))
				{
					// nothing!
				}
				else
				{
					_align = _align | libobs.obs_align_type.OBS_ALIGN_RIGHT;
				}
			}
			else
			{
				_align = libobs.obs_align_type.OBS_ALIGN_BOTTOM;
				if (name.EndsWith("left"))
				{
					_align = _align | libobs.obs_align_type.OBS_ALIGN_LEFT;
				}
				else if (name.EndsWith("center"))
				{
					_align = _align | libobs.obs_align_type.OBS_ALIGN_CENTER;
				}
				else
				{
					_align = _align | libobs.obs_align_type.OBS_ALIGN_RIGHT;
				}
			}
			if (Click != null) Click(_align);
		}
	}
}