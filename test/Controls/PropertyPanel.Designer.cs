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

namespace test.Controls
{
	partial class PropertyPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.nameLabel = new System.Windows.Forms.Label();
			this.controlPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.nameLabel.Location = new System.Drawing.Point(0, 0);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(150, 25);
			this.nameLabel.TabIndex = 0;
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// controlPanel
			// 
			this.controlPanel.AutoSize = true;
			this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlPanel.Location = new System.Drawing.Point(150, 0);
			this.controlPanel.Name = "controlPanel";
			this.controlPanel.Size = new System.Drawing.Size(450, 25);
			this.controlPanel.TabIndex = 1;
			// 
			// PropertyPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.controlPanel);
			this.Controls.Add(this.nameLabel);
			this.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.Name = "PropertyPanel";
			this.Size = new System.Drawing.Size(600, 25);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.FlowLayoutPanel controlPanel;
	}
}
