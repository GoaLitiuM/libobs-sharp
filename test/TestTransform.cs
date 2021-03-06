﻿/***************************************************************************
	Copyright (C) 2014-2015 by Nick Thijssen <lamah83@gmail.com>

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

		private readonly Vector2 _oldPos;
		private readonly Vector2 _oldScale;
		private readonly float _oldRot;
		private readonly ObsAlignment _oldAlignment;

		private Vector2 ItemPosition
		{
			get { return new Vector2((float)xNumeric.Value, (float)yNumeric.Value); }
		}

		private Vector2 ItemScale
		{
			get { return new Vector2((float)wNumeric.Value, (float)hNumeric.Value); }
		}

		public TestTransform(ObsSceneItem item)
		{
			InitializeComponent();

			// Store scene item
			_selectedItem = item;

			// Set minmax on numerics
			// the rendercontext minmax are bigger than the decimal one so this is fine
			const decimal min = decimal.MinValue;
			const decimal max = decimal.MaxValue;

			xNumeric.Minimum = min;
			xNumeric.Maximum = max;

			yNumeric.Minimum = min;
			yNumeric.Maximum = max;

			wNumeric.Minimum = min;
			wNumeric.Maximum = max;

			hNumeric.Minimum = min;
			hNumeric.Maximum = max;

			// Populate Controls

			// Position
			_oldPos = _selectedItem.Position;
			xNumeric.Value = (decimal)_oldPos.x;
			yNumeric.Value = (decimal)_oldPos.y;

			// Scale
			_oldScale = _selectedItem.Scale;
			wNumeric.Value = (decimal)_oldScale.x;
			hNumeric.Value = (decimal)_oldScale.y;

			// Rotation
			_oldRot = _selectedItem.Rotation;
			Rotation.Rotation = (int)Math.Round(_oldRot);

			// Alignment
			_oldAlignment = _selectedItem.Alignment;
			Alignment.Alignment = _oldAlignment;

			// Delegates to change transform properties
			xNumeric.ValueChanged += (sender, args) => _selectedItem.Position = ItemPosition;
			yNumeric.ValueChanged += (sender, args) => _selectedItem.Position = ItemPosition;

			wNumeric.ValueChanged += (sender, args) => _selectedItem.Scale = ItemScale;
			hNumeric.ValueChanged += (sender, args) => _selectedItem.Scale = ItemScale;

			// These even handlers return the exact value we need
			// Do this in the future for all custom even handlers
			// Makes creating inline event handlers a breeze!
			Rotation.RotationChanged += rotation => _selectedItem.Rotation = rotation;

			Alignment.AlignmentChanged += alignment => _selectedItem.Alignment = alignment;

			// Close form methods

			okButton.Click += (sender, args) => Close();
			cancelButton.Click += (sender, args) =>
			{
				_selectedItem.Position = _oldPos;
				_selectedItem.Scale = _oldScale;
				_selectedItem.Rotation = _oldRot;
				_selectedItem.Alignment = _oldAlignment;

				Close();
			};
		}
	}
}