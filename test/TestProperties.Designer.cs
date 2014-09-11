/***************************************************************************
	Copyright (C) 2014 by Nick Thijssen <lamah83@gmail.com>
	
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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.propertyPanel = new System.Windows.Forms.TableLayoutPanel();
			this.bottomPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.debugTextBox = new System.Windows.Forms.TextBox();
			this.bottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(645, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(726, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// propertyPanel
			// 
			this.propertyPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.propertyPanel.ColumnCount = 2;
			this.propertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.propertyPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.propertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyPanel.Location = new System.Drawing.Point(0, 0);
			this.propertyPanel.Name = "propertyPanel";
			this.propertyPanel.RowCount = 1;
			this.propertyPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.propertyPanel.Size = new System.Drawing.Size(804, 407);
			this.propertyPanel.TabIndex = 1;
			// 
			// bottomPanel
			// 
			this.bottomPanel.AutoSize = true;
			this.bottomPanel.Controls.Add(this.cancelButton);
			this.bottomPanel.Controls.Add(this.okButton);
			this.bottomPanel.Controls.Add(this.debugTextBox);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.bottomPanel.Location = new System.Drawing.Point(0, 407);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(804, 29);
			this.bottomPanel.TabIndex = 2;
			// 
			// debugTextBox
			// 
			this.debugTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.debugTextBox.Location = new System.Drawing.Point(266, 4);
			this.debugTextBox.Name = "debugTextBox";
			this.debugTextBox.Size = new System.Drawing.Size(373, 20);
			this.debugTextBox.TabIndex = 3;
			// 
			// TestProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(804, 436);
			this.Controls.Add(this.propertyPanel);
			this.Controls.Add(this.bottomPanel);
			this.Name = "TestProperties";
			this.Text = "TestProperties";
			this.Load += new System.EventHandler(this.TestProperties_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestProperties_FormClosed);
			this.bottomPanel.ResumeLayout(false);
			this.bottomPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TableLayoutPanel propertyPanel;
		private System.Windows.Forms.FlowLayoutPanel bottomPanel;
		private System.Windows.Forms.TextBox debugTextBox;
	}
}