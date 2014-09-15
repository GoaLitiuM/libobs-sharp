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


using OBS;
using System;
using System.Windows.Forms;

namespace test
{
	public partial class TestTransform : Form
	{
		private readonly ObsSceneItem _selectedItem;

		private readonly libobs.vec2 _oldPos;
		private readonly libobs.vec2 _oldScale;
		private readonly float _oldRot;
		private readonly ObsAlignment _oldAlignment;

		private bool _ok;
		private bool _cancel;

		private libobs.vec2 ItemPosition
		{
			get { return new libobs.vec2((float)xNumeric.Value, (float)yNumeric.Value); }
		}

		private libobs.vec2 ItemScale
		{
			get { return new libobs.vec2((float)wNumeric.Value, (float)hNumeric.Value); }
		}

		public TestTransform(ObsSceneItem item)
		{
			InitializeComponent();

			// Store scene item
			_selectedItem = item;

			// Set minmax on numerics
			xNumeric.Minimum = decimal.MinValue;
			xNumeric.Maximum = decimal.MaxValue;

			yNumeric.Minimum = decimal.MinValue;
			yNumeric.Maximum = decimal.MaxValue;

			wNumeric.Minimum = decimal.MinValue;
			wNumeric.Maximum = decimal.MaxValue;

			hNumeric.Minimum = decimal.MinValue;
			hNumeric.Maximum = decimal.MaxValue;

			// Populate Controls

			// Position
			_oldPos = _selectedItem.GetPosition();
			xNumeric.Value = (decimal)_oldPos.x;
			yNumeric.Value = (decimal)_oldPos.y;

			// Scale
			_oldScale = _selectedItem.GetScale();
			wNumeric.Value = (decimal)_oldScale.x;
			hNumeric.Value = (decimal)_oldScale.y;

			// Rotation
			_oldRot = _selectedItem.GetRotation();
			Rotation.Rotation = (int)Math.Round(_oldRot);

			// Alignment
			_oldAlignment = _selectedItem.GetAlignment();
			Alignment.Alignment = _oldAlignment;

			// Delegates to change transform properties
			xNumeric.ValueChanged += (sender, args) => _selectedItem.SetPosition(ItemPosition);
			yNumeric.ValueChanged += (sender, args) => _selectedItem.SetPosition(ItemPosition);

			wNumeric.ValueChanged += (sender, args) => _selectedItem.SetScale(ItemScale);
			hNumeric.ValueChanged += (sender, args) => _selectedItem.SetScale(ItemScale);

			// These even handlers return the exact value we need
			// Do this in the future for all custom even handlers
			// Makes creating inline event handlers a breeze!
			Rotation.RotationChanged += rotation => _selectedItem.SetRotation(rotation);

			Alignment.AlignmentChanged += alignment => _selectedItem.SetAlignment(alignment);

			// Close form methods
			cancelButton.Click += (sender, args) => Close();

			okButton.Click += (sender, args) =>
			{
				_ok = true;
				Close();
			};

			Closing += (sender, args) =>
			{
				// if ok wasnt clicked flush changes
				if (_ok) return;

				_selectedItem.SetPosition(_oldPos);
				_selectedItem.SetScale(_oldScale);
				_selectedItem.SetRotation(_oldRot);
				_selectedItem.SetAlignment(_oldAlignment);
			};
		}
	}
}