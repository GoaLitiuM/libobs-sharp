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

// TODO: condense these 2 into a single method with get/set flag

namespace test
{
	public partial class AlignmentBox : UserControl
	{
		public ObsAlignment Alignment
		{
			get { return _align; }
			set
			{
				_align = value;
				SetAlign();
			}
		}

		private ObsAlignment _align = ObsAlignment.Center;

		public delegate void ClickHandler(ObsAlignment e);

		/// <summary>
		/// Fired when one of the alignment buttons is clicked
		/// e == alignment
		/// </summary>
		public new event ClickHandler Click;

		public AlignmentBox()
		{
			InitializeComponent();

			foreach (
				RadioButton btn in panel.Controls.Cast<object>().Where(control => control.GetType() == (typeof(RadioButton))).Cast<RadioButton>())
			{
				btn.Click += btn_Click;
			}

			SetAlign();
		}

		private void SetAlign()
		{
			if ((_align & ObsAlignment.Top) != 0)
			{
				if ((_align & ObsAlignment.Left) != 0)
				{
					topleft.Checked = true;
				}
				else if ((_align & ObsAlignment.Center) != 0)
				{
					topcenter.Checked = true;
				}
				else
				{
					topright.Checked = true;
				}
			}
			else if ((_align & ObsAlignment.Center) != 0)
			{
				if ((_align & ObsAlignment.Left) != 0)
				{
					centerleft.Checked = true;
				}
				else if ((_align & ObsAlignment.Center) != 0)
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
				if ((_align & ObsAlignment.Left) != 0)
				{
					bottomleft.Checked = true;
				}
				else if ((_align & ObsAlignment.Center) != 0)
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
			RadioButton btn = (RadioButton)sender;
			string name = btn.Name;
			if (name.StartsWith("top"))
			{
				_align = ObsAlignment.Top;
				if (name.EndsWith("left"))
				{
					_align = _align | ObsAlignment.Left;
				}
				else if (name.EndsWith("center"))
				{
					_align = _align | ObsAlignment.Center;
				}
				else
				{
					_align = _align | ObsAlignment.Right;
				}
			}
			else if (name.StartsWith("center"))
			{
				_align = ObsAlignment.Center;
				if (name.EndsWith("left"))
				{
					_align = _align | ObsAlignment.Left;
				}
				else if (name.EndsWith("center"))
				{
					// nothing!
				}
				else
				{
					_align = _align | ObsAlignment.Right;
				}
			}
			else
			{
				_align = ObsAlignment.Bottom;
				if (name.EndsWith("left"))
				{
					_align = _align | ObsAlignment.Left;
				}
				else if (name.EndsWith("center"))
				{
					_align = _align | ObsAlignment.Center;
				}
				else
				{
					_align = _align | ObsAlignment.Right;
				}
			}
			if (Click != null) Click(_align);
		}
	}
}