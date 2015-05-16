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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using OBS;

using test.Controls;
using test.Utility;

namespace test
{
	public partial class TestFilter
	{
		private Source SelectedFilter { get; set; }

		private PropertiesView _view;
		private Source source { get; set; }
		private readonly ObsData _sourceSettings;
		private readonly ObsData _oldSettings;

		private readonly List<ObsData> _oldFilterSettings = new List<ObsData>();

		private TestFilter()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Create a property dialog for an existing source
		/// </summary>
		/// <param name="source">Source of type ObsSource</param>
		public TestFilter(Source source) : this()
		{
			this.source = source;
			_sourceSettings = source.GetSettings();
			_oldSettings = new ObsData(_sourceSettings);

			FilterListBox.DisplayMember = "Name";
			FilterListBox.DataSource = source.Filters;

			if (source.Filters.Any())
			{
				foreach (var filter in source.Filters)
					_oldFilterSettings.Add(filter.GetSettings());

				Select(source.Filters.First());
			}

			Load += (sender, args) =>
			{
				InitPreview((uint)previewPanel.Width, (uint)previewPanel.Height, previewPanel.Handle);
			};

			previewPanel.SizeChanged += (sender, args) =>
			{
				ResizePreview((uint)previewPanel.Width, (uint)previewPanel.Height);
			};

			FormClosed += (sender, args) =>
			{
				ClosePreview();
			};

			defaultButton.Click += (sender, args) =>
			{
				SelectedFilter.GetSettings().Clear();
				SelectedFilter.Update(SelectedFilter.GetDefaults());
				_view.ReloadProperties();
			};

			okButton.Click += (o, args) =>
			{
				_view.UpdateSettings();
				DialogResult = DialogResult.OK;
				Close();
			};

			cancelButton.Click += (o, args) =>
			{
				_sourceSettings.Clear();
				source.Update(_oldSettings);
				DialogResult = DialogResult.Cancel;
				Close();
			};

			undoButton.Click += (sender, args) =>
			{
				SelectedFilter.GetSettings().Clear();
				SelectedFilter.Update(_oldFilterSettings[source.Filters.IndexOf(SelectedFilter)]);
				_view.ReloadProperties();
			};

			AddFilterButton.Click += (sender, args) =>
			{
				FilterMenu().Show(this, PointToClient(Cursor.Position));
			};

			RemoveFilterButton.Click += (sender, args) =>
			{
				if (SelectedFilter != null)
				{
					source.RemoveFilter(SelectedFilter);
					_oldFilterSettings.RemoveAt(source.Filters.IndexOf(SelectedFilter));
				}
			};
		}

		private void PopulateControls(Source filter)
		{
			if (propertyPanel.Controls.Contains(_view))
				propertyPanel.Controls.Remove(_view);

			_view = new PropertiesView(filter.GetSettings(), filter, filter.GetProperties, filter.Update);
			propertyPanel.Controls.Add(_view);
		}

		private void Select(Source filter)
		{
			if (filter == SelectedFilter)
				return;

			SelectedFilter = filter;
			FilterListBox.SelectedIndex = source.Filters.IndexOf(filter);
			PopulateControls(SelectedFilter);
		}

		private void Select(int filterindex)
		{
			if (filterindex != -1)
			{
				var filter = source.Filters[filterindex];
				if (SelectedFilter == filter)
					return;

				SelectedFilter = filter;
				FilterListBox.SelectedIndex = filterindex;
				PopulateControls(SelectedFilter);
			}
			else
			{
				SelectedFilter = null;
			}
		}

		ContextMenuStrip FilterMenu()
		{
			var filtermenu = new ContextMenuStrip();
			foreach (string filterType in Obs.GetSourceFilterTypes())
			{
				string displayname = Obs.GetSourceTypeDisplayName(ObsSourceType.Filter, filterType) + source.Filters.Count + 1;

				var menuitem = new ToolStripMenuItem(displayname + " (" + filterType + ")");

				menuitem.Click += (o, args) =>
				{
					var filter = new Source(ObsSourceType.Filter, filterType, displayname);
					source.AddFilter(filter);
					_oldFilterSettings.Insert(0, filter.GetSettings());
					Select(filter);
				};

				filtermenu.Items.Add(menuitem);
			}

			return filtermenu;
		}

		private void FilterListBox_MouseUp(object sender, MouseEventArgs e)
		{
			var index = FilterListBox.IndexFromPoint(e.Location);
			Select(index);

			if (e.Button != MouseButtons.Right)
				return;

			var contextmenu = new ContextMenuStrip { Renderer = new AccessKeyMenuStripRenderer() };
			var add = new ToolStripMenuItem("Add...") { DropDown = FilterMenu() };

			var remove = new ToolStripMenuItem("Remove");
			remove.Click += (o, args) =>
			{
				source.RemoveFilter(SelectedFilter);
			};

			var top = new ToolStripMenuItem("Move to &Top");
			top.Click += (o, args) =>
			{
				Select(source.MoveItem(SelectedFilter, obs_order_movement.OBS_ORDER_MOVE_TOP));
			};

			var up = new ToolStripMenuItem("Move &Up");
			up.Click += (o, args) =>
			{
				Select(source.MoveItem(SelectedFilter, obs_order_movement.OBS_ORDER_MOVE_UP));
			};

			var down = new ToolStripMenuItem("Move &Down");
			down.Click += (o, args) =>
			{
				Select(source.MoveItem(SelectedFilter, obs_order_movement.OBS_ORDER_MOVE_DOWN));
			};

			var bottom = new ToolStripMenuItem("Move to &Bottom");
			bottom.Click += (o, args) =>
			{
				Select(source.MoveItem(SelectedFilter, obs_order_movement.OBS_ORDER_MOVE_BOTTOM));
			};

			if (SelectedFilter == null)
			{
				remove.Enabled = false;
				top.Enabled = false;
				up.Enabled = false;
				down.Enabled = false;
				bottom.Enabled = false;
			}

			if (index == 0)
			{
				top.Enabled = false;
				up.Enabled = false;
			}
			if (index == source.Filters.Count - 1)
			{
				down.Enabled = false;
				bottom.Enabled = false;
			}

			contextmenu.Items.AddRange(new ToolStripItem[]
			                           {
				                           add,
										   remove,
										   new ToolStripSeparator(), 
										   top,
										   up,
										   down,
										   bottom
			                           });

			contextmenu.Show(this, PointToClient(Cursor.Position));
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}