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
			this.previewPanel = new System.Windows.Forms.Panel();
			this.propertyPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.bottomPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.undoButton = new System.Windows.Forms.Button();
			this.bottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// previewPanel
			// 
			this.previewPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.previewPanel.Location = new System.Drawing.Point(0, 0);
			this.previewPanel.Name = "previewPanel";
			this.previewPanel.Size = new System.Drawing.Size(778, 278);
			this.previewPanel.TabIndex = 3;
			// 
			// propertyPanel
			// 
			this.propertyPanel.AutoScroll = true;
			this.propertyPanel.AutoSize = true;
			this.propertyPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.propertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.propertyPanel.Location = new System.Drawing.Point(0, 278);
			this.propertyPanel.Name = "propertyPanel";
			this.propertyPanel.Size = new System.Drawing.Size(778, 208);
			this.propertyPanel.TabIndex = 8;
			// 
			// bottomPanel
			// 
			this.bottomPanel.AutoSize = true;
			this.bottomPanel.Controls.Add(this.cancelButton);
			this.bottomPanel.Controls.Add(this.okButton);
			this.bottomPanel.Controls.Add(this.undoButton);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.bottomPanel.Location = new System.Drawing.Point(0, 486);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(778, 29);
			this.bottomPanel.TabIndex = 7;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(700, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(619, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// undoButton
			// 
			this.undoButton.Location = new System.Drawing.Point(538, 3);
			this.undoButton.Name = "undoButton";
			this.undoButton.Size = new System.Drawing.Size(75, 23);
			this.undoButton.TabIndex = 5;
			this.undoButton.Text = "Undo";
			this.undoButton.UseVisualStyleBackColor = true;
			// 
			// TestProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(778, 515);
			this.ControlBox = false;
			this.Controls.Add(this.propertyPanel);
			this.Controls.Add(this.bottomPanel);
			this.Controls.Add(this.previewPanel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(675, 525);
			this.Name = "TestProperties";
			this.ShowIcon = false;
			this.Text = "TestProperties";
			this.bottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel previewPanel;
		private System.Windows.Forms.FlowLayoutPanel propertyPanel;
		private System.Windows.Forms.FlowLayoutPanel bottomPanel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button undoButton;
	}
}