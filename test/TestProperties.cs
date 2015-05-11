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
using System.Windows.Forms;
using test.Controls;

namespace test
{
	public partial class TestProperties : Form
	{
		public ObsSource Source { get { return source; } }

		private PropertiesView view;
		private ObsSource source;
		private ObsData sourceSettings;
		private ObsData oldSettings;

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
			this.source = source;
			sourceSettings = source.GetSettings();
			oldSettings = new ObsData(sourceSettings);

			view = new PropertiesView(sourceSettings, source,
				source.GetProperties, source.Update);

			propertyPanel.Controls.Add(view);

			previewPanel.SizeChanged += (sender, args) =>
			{
				ResizePreview((uint)previewPanel.Width, (uint)previewPanel.Height);
			};

			okButton.Click += (o, args) =>
			{
				view.UpdateSettings();
				DialogResult = DialogResult.OK;
				Close();
			};
			cancelButton.Click += (o, args) =>
			{
				sourceSettings.Clear();
				source.Update(oldSettings);
				DialogResult = DialogResult.Cancel;
				Close();
			};

			Load += (sender, args) =>
			{
				InitPreview((uint)previewPanel.Width, (uint)previewPanel.Height, this.Handle);
			};

			FormClosed += (sender, args) =>
			{
				ClosePreview();
			};
		}
	}
}