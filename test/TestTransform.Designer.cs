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

using test.Controls;

namespace test
{
	partial class TestTransform
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.xNumeric = new System.Windows.Forms.NumericUpDown();
			this.yNumeric = new System.Windows.Forms.NumericUpDown();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.wNumeric = new System.Windows.Forms.NumericUpDown();
			this.hNumeric = new System.Windows.Forms.NumericUpDown();
			this.positionLabel = new System.Windows.Forms.Label();
			this.sizeLabel = new System.Windows.Forms.Label();
			this.rotationLabel = new System.Windows.Forms.Label();
			this.positionalalignmentLabel = new System.Windows.Forms.Label();
			this.Alignment = new test.Controls.AlignmentBox();
			this.Rotation = new test.Controls.RotationBox();
			this.bottomPanel.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.yNumeric)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.wNumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hNumeric)).BeginInit();
			this.SuspendLayout();
			// 
			// bottomPanel
			// 
			this.bottomPanel.AutoSize = true;
			this.bottomPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bottomPanel.Controls.Add(this.cancelButton);
			this.bottomPanel.Controls.Add(this.okButton);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.bottomPanel.Location = new System.Drawing.Point(0, 230);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(364, 29);
			this.bottomPanel.TabIndex = 0;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(286, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(205, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.positionLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.sizeLabel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.rotationLabel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.positionalalignmentLabel, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.Alignment, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.Rotation, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(364, 230);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.Controls.Add(this.xNumeric);
			this.flowLayoutPanel2.Controls.Add(this.yNumeric);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(107, 0);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(257, 26);
			this.flowLayoutPanel2.TabIndex = 7;
			// 
			// xNumeric
			// 
			this.xNumeric.DecimalPlaces = 3;
			this.xNumeric.Location = new System.Drawing.Point(3, 3);
			this.xNumeric.Name = "xNumeric";
			this.xNumeric.Size = new System.Drawing.Size(120, 20);
			this.xNumeric.TabIndex = 0;
			// 
			// yNumeric
			// 
			this.yNumeric.DecimalPlaces = 3;
			this.yNumeric.Location = new System.Drawing.Point(129, 3);
			this.yNumeric.Name = "yNumeric";
			this.yNumeric.Size = new System.Drawing.Size(120, 20);
			this.yNumeric.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.wNumeric);
			this.flowLayoutPanel1.Controls.Add(this.hNumeric);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(107, 26);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(257, 26);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// wNumeric
			// 
			this.wNumeric.DecimalPlaces = 3;
			this.wNumeric.Location = new System.Drawing.Point(3, 3);
			this.wNumeric.Name = "wNumeric";
			this.wNumeric.Size = new System.Drawing.Size(120, 20);
			this.wNumeric.TabIndex = 1;
			// 
			// hNumeric
			// 
			this.hNumeric.DecimalPlaces = 3;
			this.hNumeric.Location = new System.Drawing.Point(129, 3);
			this.hNumeric.Name = "hNumeric";
			this.hNumeric.Size = new System.Drawing.Size(120, 20);
			this.hNumeric.TabIndex = 0;
			// 
			// positionLabel
			// 
			this.positionLabel.AutoSize = true;
			this.positionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.positionLabel.Location = new System.Drawing.Point(3, 0);
			this.positionLabel.Name = "positionLabel";
			this.positionLabel.Size = new System.Drawing.Size(101, 26);
			this.positionLabel.TabIndex = 0;
			this.positionLabel.Text = "Position";
			this.positionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// sizeLabel
			// 
			this.sizeLabel.AutoSize = true;
			this.sizeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sizeLabel.Location = new System.Drawing.Point(3, 26);
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(101, 26);
			this.sizeLabel.TabIndex = 6;
			this.sizeLabel.Text = "Size";
			this.sizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// rotationLabel
			// 
			this.rotationLabel.AutoSize = true;
			this.rotationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rotationLabel.Location = new System.Drawing.Point(3, 52);
			this.rotationLabel.Name = "rotationLabel";
			this.rotationLabel.Size = new System.Drawing.Size(101, 78);
			this.rotationLabel.TabIndex = 5;
			this.rotationLabel.Text = "Rotation";
			this.rotationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// positionalalignmentLabel
			// 
			this.positionalalignmentLabel.AutoSize = true;
			this.positionalalignmentLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.positionalalignmentLabel.Location = new System.Drawing.Point(3, 130);
			this.positionalalignmentLabel.Name = "positionalalignmentLabel";
			this.positionalalignmentLabel.Size = new System.Drawing.Size(101, 78);
			this.positionalalignmentLabel.TabIndex = 4;
			this.positionalalignmentLabel.Text = "Positional Alignment";
			this.positionalalignmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Alignment
			// 
			this.Alignment.Alignment = OBS.ObsAlignment.Center;
			this.Alignment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.Alignment.AutoSize = true;
			this.Alignment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Alignment.Location = new System.Drawing.Point(110, 133);
			this.Alignment.Name = "Alignment";
			this.Alignment.Size = new System.Drawing.Size(72, 72);
			this.Alignment.TabIndex = 2;
			// 
			// Rotation
			// 
			this.Rotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.Rotation.Debug = false;
			this.Rotation.Location = new System.Drawing.Point(110, 55);
			this.Rotation.MaximumSize = new System.Drawing.Size(400, 400);
			this.Rotation.MinimumSize = new System.Drawing.Size(50, 50);
			this.Rotation.Name = "Rotation";
			this.Rotation.Rotation = 0;
			this.Rotation.Size = new System.Drawing.Size(72, 72);
			this.Rotation.SnapAngle = 45;
			this.Rotation.SnapToAngle = true;
			this.Rotation.SnapTolerance = 10;
			this.Rotation.TabIndex = 3;
			// 
			// TestTransform
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(364, 259);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.bottomPanel);
			this.Name = "TestTransform";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Edit Transform";
			this.bottomPanel.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.yNumeric)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.wNumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hNumeric)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel bottomPanel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.NumericUpDown wNumeric;
		private System.Windows.Forms.NumericUpDown hNumeric;
		private System.Windows.Forms.Label positionLabel;
		private System.Windows.Forms.Label sizeLabel;
		private System.Windows.Forms.Label rotationLabel;
		private System.Windows.Forms.Label positionalalignmentLabel;
		private AlignmentBox Alignment;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.NumericUpDown xNumeric;
		private System.Windows.Forms.NumericUpDown yNumeric;
		private RotationBox Rotation;

	}
}