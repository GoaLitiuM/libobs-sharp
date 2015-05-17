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
		private PropertiesView view;
		private ObsSource source;
		private ObsData sourceSettings;

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

			view = new PropertiesView(sourceSettings, source, source.GetProperties, source.GetDefaults, source.Update);
			propertyPanel.Controls.Add(view);

			Load += (sender, args) =>
			{
				InitPreview((uint)previewPanel.Width, (uint)previewPanel.Height, previewPanel.Handle);
			};

			previewPanel.SizeChanged += (sender, args) =>
			{
				ResizePreview((uint)previewPanel.Width, (uint)previewPanel.Height);
			};

			undoButton.Click += (sender, args) =>
			{
				view.ResetChanges();
			};

			defaultButton.Click += (sender, args) =>
			{
				view.ResetToDefaults();
			};

			okButton.Click += (o, args) =>
			{
				view.UpdateSettings();
				DialogResult = DialogResult.OK;
				Close();
			};

			cancelButton.Click += (o, args) =>
			{
				view.ResetChanges();
				DialogResult = DialogResult.Cancel;
				Close();
			};

			FormClosed += (sender, args) =>
			{
				ClosePreview();
			};
		}
	}
}