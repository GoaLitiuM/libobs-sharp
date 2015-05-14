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

using System.Windows.Forms;

namespace test
{
	partial class TestFilter : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.propertiesPanel = new System.Windows.Forms.TableLayoutPanel();
			this.bottomPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.undoButton = new System.Windows.Forms.Button();
			this.defaultButton = new System.Windows.Forms.Button();
			this.FilterListBox = new System.Windows.Forms.ListBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.AddFilterButton = new System.Windows.Forms.Button();
			this.RemoveFilterButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.propertyPanel = new System.Windows.Forms.Panel();
			this.previewPanel = new System.Windows.Forms.Panel();
			this.propertiesPanel.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// propertiesPanel
			// 
			this.propertiesPanel.AutoSize = true;
			this.propertiesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.propertiesPanel.ColumnCount = 2;
			this.propertiesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.propertiesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.propertiesPanel.Controls.Add(this.bottomPanel, 1, 1);
			this.propertiesPanel.Controls.Add(this.FilterListBox, 0, 0);
			this.propertiesPanel.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.propertiesPanel.Controls.Add(this.tableLayoutPanel1, 1, 0);
			this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertiesPanel.Location = new System.Drawing.Point(0, 0);
			this.propertiesPanel.Name = "propertiesPanel";
			this.propertiesPanel.RowCount = 2;
			this.propertiesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.propertiesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.propertiesPanel.Size = new System.Drawing.Size(797, 616);
			this.propertiesPanel.TabIndex = 9;
			// 
			// bottomPanel
			// 
			this.bottomPanel.AutoSize = true;
			this.bottomPanel.Controls.Add(this.cancelButton);
			this.bottomPanel.Controls.Add(this.okButton);
			this.bottomPanel.Controls.Add(this.undoButton);
			this.bottomPanel.Controls.Add(this.defaultButton);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.bottomPanel.Location = new System.Drawing.Point(203, 587);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(591, 26);
			this.bottomPanel.TabIndex = 10;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(513, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(432, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// undoButton
			// 
			this.undoButton.Location = new System.Drawing.Point(351, 3);
			this.undoButton.Name = "undoButton";
			this.undoButton.Size = new System.Drawing.Size(75, 23);
			this.undoButton.TabIndex = 7;
			this.undoButton.Text = "Undo";
			this.undoButton.UseVisualStyleBackColor = true;
			// 
			// defaultButton
			// 
			this.defaultButton.Location = new System.Drawing.Point(270, 3);
			this.defaultButton.Name = "defaultButton";
			this.defaultButton.Size = new System.Drawing.Size(75, 23);
			this.defaultButton.TabIndex = 6;
			this.defaultButton.Text = "Default";
			this.defaultButton.UseVisualStyleBackColor = true;
			// 
			// FilterListBox
			// 
			this.FilterListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FilterListBox.FormattingEnabled = true;
			this.FilterListBox.Location = new System.Drawing.Point(3, 3);
			this.FilterListBox.Name = "FilterListBox";
			this.FilterListBox.Size = new System.Drawing.Size(194, 578);
			this.FilterListBox.TabIndex = 1;
			this.FilterListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FilterListBox_MouseUp);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.AddFilterButton);
			this.flowLayoutPanel1.Controls.Add(this.RemoveFilterButton);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 587);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(194, 26);
			this.flowLayoutPanel1.TabIndex = 11;
			// 
			// AddFilterButton
			// 
			this.AddFilterButton.Location = new System.Drawing.Point(3, 3);
			this.AddFilterButton.Name = "AddFilterButton";
			this.AddFilterButton.Size = new System.Drawing.Size(91, 23);
			this.AddFilterButton.TabIndex = 0;
			this.AddFilterButton.Text = "Add Filter";
			this.AddFilterButton.UseVisualStyleBackColor = true;
			// 
			// RemoveFilterButton
			// 
			this.RemoveFilterButton.Location = new System.Drawing.Point(100, 3);
			this.RemoveFilterButton.Name = "RemoveFilterButton";
			this.RemoveFilterButton.Size = new System.Drawing.Size(91, 23);
			this.RemoveFilterButton.TabIndex = 1;
			this.RemoveFilterButton.Text = "Remove Filter";
			this.RemoveFilterButton.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.propertyPanel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.previewPanel, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(203, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(591, 578);
			this.tableLayoutPanel1.TabIndex = 12;
			this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
			// 
			// propertyPanel
			// 
			this.propertyPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.propertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyPanel.Location = new System.Drawing.Point(3, 292);
			this.propertyPanel.Name = "propertyPanel";
			this.propertyPanel.Size = new System.Drawing.Size(585, 283);
			this.propertyPanel.TabIndex = 1;
			// 
			// previewPanel
			// 
			this.previewPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.previewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.previewPanel.Location = new System.Drawing.Point(3, 3);
			this.previewPanel.Name = "previewPanel";
			this.previewPanel.Size = new System.Drawing.Size(585, 283);
			this.previewPanel.TabIndex = 2;
			// 
			// TestFilter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(797, 616);
			this.ControlBox = false;
			this.Controls.Add(this.propertiesPanel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TestFilter";
			this.ShowIcon = false;
			this.Text = "TestFilter";
			this.propertiesPanel.ResumeLayout(false);
			this.propertiesPanel.PerformLayout();
			this.bottomPanel.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TableLayoutPanel propertiesPanel;
		private ListBox FilterListBox;
		private FlowLayoutPanel bottomPanel;
		private Button cancelButton;
		private Button okButton;
		private Button defaultButton;
		private FlowLayoutPanel flowLayoutPanel1;
		private Button AddFilterButton;
		private Button RemoveFilterButton;
		private Button undoButton;
		private TableLayoutPanel tableLayoutPanel1;
		private Panel propertyPanel;
		private Panel previewPanel;
	}
}