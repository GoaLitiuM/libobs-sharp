/***************************************************************************
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

using System;
using System.Linq;
using System.Windows.Forms;
using OBS;
using test.Controls;

namespace test
{
	public partial class TestProperties : Form
	{
		public ObsSource Source;
		
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
			// Create Controls
			GenerateControls();

			//TODO: this shit is confusing!
			InitPreview(previewPanel.Width, previewPanel.Width, this.Handle);

			// Attach close events
			// TODO: make it so cancel actually reverts stuff
			okButton.Click += (o, args) =>
			{
				Source.Update();
				Close();
			};
			cancelButton.Click += (o, args) => Close();
		}
		
		private void GenerateControls()
		{
			// Add property controls
			ObsData settings = Source.GetSettings();
			foreach (var proppanel in _properties.Select(property => new PropertyPanel(property,settings)
			{
				Enabled = property.Enabled,
				Visible = property.Visible,
				Tag = property.Name
			}))
			{
				propertyPanel.Controls.Add(proppanel);
			}
			propertyPanel.Refresh();
		}
	}
}
