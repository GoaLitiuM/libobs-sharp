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

namespace test
{
    partial class TestForm
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
			this.MainViewPanel = new System.Windows.Forms.Panel();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.AddSourceToSceneButton = new System.Windows.Forms.Button();
			this.DelSourceButton = new System.Windows.Forms.Button();
			this.AddSourceButton = new System.Windows.Forms.Button();
			this.SourceListBox = new System.Windows.Forms.ListBox();
			this.HideItemCheckBox = new System.Windows.Forms.CheckBox();
			this.MuteSourceCheckBox = new System.Windows.Forms.CheckBox();
			this.EnableSourceCheckBox = new System.Windows.Forms.CheckBox();
			this.DelSceneButton = new System.Windows.Forms.Button();
			this.AddSceneButton = new System.Windows.Forms.Button();
			this.DelItemButton = new System.Windows.Forms.Button();
			this.AddItemButton = new System.Windows.Forms.Button();
			this.ItemListBox = new System.Windows.Forms.ListBox();
			this.SceneListBox = new System.Windows.Forms.ListBox();
			this.bottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainViewPanel
			// 
			this.MainViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainViewPanel.Location = new System.Drawing.Point(12, 9);
			this.MainViewPanel.Name = "MainViewPanel";
			this.MainViewPanel.Size = new System.Drawing.Size(694, 331);
			this.MainViewPanel.TabIndex = 0;
			this.MainViewPanel.SizeChanged += new System.EventHandler(this.MainViewPanel_SizeChanged);
			// 
			// bottomPanel
			// 
			this.bottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bottomPanel.Controls.Add(this.AddSourceToSceneButton);
			this.bottomPanel.Controls.Add(this.DelSourceButton);
			this.bottomPanel.Controls.Add(this.AddSourceButton);
			this.bottomPanel.Controls.Add(this.SourceListBox);
			this.bottomPanel.Controls.Add(this.HideItemCheckBox);
			this.bottomPanel.Controls.Add(this.MuteSourceCheckBox);
			this.bottomPanel.Controls.Add(this.EnableSourceCheckBox);
			this.bottomPanel.Controls.Add(this.DelSceneButton);
			this.bottomPanel.Controls.Add(this.AddSceneButton);
			this.bottomPanel.Controls.Add(this.DelItemButton);
			this.bottomPanel.Controls.Add(this.AddItemButton);
			this.bottomPanel.Controls.Add(this.ItemListBox);
			this.bottomPanel.Controls.Add(this.SceneListBox);
			this.bottomPanel.Location = new System.Drawing.Point(12, 346);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(694, 121);
			this.bottomPanel.TabIndex = 1;
			// 
			// AddSourceToSceneButton
			// 
			this.AddSourceToSceneButton.Location = new System.Drawing.Point(327, 39);
			this.AddSourceToSceneButton.Name = "AddSourceToSceneButton";
			this.AddSourceToSceneButton.Size = new System.Drawing.Size(75, 23);
			this.AddSourceToSceneButton.TabIndex = 12;
			this.AddSourceToSceneButton.Text = "< <";
			this.AddSourceToSceneButton.UseVisualStyleBackColor = true;
			this.AddSourceToSceneButton.Click += new System.EventHandler(this.AddSourceToSceneButton_Click);
			// 
			// DelSourceButton
			// 
			this.DelSourceButton.Location = new System.Drawing.Point(489, 98);
			this.DelSourceButton.Name = "DelSourceButton";
			this.DelSourceButton.Size = new System.Drawing.Size(75, 23);
			this.DelSourceButton.TabIndex = 11;
			this.DelSourceButton.Text = "Del Source";
			this.DelSourceButton.UseVisualStyleBackColor = true;
			this.DelSourceButton.Click += new System.EventHandler(this.DelSourceButton_Click);
			// 
			// AddSourceButton
			// 
			this.AddSourceButton.Location = new System.Drawing.Point(408, 98);
			this.AddSourceButton.Name = "AddSourceButton";
			this.AddSourceButton.Size = new System.Drawing.Size(75, 23);
			this.AddSourceButton.TabIndex = 10;
			this.AddSourceButton.Text = "Add Source";
			this.AddSourceButton.UseVisualStyleBackColor = true;
			this.AddSourceButton.Click += new System.EventHandler(this.AddSourceButton_Click);
			// 
			// SourceListBox
			// 
			this.SourceListBox.FormattingEnabled = true;
			this.SourceListBox.Location = new System.Drawing.Point(408, 0);
			this.SourceListBox.Name = "SourceListBox";
			this.SourceListBox.Size = new System.Drawing.Size(156, 95);
			this.SourceListBox.TabIndex = 9;
			this.SourceListBox.SelectedIndexChanged += new System.EventHandler(this.SourceListBox_SelectedIndexChanged);
			this.SourceListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SourceListBox_MouseDown);
			// 
			// HideItemCheckBox
			// 
			this.HideItemCheckBox.AutoCheck = false;
			this.HideItemCheckBox.AutoSize = true;
			this.HideItemCheckBox.Location = new System.Drawing.Point(327, 3);
			this.HideItemCheckBox.Name = "HideItemCheckBox";
			this.HideItemCheckBox.Size = new System.Drawing.Size(56, 17);
			this.HideItemCheckBox.TabIndex = 8;
			this.HideItemCheckBox.Text = "Visible";
			this.HideItemCheckBox.UseVisualStyleBackColor = true;
			this.HideItemCheckBox.Click += new System.EventHandler(this.ItemCheckBox_Click);
			// 
			// MuteSourceCheckBox
			// 
			this.MuteSourceCheckBox.AutoCheck = false;
			this.MuteSourceCheckBox.AutoSize = true;
			this.MuteSourceCheckBox.Location = new System.Drawing.Point(570, 26);
			this.MuteSourceCheckBox.Name = "MuteSourceCheckBox";
			this.MuteSourceCheckBox.Size = new System.Drawing.Size(56, 17);
			this.MuteSourceCheckBox.TabIndex = 7;
			this.MuteSourceCheckBox.Text = "Muted";
			this.MuteSourceCheckBox.UseVisualStyleBackColor = true;
			this.MuteSourceCheckBox.Click += new System.EventHandler(this.MuteSourceCheckBox_Click);
			// 
			// EnableSourceCheckBox
			// 
			this.EnableSourceCheckBox.AutoCheck = false;
			this.EnableSourceCheckBox.AutoSize = true;
			this.EnableSourceCheckBox.Location = new System.Drawing.Point(570, 3);
			this.EnableSourceCheckBox.Name = "EnableSourceCheckBox";
			this.EnableSourceCheckBox.Size = new System.Drawing.Size(65, 17);
			this.EnableSourceCheckBox.TabIndex = 6;
			this.EnableSourceCheckBox.Text = "Enabled";
			this.EnableSourceCheckBox.UseVisualStyleBackColor = true;
			this.EnableSourceCheckBox.Click += new System.EventHandler(this.EnableSourceCheckBox_Click);
			// 
			// DelSceneButton
			// 
			this.DelSceneButton.Location = new System.Drawing.Point(84, 98);
			this.DelSceneButton.Name = "DelSceneButton";
			this.DelSceneButton.Size = new System.Drawing.Size(75, 23);
			this.DelSceneButton.TabIndex = 5;
			this.DelSceneButton.Text = "Del Scene";
			this.DelSceneButton.UseVisualStyleBackColor = true;
			this.DelSceneButton.Click += new System.EventHandler(this.DelSceneButton_Click);
			// 
			// AddSceneButton
			// 
			this.AddSceneButton.Location = new System.Drawing.Point(3, 98);
			this.AddSceneButton.Name = "AddSceneButton";
			this.AddSceneButton.Size = new System.Drawing.Size(75, 23);
			this.AddSceneButton.TabIndex = 4;
			this.AddSceneButton.Text = "Add Scene";
			this.AddSceneButton.UseVisualStyleBackColor = true;
			this.AddSceneButton.Click += new System.EventHandler(this.AddSceneButton_Click);
			// 
			// DelItemButton
			// 
			this.DelItemButton.Location = new System.Drawing.Point(246, 98);
			this.DelItemButton.Name = "DelItemButton";
			this.DelItemButton.Size = new System.Drawing.Size(75, 23);
			this.DelItemButton.TabIndex = 3;
			this.DelItemButton.Text = "Del Item";
			this.DelItemButton.UseVisualStyleBackColor = true;
			this.DelItemButton.Click += new System.EventHandler(this.DelItemButton_Click);
			// 
			// AddItemButton
			// 
			this.AddItemButton.Location = new System.Drawing.Point(165, 98);
			this.AddItemButton.Name = "AddItemButton";
			this.AddItemButton.Size = new System.Drawing.Size(75, 23);
			this.AddItemButton.TabIndex = 2;
			this.AddItemButton.Text = "Add Item";
			this.AddItemButton.UseVisualStyleBackColor = true;
			this.AddItemButton.Click += new System.EventHandler(this.AddItemButton_Click);
			// 
			// ItemListBox
			// 
			this.ItemListBox.FormattingEnabled = true;
			this.ItemListBox.Location = new System.Drawing.Point(165, 0);
			this.ItemListBox.Name = "ItemListBox";
			this.ItemListBox.Size = new System.Drawing.Size(156, 95);
			this.ItemListBox.TabIndex = 1;
			this.ItemListBox.SelectedIndexChanged += new System.EventHandler(this.ItemListBox_SelectedIndexChanged);
			this.ItemListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ItemListBox_MouseUp);
			// 
			// SceneListBox
			// 
			this.SceneListBox.FormattingEnabled = true;
			this.SceneListBox.Location = new System.Drawing.Point(3, 0);
			this.SceneListBox.Name = "SceneListBox";
			this.SceneListBox.Size = new System.Drawing.Size(156, 95);
			this.SceneListBox.TabIndex = 0;
			this.SceneListBox.SelectedIndexChanged += new System.EventHandler(this.SceneListBox_SelectedIndexChanged);
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(718, 479);
			this.Controls.Add(this.bottomPanel);
			this.Controls.Add(this.MainViewPanel);
			this.Name = "TestForm";
			this.Text = "libobs-sharp-test";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
			this.Load += new System.EventHandler(this.TestForm_Load);
			this.bottomPanel.ResumeLayout(false);
			this.bottomPanel.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		public System.Windows.Forms.Panel MainViewPanel;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Button DelSceneButton;
		private System.Windows.Forms.Button AddSceneButton;
		private System.Windows.Forms.Button DelItemButton;
		private System.Windows.Forms.Button AddItemButton;
		private System.Windows.Forms.ListBox ItemListBox;
		private System.Windows.Forms.ListBox SceneListBox;
		private System.Windows.Forms.CheckBox MuteSourceCheckBox;
		private System.Windows.Forms.CheckBox EnableSourceCheckBox;
		private System.Windows.Forms.CheckBox HideItemCheckBox;
		private System.Windows.Forms.Button DelSourceButton;
		private System.Windows.Forms.Button AddSourceButton;
		private System.Windows.Forms.ListBox SourceListBox;
		private System.Windows.Forms.Button AddSourceToSceneButton;
    }
}

