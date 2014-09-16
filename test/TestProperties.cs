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

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OBS;
using test.Controls;

namespace test
{
	public partial class TestProperties
	{
		public readonly ObsSource Source;

		private TestProperties()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Create a property dialog for an existing source
		/// </summary>
		/// <param name="source">Source of type ObsSource</param>
		public TestProperties(ObsSource source)
			: this()
		{
			Source = source;
			_properties = Obs.GetSourceProperties(source);
		}

		private ObsProperty[] _properties;

		private void TestProperties_Load(object sender, EventArgs e)
		{
			// Add property controls
			foreach (var proppanel in _properties.Select(property => new PropertyPanel(property)
			{
				Enabled = property.Enabled,
				Visible = property.Visible,
				Tag = property.Name
			}))
			{
				propertyPanel.Controls.Add(proppanel);
			}
			propertyPanel.Refresh();


			// Scale Preview Panel
			// top panel ratio
			float backratio = (float)previewBackPanel.Width / previewBackPanel.Height;

			// source size
			int width = (int)Source.Width;
			int height = (int)Source.Height;

			// source ratio
			float previewratio = (float)width / height;

			// padding
			const int padding = 1;

			if (backratio > previewratio)
			{
				previewPanel.Height = height - padding;
				previewPanel.Width = (int)Math.Round(height * previewratio) - padding;
			}
			else
			{
				previewPanel.Width = width - padding;
				previewPanel.Height = (int)Math.Round(width / previewratio) - padding;
			}

			previewBackPanel.Height = (int)(previewPanel.Height * 1.25);
			previewBackPanel.Width = (int)(previewPanel.Width * 1.25);

			// Center previewpanel in backpanel
			var offsetx = (previewBackPanel.Width - previewPanel.Width) / 2;
			var offsety = (previewBackPanel.Height - previewPanel.Height) / 2;

			previewPanel.Location = new Point(offsetx, offsety);

			InitPreview(previewPanel.Width, previewPanel.Height, previewPanel.Handle);

			// Assign close form event handlers
			okButton.Click += (o, args) => Close();
			cancelButton.Click += (o, args) => Close();
			FormClosed += (o, args) => ClosePreview();
		}
	}
}
