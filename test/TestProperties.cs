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
using OBS.Graphics;
using System;
using System.Windows.Forms;
using test.Controls;

namespace test
{
	public partial class TestProperties : Form
	{
		private DisplayPanel previewPanel;
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
		}

		private void TestProperties_Load(object sender, System.EventArgs e)
		{
			previewPanel = new DisplayPanel();
			previewPanel.displayCreated += () =>
			{
				previewPanel.Display.AddDrawCallback(RenderPreview);
			};

			topPanel.Controls.Add(previewPanel);
			previewPanel.Dock = DockStyle.Fill;
			previewPanel.Show();
		}

		private void TestProperties_FormClosed(object sender, FormClosedEventArgs e)
		{
			previewPanel.Display.RemoveDrawCallback(RenderPreview);
			previewPanel.Dispose();
		}

		private void RenderPreview(IntPtr data, uint cx, uint cy)
		{
			int newW = (int)cx;
			int newH = (int)cy;
			int sourceWidth = (int)source.Width;
			int sourceHeight = (int)source.Height;
			float previewAspect = (float)cx / cy;
			float sourceAspect = (float)sourceWidth / sourceHeight;

			//calculate new width and height for source to make it fit inside the preview area
			if (previewAspect > sourceAspect)
				newW = (int)(cy * sourceAspect);
			else
				newH = (int)(cx / sourceAspect);

			int centerX = ((int)cx - newW) / 2;
			int centerY = ((int)cy - newH) / 2;

			GS.ViewportPush();
			GS.ProjectionPush();

			//setup orthographic projection of the source
			GS.Ortho(0.0f, sourceWidth, 0.0f, sourceHeight, -100.0f, 100.0f);
			GS.SetViewport(centerX, centerY, newW, newH);

			//render source content
			source.Render();

			GS.ProjectionPop();
			GS.ViewportPop();

			GS.LoadVertexBuffer(null);
		}
	}
}