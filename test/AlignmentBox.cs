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

using OBS;
using System;
using System.Linq;
using System.Windows.Forms;

#endregion Usings

namespace test
{
	public partial class AlignmentBox : UserControl
	{
		public delegate void ClickHandler(ObsAlignment e);

		private ObsAlignment _align = ObsAlignment.Center;

		public AlignmentBox()
		{
			InitializeComponent();

			topleft.Tag = ObsAlignment.Top | ObsAlignment.Left;
			topcenter.Tag = ObsAlignment.Top | ObsAlignment.Center;
			topright.Tag = ObsAlignment.Top | ObsAlignment.Right;
			centerleft.Tag = ObsAlignment.Center | ObsAlignment.Left;
			center.Tag = ObsAlignment.Center;
			centerright.Tag = ObsAlignment.Center | ObsAlignment.Right;
			bottomleft.Tag = ObsAlignment.Bottom | ObsAlignment.Left;
			bottomcenter.Tag = ObsAlignment.Bottom | ObsAlignment.Center;
			bottomright.Tag = ObsAlignment.Bottom | ObsAlignment.Right;

			foreach (object control in panel.Controls)
			{
				((RadioButton)control).Click += btn_Click;
			}

			SetAlign();
		}

		public ObsAlignment Alignment
		{
			get { return _align; }
			set
			{
				_align = value;
				SetAlign();
			}
		}

		/// <summary>
		/// Fired when one of the alignment buttons is clicked
		/// e == alignment
		/// </summary>
		public new event ClickHandler Click;

		private void SetAlign()
		{
			foreach (RadioButton btn in panel.Controls.Cast<RadioButton>().Where(btn => (ObsAlignment)btn.Tag == Alignment))
			{
				btn.Checked = true;
				break;
			}
		}

		private void btn_Click(object sender, EventArgs e)
		{
			RadioButton btn = (RadioButton)sender;
			Alignment = (ObsAlignment)btn.Tag;
			if (Click != null)
			{
				Click(_align);
			}
		}
	}
}