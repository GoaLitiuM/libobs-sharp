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
	partial class AlignmentBox
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
			this.panel = new System.Windows.Forms.FlowLayoutPanel();
			this.topleft = new System.Windows.Forms.RadioButton();
			this.topcenter = new System.Windows.Forms.RadioButton();
			this.topright = new System.Windows.Forms.RadioButton();
			this.centerleft = new System.Windows.Forms.RadioButton();
			this.center = new System.Windows.Forms.RadioButton();
			this.centerright = new System.Windows.Forms.RadioButton();
			this.bottomleft = new System.Windows.Forms.RadioButton();
			this.bottomcenter = new System.Windows.Forms.RadioButton();
			this.bottomright = new System.Windows.Forms.RadioButton();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.AutoSize = true;
			this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel.Controls.Add(this.topleft);
			this.panel.Controls.Add(this.topcenter);
			this.panel.Controls.Add(this.topright);
			this.panel.Controls.Add(this.centerleft);
			this.panel.Controls.Add(this.center);
			this.panel.Controls.Add(this.centerright);
			this.panel.Controls.Add(this.bottomleft);
			this.panel.Controls.Add(this.bottomcenter);
			this.panel.Controls.Add(this.bottomright);
			this.panel.Location = new System.Drawing.Point(3, 3);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(66, 66);
			this.panel.TabIndex = 2;
			// 
			// topleft
			// 
			this.topleft.Appearance = System.Windows.Forms.Appearance.Button;
			this.topleft.Location = new System.Drawing.Point(1, 1);
			this.topleft.Margin = new System.Windows.Forms.Padding(1);
			this.topleft.Name = "topleft";
			this.topleft.Size = new System.Drawing.Size(15, 15);
			this.topleft.TabIndex = 0;
			this.topleft.TabStop = true;
			this.topleft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.topleft.UseVisualStyleBackColor = true;
			// 
			// topcenter
			// 
			this.topcenter.Appearance = System.Windows.Forms.Appearance.Button;
			this.topcenter.Location = new System.Drawing.Point(18, 1);
			this.topcenter.Margin = new System.Windows.Forms.Padding(1);
			this.topcenter.Name = "topcenter";
			this.topcenter.Size = new System.Drawing.Size(30, 15);
			this.topcenter.TabIndex = 1;
			this.topcenter.TabStop = true;
			this.topcenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.topcenter.UseVisualStyleBackColor = true;
			// 
			// topright
			// 
			this.topright.Appearance = System.Windows.Forms.Appearance.Button;
			this.panel.SetFlowBreak(this.topright, true);
			this.topright.Location = new System.Drawing.Point(50, 1);
			this.topright.Margin = new System.Windows.Forms.Padding(1);
			this.topright.Name = "topright";
			this.topright.Size = new System.Drawing.Size(15, 15);
			this.topright.TabIndex = 2;
			this.topright.TabStop = true;
			this.topright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.topright.UseVisualStyleBackColor = true;
			// 
			// centerleft
			// 
			this.centerleft.Appearance = System.Windows.Forms.Appearance.Button;
			this.centerleft.Location = new System.Drawing.Point(1, 18);
			this.centerleft.Margin = new System.Windows.Forms.Padding(1);
			this.centerleft.Name = "centerleft";
			this.centerleft.Size = new System.Drawing.Size(15, 30);
			this.centerleft.TabIndex = 3;
			this.centerleft.TabStop = true;
			this.centerleft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.centerleft.UseVisualStyleBackColor = true;
			// 
			// center
			// 
			this.center.Appearance = System.Windows.Forms.Appearance.Button;
			this.center.Location = new System.Drawing.Point(18, 18);
			this.center.Margin = new System.Windows.Forms.Padding(1);
			this.center.Name = "center";
			this.center.Size = new System.Drawing.Size(30, 30);
			this.center.TabIndex = 4;
			this.center.TabStop = true;
			this.center.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.center.UseVisualStyleBackColor = true;
			// 
			// centerright
			// 
			this.centerright.Appearance = System.Windows.Forms.Appearance.Button;
			this.panel.SetFlowBreak(this.centerright, true);
			this.centerright.Location = new System.Drawing.Point(50, 18);
			this.centerright.Margin = new System.Windows.Forms.Padding(1);
			this.centerright.Name = "centerright";
			this.centerright.Size = new System.Drawing.Size(15, 30);
			this.centerright.TabIndex = 5;
			this.centerright.TabStop = true;
			this.centerright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.centerright.UseVisualStyleBackColor = true;
			// 
			// bottomleft
			// 
			this.bottomleft.Appearance = System.Windows.Forms.Appearance.Button;
			this.bottomleft.Location = new System.Drawing.Point(1, 50);
			this.bottomleft.Margin = new System.Windows.Forms.Padding(1);
			this.bottomleft.Name = "bottomleft";
			this.bottomleft.Size = new System.Drawing.Size(15, 15);
			this.bottomleft.TabIndex = 6;
			this.bottomleft.TabStop = true;
			this.bottomleft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.bottomleft.UseVisualStyleBackColor = true;
			// 
			// bottomcenter
			// 
			this.bottomcenter.Appearance = System.Windows.Forms.Appearance.Button;
			this.bottomcenter.Location = new System.Drawing.Point(18, 50);
			this.bottomcenter.Margin = new System.Windows.Forms.Padding(1);
			this.bottomcenter.Name = "bottomcenter";
			this.bottomcenter.Size = new System.Drawing.Size(30, 15);
			this.bottomcenter.TabIndex = 7;
			this.bottomcenter.TabStop = true;
			this.bottomcenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.bottomcenter.UseVisualStyleBackColor = true;
			// 
			// bottomright
			// 
			this.bottomright.Appearance = System.Windows.Forms.Appearance.Button;
			this.bottomright.Location = new System.Drawing.Point(50, 50);
			this.bottomright.Margin = new System.Windows.Forms.Padding(1);
			this.bottomright.Name = "bottomright";
			this.bottomright.Size = new System.Drawing.Size(15, 15);
			this.bottomright.TabIndex = 8;
			this.bottomright.TabStop = true;
			this.bottomright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.bottomright.UseVisualStyleBackColor = true;
			// 
			// AlignBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.panel);
			this.Name = "AlignBox";
			this.Size = new System.Drawing.Size(72, 72);
			this.panel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel panel;
		private System.Windows.Forms.RadioButton topleft;
		private System.Windows.Forms.RadioButton topcenter;
		private System.Windows.Forms.RadioButton topright;
		private System.Windows.Forms.RadioButton centerleft;
		private System.Windows.Forms.RadioButton center;
		private System.Windows.Forms.RadioButton centerright;
		private System.Windows.Forms.RadioButton bottomleft;
		private System.Windows.Forms.RadioButton bottomcenter;
		private System.Windows.Forms.RadioButton bottomright;

	}
}
