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

			var aligntags = new List<Tuple<RadioButton, ObsAlignment>>
			{
				Tuple.Create(topleft, ObsAlignment.Top | ObsAlignment.Left),
				Tuple.Create(topcenter, ObsAlignment.Top | ObsAlignment.Center),
				Tuple.Create(topright, ObsAlignment.Top | ObsAlignment.Right),
				Tuple.Create(centerleft, ObsAlignment.Center | ObsAlignment.Left),
				Tuple.Create(center, ObsAlignment.Center),
				Tuple.Create(centerright, ObsAlignment.Center | ObsAlignment.Right),
				Tuple.Create(bottomleft, ObsAlignment.Bottom | ObsAlignment.Left),
				Tuple.Create(bottomcenter, ObsAlignment.Bottom | ObsAlignment.Center),
				Tuple.Create(bottomright, ObsAlignment.Bottom | ObsAlignment.Right)
			};

			foreach (var aligntag in aligntags)
			{
				RadioButton radiobutton = aligntag.Item1;
				ObsAlignment alignment = aligntag.Item2;

				radiobutton.Click += btn_Click;
				radiobutton.Tag = alignment;
			}
			
			SetAlign();
		}

		private void SetAlign()
		{
			foreach (var btn in panel.Controls.Cast<RadioButton>().Where(btn => (ObsAlignment)btn.Tag == Alignment))
			{
				btn.Checked = true;
				break;
			}
		}

		private void btn_Click(object sender, EventArgs e)
		{
			RadioButton btn = (RadioButton)sender;
			Alignment = (ObsAlignment)btn.Tag;
			if (Click != null) Click(_align);
		}
	}
}