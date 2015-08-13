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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using OBS;

using test.Objects;

namespace test
{
	public partial class TestForm
	{
		private Controls.DisplayPanel previewPanel;
		private Presentation _presentation;

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

			InitPrimitives();

			_presentation = new Presentation();

			// Bindings
			// Scene
			SceneListBox.DisplayMember = "Name";
			SceneListBox.ValueMember = "Items";
			SceneListBox.DataSource = _presentation.Scenes;

			// Item
			ItemListBox.DisplayMember = "Name";

			// Source
			SourceListBox.DisplayMember = "Name";
			SourceListBox.DataSource = _presentation.Sources;


			_presentation.AddScene();

			ItemListBox.DataSource = SceneListBox.SelectedValue;

			var source = _presentation.CreateSource("random", "some random source");
			_presentation.AddSource(source);
			var item = _presentation.CreateItem(source);
			_presentation.AddItem(item);

			_presentation.SetScene(SceneListBox.SelectedIndex);
			_presentation.SetItem(ItemListBox.SelectedIndex);
			_presentation.SetSource(SourceListBox.SelectedIndex);

			HideItemCheckBox.DataBindings.Add(
				new Binding("Checked", _presentation.SelectedItem, "Visible", false, DataSourceUpdateMode.OnPropertyChanged));

			EnableSourceCheckBox.DataBindings.Add(
				new Binding("Checked", _presentation.SelectedSource, "Enabled", false, DataSourceUpdateMode.OnPropertyChanged));

			MuteSourceCheckBox.DataBindings.Add(
				new Binding("Checked", _presentation.SelectedSource, "Muted", false, DataSourceUpdateMode.OnPropertyChanged));


			// setup scene preview panel
			previewPanel = new Controls.DisplayPanel();
			previewPanel.displayCreated += () =>
			{
				previewPanel.Display.AddDrawCallback(RenderMain);
			};

			topPanel.Controls.Add(previewPanel);
			previewPanel.Dock = DockStyle.Fill;
			previewPanel.Show();
		}

		private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			previewPanel.Display.RemoveDrawCallback(RenderMain);
			previewPanel.Dispose();

			_presentation.Dispose();

			if (_boxPrimitive != null)
				_boxPrimitive.Dispose();
			if (_circlePrimitive != null)
				_circlePrimitive.Dispose();

			Obs.Shutdown();
		}

		#region SceneControls

		private void AddSceneButton_Click(object sender, EventArgs e)
		{
			_presentation.AddScene();
			SceneListBox.SelectedIndex = 0;
		}

		private void DelSceneButton_Click(object sender, EventArgs e)
		{
			_presentation.DelScene();
		}

		private void SceneListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_presentation.SetScene(SceneListBox.SelectedIndex);

			if (_presentation.SelectedScene == null)
				return;

			ItemListBox.DataSource = SceneListBox.SelectedValue;
		}

		#endregion

		#region ItemControls

		private void AddItemButton_Click(object sender, EventArgs e)
		{
			var contextmenu = _presentation.AddSourceContextMenu();
			contextmenu.ItemClicked += (o, args) =>
			{
				var tag = (Tuple<string, string>)args.ClickedItem.Tag;
				var source = _presentation.CreateSource(tag.Item1, tag.Item2);
				var item = _presentation.CreateItem(source);
				if (new TestProperties(source).ShowDialog() == DialogResult.OK)
				{
					_presentation.AddSource(source);
					
					_presentation.AddItem(item);

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
			_presentation.DelItem();
		}

		private void ItemListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_presentation.SetItem(ItemListBox.SelectedIndex);

			HideItemCheckBox.Enabled = _presentation.SelectedItem != null;

			if (_presentation.SelectedItem != null)
			{
				foreach (Item item in _presentation.SelectedScene.Items)
				{
					item.Selected = false;
				}

				_presentation.SelectedItem.Selected = true;

				HideItemCheckBox.DataBindings.Clear();
				HideItemCheckBox.DataBindings.Add(
					new Binding("Checked", _presentation.SelectedItem, "Visible", false, DataSourceUpdateMode.OnPropertyChanged));
			}
		}

		private void ItemListBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (_presentation.SelectedScene == null || _presentation.SelectedItem == null || e.Button != MouseButtons.Right)
				return;

			var contextmenu = _presentation.ItemContextMenu();
			contextmenu.Show(this, PointToClient(Cursor.Position));
		}

		#endregion

		#region SourceControls

		private void AddSourceButton_Click(object sender, EventArgs e)
		{
			var contextmenu = _presentation.AddSourceContextMenu();
			contextmenu.ItemClicked += (o, args) =>
			{
				var tag = (Tuple<string, string>)args.ClickedItem.Tag;
				var source = _presentation.CreateSource(tag.Item1, tag.Item2);
				if (new TestProperties(source).ShowDialog() == DialogResult.OK)
				{
					_presentation.AddSource(source);
					SourceListBox.SelectedIndex = 0;
				}
				else
				{
					source.Remove();
					source.Dispose();
				}
			};
			contextmenu.Show(this, PointToClient(Cursor.Position));
		}

		private void DelSourceButton_Click(object sender, EventArgs e)
		{
			_presentation.DelSource();
		}

		private void AddSourceToSceneButton_Click(object sender, EventArgs e)
		{
			if (_presentation.SelectedSource == null) return;

			var item = _presentation.CreateItem(_presentation.SelectedSource);

			_presentation.AddItem(item);

			ItemListBox.SelectedIndex = 0;
		}

		private void SourceListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			_presentation.SetSource(SourceListBox.SelectedIndex);

			if (_presentation.SelectedSource == null)
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
				new Binding("Checked", _presentation.SelectedSource, "Enabled", false, DataSourceUpdateMode.OnPropertyChanged));

			MuteSourceCheckBox.DataBindings.Clear();
			MuteSourceCheckBox.DataBindings.Add(
				new Binding("Checked", _presentation.SelectedSource, "Muted", false, DataSourceUpdateMode.OnPropertyChanged));
		}

		private void SourceListBox_MouseDown(object sender, MouseEventArgs e)
		{
			// display the filter menu when rightclicking on a source in the sourcelistbox
			if (e.Button != MouseButtons.Right || _presentation.SelectedSource == null) return;

			var contextmenu = _presentation.SourceContextMenu();
			contextmenu.Show(this, PointToClient(Cursor.Position));
		}

		#endregion
	}
}
