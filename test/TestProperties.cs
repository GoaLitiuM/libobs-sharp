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

using OBS;
using System;
using System.Linq;
using System.Windows.Forms;
using test.Controls;

namespace test
{
	public partial class TestProperties : Form
	{
		public ObsSource Source;

		private ObsData sourceSettings;
		private ObsData oldSettings;
		private bool deferUpdate;

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

			sourceSettings = Source.GetSettings();
		}

		private ObsProperties properties;

		private void TestProperties_Load(object sender, EventArgs e)
		{
			ReloadProperties(Source);

			GenerateControls();

			//TODO: this shit is confusing!
			InitPreview(previewPanel.Width, previewPanel.Height, this.Handle);

			oldSettings = new ObsData(sourceSettings);

			// Attach close events

			okButton.Click += (o, args) =>
			{
				//Source.Update();
				Close();
			};
			cancelButton.Click += (o, args) =>
			{
				sourceSettings.Clear();
				UpdateSettings(Source, oldSettings);
				Close();
			};
		}

		private void TestProperties_FormClosed(object sender, FormClosedEventArgs e)
		{
			ClosePreview();
		}

		private void GenerateControls()
		{
			propertyPanel.Controls.Clear();

			// Add property controls

			ObsProperty[] propertyList = properties.GetPropertyList();
			foreach (var control in propertyList.Select(property => new PropertyControl(property, sourceSettings, PropertyModified)
			{
				Enabled = property.Enabled,
				Visible = property.Visible,
				Tag = property.Name
			}))
			{
				propertyPanel.Controls.Add(control);
			}

			propertyPanel.Refresh();
		}

		private void PropertyModified(bool refreshProperties)
		{
			if (!deferUpdate)
				UpdateSettings(Source, sourceSettings);

			if (!refreshProperties)
				return;

			GenerateControls();
		}

		//TODO: interface for other types of properties panels (settings, filters, properties)

		private void UpdateSettings(object obj, ObsData settings)
		{
			ObsSource source = (ObsSource)obj;

			source.Update(settings);
		}

		private void ReloadProperties(object obj)
		{
			ObsSource source = (ObsSource)obj;

			properties = source.GetProperties();
			deferUpdate = properties.Flags.HasFlag(ObsPropertiesFlags.DeferUpdate);
		}
	}
}