/***************************************************************************
	Copyright (C) 2014-2015 by Ari Vuollet <ari.vuollet@kapsi.fi>

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
using System.Windows.Forms;

using OBS;

using test.Objects;
using test.Controls;

namespace test
{
	public partial class TestForm
	{
		private Presentation presentation;

		private PreviewPanel previewPanel;

		public TestForm()
		{
			InitializeComponent();
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
			if (Environment.Is64BitProcess)
				Text += " (64-bit)";
			else
				Text += " (32-bit)";

			presentation = new Presentation();

			// Bindings
			// Scene
			SceneListBox.DisplayMember = "Name";
			SceneListBox.ValueMember = "Items";
			SceneListBox.DataSource = presentation.Scenes;

			// Item
			ItemListBox.DisplayMember = "Name";

			// Source
			SourceListBox.DisplayMember = "Name";
			SourceListBox.DataSource = presentation.Sources;


			presentation.AddScene();

			ItemListBox.DataSource = SceneListBox.SelectedValue;

			var source = presentation.CreateSource("random", "some random source");
			presentation.AddSource(source);
			var item = presentation.CreateItem(source);
			presentation.AddItem(item);

			presentation.SetScene(SceneListBox.SelectedIndex);
			presentation.SetItem(ItemListBox.SelectedIndex);
			presentation.SetSource(SourceListBox.SelectedIndex);

			HideItemCheckBox.DataBindings.Add(
				new Binding("Checked", presentation.SelectedItem, "Visible", false, DataSourceUpdateMode.OnPropertyChanged));

			EnableSourceCheckBox.DataBindings.Add(
				new Binding("Checked", presentation.SelectedSource, "Enabled", false, DataSourceUpdateMode.OnPropertyChanged));

			MuteSourceCheckBox.DataBindings.Add(
				new Binding("Checked", presentation.SelectedSource, "Muted", false, DataSourceUpdateMode.OnPropertyChanged));


			// setup scene preview panel
			previewPanel = new PreviewPanel();
			previewPanel.Dock = DockStyle.Fill;

			topPanel.Controls.Add(previewPanel);
			previewPanel.Show();

			previewPanel.SetScene(presentation.SelectedScene);
		}

		private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			previewPanel.Dispose();
			presentation.Dispose();

			Obs.Shutdown();
		}

		#region SceneControls

		private void AddSceneButton_Click(object sender, EventArgs e)
		{
			presentation.AddScene();
			SceneListBox.SelectedIndex = 0;
		}

		private void DelSceneButton_Click(object sender, EventArgs e)
		{
			presentation.DelScene();
		}

		private void SceneListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			presentation.SetScene(SceneListBox.SelectedIndex);

			if (presentation.SelectedScene == null)
				return;

			ItemListBox.DataSource = SceneListBox.SelectedValue;
		}

		#endregion

		#region ItemControls

		private void AddItemButton_Click(object sender, EventArgs e)
		{
			var contextmenu = presentation.AddSourceContextMenu();
			contextmenu.ItemClicked += (o, args) =>
			{
				var tag = (Tuple<string, string>)args.ClickedItem.Tag;
				var source = presentation.CreateSource(tag.Item1, tag.Item2);
				var item = presentation.CreateItem(source);
				if (new TestProperties(source).ShowDialog() == DialogResult.OK)
				{
					presentation.AddSource(source);
					
					presentation.AddItem(item);

					ItemListBox.SelectedIndex = 0;
					SourceListBox.SelectedIndex = 0;
				}
				else
				{
					item.Remove();
					item.Dispose();
					source.Remove();
					source.Dispose();
				}
			};
			contextmenu.Show(this, PointToClient(Cursor.Position));

		}

		private void DelItemButton_Click(object sender, EventArgs e)
		{
			presentation.DelItem();
		}

		private void ItemListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			presentation.SetItem(ItemListBox.SelectedIndex);

			HideItemCheckBox.Enabled = presentation.SelectedItem != null;

			if (presentation.SelectedItem != null)
			{
				foreach (Item item in presentation.SelectedScene.Items)
				{
					item.Selected = false;
				}

				presentation.SelectedItem.Selected = true;

				HideItemCheckBox.DataBindings.Clear();
				HideItemCheckBox.DataBindings.Add(
					new Binding("Checked", presentation.SelectedItem, "Visible", false, DataSourceUpdateMode.OnPropertyChanged));
			}
		}

		private void ItemListBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (presentation.SelectedScene == null || presentation.SelectedItem == null || e.Button != MouseButtons.Right)
				return;

			var contextmenu = presentation.ItemContextMenu();
			contextmenu.Show(this, PointToClient(Cursor.Position));
		}

		#endregion

		#region SourceControls

		private void AddSourceButton_Click(object sender, EventArgs e)
		{
			var contextmenu = presentation.AddSourceContextMenu();
			contextmenu.ItemClicked += (o, args) =>
			{
				var tag = (Tuple<string, string>)args.ClickedItem.Tag;
				var source = presentation.CreateSource(tag.Item1, tag.Item2);
				var item = presentation.CreateItem(source);
				if (new TestProperties(source).ShowDialog() == DialogResult.OK)
				{
					presentation.AddSource(source);
					SourceListBox.SelectedIndex = 0;
				}
				else
				{
					source.Remove();
					source.Dispose();
				}
				item.Remove();
				item.Dispose();
			};
			contextmenu.Show(this, PointToClient(Cursor.Position));
		}

		private void DelSourceButton_Click(object sender, EventArgs e)
		{
			presentation.DelSource();
		}

		private void AddSourceToSceneButton_Click(object sender, EventArgs e)
		{
			if (presentation.SelectedSource == null) return;

			var item = presentation.CreateItem(presentation.SelectedSource);

			presentation.AddItem(item);

			ItemListBox.SelectedIndex = 0;
		}

		private void SourceListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			presentation.SetSource(SourceListBox.SelectedIndex);

			if (presentation.SelectedSource == null)
			{
				EnableSourceCheckBox.Enabled = false;
				MuteSourceCheckBox.Enabled = false;
				AddSourceToSceneButton.Enabled = false;
				return;
			}

			EnableSourceCheckBox.Enabled = true;
			MuteSourceCheckBox.Enabled = true;
			AddSourceToSceneButton.Enabled = true;

			EnableSourceCheckBox.DataBindings.Clear();
			EnableSourceCheckBox.DataBindings.Add(
				new Binding("Checked", presentation.SelectedSource, "Enabled", false, DataSourceUpdateMode.OnPropertyChanged));

			MuteSourceCheckBox.DataBindings.Clear();
			MuteSourceCheckBox.DataBindings.Add(
				new Binding("Checked", presentation.SelectedSource, "Muted", false, DataSourceUpdateMode.OnPropertyChanged));
		}

		private void SourceListBox_MouseDown(object sender, MouseEventArgs e)
		{
			// display the filter menu when rightclicking on a source in the sourcelistbox
			if (e.Button != MouseButtons.Right || presentation.SelectedSource == null) return;

			var contextmenu = presentation.SourceContextMenu();
			contextmenu.Show(this, PointToClient(Cursor.Position));
		}

		#endregion
	}
}
