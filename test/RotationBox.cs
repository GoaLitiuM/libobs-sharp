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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

#endregion

namespace test
{
	public sealed partial class RotationBox : UserControl
	{
		private bool _mouseDown;

		private int _rotation;

		public RotationBox()
		{
			InitializeComponent();
			DoubleBuffered = true;

			Paint += backPanel_Paint;

			MouseDown += delegate(object sender, MouseEventArgs e)
			{
				_mouseDown = true;
				Rotation = (int) Math.Round(GetRotation(CenterPoint, e.Location)) + 90;
			};

			MouseUp += delegate { _mouseDown = false; };

			MouseMove += delegate(object sender, MouseEventArgs e)
			{
				if (_mouseDown)
				{
					Rotation = (int) Math.Round(GetRotation(CenterPoint, e.Location)) + 90;
				}
			};
		}

		public bool Debug { get; set; }

		public int Rotation
		{
			get { return _rotation + 90; }
			set
			{
				int rot = value;
				// adjust value so it loops back to 0
				if (rot > 359)
				{
					rot = rot - 360;
				}
				else if (rot < 0)
				{
					rot = 360 - rot;
				}
				// internal value offset by 90 degress due to radians conversion
				_rotation = rot - 90;
				// repaint control
				Refresh();
			}
		}

		private void backPanel_Paint(object sender, PaintEventArgs e)
		{
			List<int> pipped = new List<int>();

			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;

			int radius = (int) Math.Round(((float) Width/2)*0.90);

			#region Pips

			for (int i = 0; i < 360; i += 45)
			{
				float radians = DegreeToRadian(i);
				Size rectsize = new Size(5, 5);
				Point rectpost = GetOffsetPosition(GetPosition(radians, radius), rectsize);
				Rectangle rect = new Rectangle(rectpost, rectsize);

				graphics.DrawRectangle(new Pen(Color.Black), rect);

				pipped.Add(i);
			}

			for (int i = 0; i < 360; i += 15)
			{
				if (pipped.Contains(i))
				{
					continue;
				}
				float radians = DegreeToRadian(i);
				Size pipsize = new Size(4, 4);
				Point pippos = GetOffsetPosition(GetPosition(radians, radius), pipsize);
				Rectangle piprect = new Rectangle(pippos, pipsize);
				graphics.FillEllipse(new SolidBrush(Color.Black), piprect);
			}

			#endregion

			#region Rotation Indicator

			int indicatorradius = (int) Math.Floor(((float) Width/2)*0.70);
			Pen indicatorpen = new Pen(Color.Black, 3);
			Point p2 = GetPosition(DegreeToRadian(_rotation), indicatorradius);

			graphics.DrawLine(indicatorpen, CenterPoint, p2);

			#endregion

			// Paint degrees in control
			if (Debug)
			{
				graphics.DrawString(
					Rotation.ToString(CultureInfo.InvariantCulture),
					new Font("Consolas", 12),
					new SolidBrush(Color.Black),
					new PointF(CenterPoint.X, CenterPoint.Y));
			}
		}

		#region Helpers

		private Point CenterPoint
		{
			get { return GetCenter(this); }
		}

		private float GetRotation(Point center, Point point)
		{
			// Get distance
			int qx = center.X - point.X;
			int qy = center.X - point.Y;

			// Get absolute distance
			int x = Math.Abs(qx);
			int y = Math.Abs(qy);

			// Get Degrees
			float ratio = (float) x/y;

			// Calculate degrees
			float degrees = RadianToDegree((float) Math.Atan(ratio));

			if (qx != x)
			{
				if (qy != y)
				{
					degrees = 90 - degrees;
				}
				else
				{
					degrees = 270 + degrees;
				}
			}
			else
			{
				if (qy != y)
				{
					degrees = 90 + degrees;
				}
				else
				{
					degrees = 270 - degrees;
				}
			}

			return degrees;
		}

		private Point GetPosition(float radians, int radius)
		{
			return new Point
			{
				Y = (int) Math.Round(CenterPoint.Y + radius*Math.Sin(radians)),
				X = (int) Math.Round(CenterPoint.X + radius*Math.Cos(radians))
			};
		}

		private Point GetOffsetPosition(Point position, Size b)
		{
			Point offset = new Point(b.Width/2, b.Height/2);
			return new Point(position.X - offset.X, position.Y - offset.Y);
		}

		private Point GetCenter(Control p)
		{
			return new Point(p.Width/2, p.Height/2);
		}

		private float DegreeToRadian(float angle)
		{
			return (float) (Math.PI*angle/180.0);
		}

		private float RadianToDegree(float angle)
		{
			return (float) (angle*(180.0/Math.PI));
		}

		#endregion
	}
}