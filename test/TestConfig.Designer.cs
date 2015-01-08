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
	partial class TestConfig
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
			this.serviceComboBox = new System.Windows.Forms.ComboBox();
			this.serverComboBox = new System.Windows.Forms.ComboBox();
			this.audioComboBox = new System.Windows.Forms.ComboBox();
			this.videoAdapterComboBox = new System.Windows.Forms.ComboBox();
			this.streamKeyTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(136, 174);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(217, 174);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// serviceComboBox
			// 
			this.serviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.serviceComboBox.FormattingEnabled = true;
			this.serviceComboBox.Location = new System.Drawing.Point(92, 12);
			this.serviceComboBox.Name = "serviceComboBox";
			this.serviceComboBox.Size = new System.Drawing.Size(200, 21);
			this.serviceComboBox.TabIndex = 2;
			// 
			// serverComboBox
			// 
			this.serverComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.serverComboBox.FormattingEnabled = true;
			this.serverComboBox.Location = new System.Drawing.Point(92, 39);
			this.serverComboBox.Name = "serverComboBox";
			this.serverComboBox.Size = new System.Drawing.Size(200, 21);
			this.serverComboBox.TabIndex = 3;
			// 
			// audioComboBox
			// 
			this.audioComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.audioComboBox.FormattingEnabled = true;
			this.audioComboBox.Location = new System.Drawing.Point(92, 146);
			this.audioComboBox.Name = "audioComboBox";
			this.audioComboBox.Size = new System.Drawing.Size(200, 21);
			this.audioComboBox.TabIndex = 4;
			// 
			// videoAdapterComboBox
			// 
			this.videoAdapterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.videoAdapterComboBox.FormattingEnabled = true;
			this.videoAdapterComboBox.Location = new System.Drawing.Point(92, 119);
			this.videoAdapterComboBox.Name = "videoAdapterComboBox";
			this.videoAdapterComboBox.Size = new System.Drawing.Size(200, 21);
			this.videoAdapterComboBox.TabIndex = 5;
			// 
			// streamKeyTextBox
			// 
			this.streamKeyTextBox.Location = new System.Drawing.Point(92, 66);
			this.streamKeyTextBox.Name = "streamKeyTextBox";
			this.streamKeyTextBox.Size = new System.Drawing.Size(200, 20);
			this.streamKeyTextBox.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(40, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Service:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(45, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Server:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(26, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Streamkey:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 122);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "VideoAdapter:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(49, 149);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(37, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Audio:";
			// 
			// TestConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(309, 209);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.streamKeyTextBox);
			this.Controls.Add(this.videoAdapterComboBox);
			this.Controls.Add(this.audioComboBox);
			this.Controls.Add(this.serverComboBox);
			this.Controls.Add(this.serviceComboBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Name = "TestConfig";
			this.Text = "TestConfig";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ComboBox serviceComboBox;
		private System.Windows.Forms.ComboBox serverComboBox;
		private System.Windows.Forms.ComboBox audioComboBox;
		private System.Windows.Forms.ComboBox videoAdapterComboBox;
		private System.Windows.Forms.TextBox streamKeyTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
	}
}