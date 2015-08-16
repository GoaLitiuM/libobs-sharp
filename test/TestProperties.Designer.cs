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

namespace test
{
	partial class TestProperties
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
			this.bottomPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.defaultButton = new System.Windows.Forms.Button();
			this.undoButton = new System.Windows.Forms.Button();
			this.propertiesPanel = new System.Windows.Forms.TableLayoutPanel();
			this.propertyPanel = new System.Windows.Forms.Panel();
			this.topPanel = new System.Windows.Forms.Panel();
			this.bottomPanel.SuspendLayout();
			this.propertiesPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// bottomPanel
			// 
			this.bottomPanel.AutoSize = true;
			this.bottomPanel.Controls.Add(this.cancelButton);
			this.bottomPanel.Controls.Add(this.okButton);
			this.bottomPanel.Controls.Add(this.defaultButton);
			this.bottomPanel.Controls.Add(this.undoButton);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.bottomPanel.Location = new System.Drawing.Point(0, 457);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(659, 29);
			this.bottomPanel.TabIndex = 7;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(581, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(500, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// defaultButton
			// 
			this.defaultButton.Location = new System.Drawing.Point(419, 3);
			this.defaultButton.Name = "defaultButton";
			this.defaultButton.Size = new System.Drawing.Size(75, 23);
			this.defaultButton.TabIndex = 6;
			this.defaultButton.Text = "Default";
			this.defaultButton.UseVisualStyleBackColor = true;
			// 
			// undoButton
			// 
			this.undoButton.Location = new System.Drawing.Point(338, 3);
			this.undoButton.Name = "undoButton";
			this.undoButton.Size = new System.Drawing.Size(75, 23);
			this.undoButton.TabIndex = 5;
			this.undoButton.Text = "Undo";
			this.undoButton.UseVisualStyleBackColor = true;
			// 
			// propertiesPanel
			// 
			this.propertiesPanel.AutoSize = true;
			this.propertiesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.propertiesPanel.ColumnCount = 3;
			this.propertiesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.propertiesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 600F));
			this.propertiesPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.propertiesPanel.Controls.Add(this.propertyPanel, 1, 0);
			this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertiesPanel.Location = new System.Drawing.Point(0, 278);
			this.propertiesPanel.Name = "propertiesPanel";
			this.propertiesPanel.RowCount = 1;
			this.propertiesPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.propertiesPanel.Size = new System.Drawing.Size(659, 179);
			this.propertiesPanel.TabIndex = 8;
			// 
			// propertyPanel
			// 
			this.propertyPanel.AutoSize = true;
			this.propertyPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.propertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyPanel.Location = new System.Drawing.Point(32, 3);
			this.propertyPanel.Name = "propertyPanel";
			this.propertyPanel.Size = new System.Drawing.Size(594, 173);
			this.propertyPanel.TabIndex = 0;
			// 
			// topPanel
			// 
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(659, 278);
			this.topPanel.TabIndex = 3;
			// 
			// TestProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(659, 486);
			this.ControlBox = false;
			this.Controls.Add(this.propertiesPanel);
			this.Controls.Add(this.bottomPanel);
			this.Controls.Add(this.topPanel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(675, 525);
			this.Name = "TestProperties";
			this.ShowIcon = false;
			this.Text = "TestProperties";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestProperties_FormClosed);
			this.Load += new System.EventHandler(this.TestProperties_Load);
			this.bottomPanel.ResumeLayout(false);
			this.propertiesPanel.ResumeLayout(false);
			this.propertiesPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel bottomPanel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button undoButton;
		private System.Windows.Forms.Button defaultButton;
		private System.Windows.Forms.TableLayoutPanel propertiesPanel;
		private System.Windows.Forms.Panel propertyPanel;
		private System.Windows.Forms.Panel topPanel;
	}
}