/***************************************************************************
	Copyright (C) 2014 by Ari Vuollet <ari.vuollet@kapsi.fi>
	
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
			this.mainViewPanel = new System.Windows.Forms.Panel();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.delSceneButton = new System.Windows.Forms.Button();
			this.addSceneButton = new System.Windows.Forms.Button();
			this.delSourceButton = new System.Windows.Forms.Button();
			this.addSourceButton = new System.Windows.Forms.Button();
			this.sourceListBox = new System.Windows.Forms.ListBox();
			this.sceneListBox = new System.Windows.Forms.ListBox();
			this.bottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainViewPanel
			// 
			this.mainViewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainViewPanel.Location = new System.Drawing.Point(12, 9);
			this.mainViewPanel.Name = "mainViewPanel";
			this.mainViewPanel.Size = new System.Drawing.Size(694, 331);
			this.mainViewPanel.TabIndex = 0;
			this.mainViewPanel.SizeChanged += new System.EventHandler(this.mainViewPanel_SizeChanged);
			// 
			// bottomPanel
			// 
			this.bottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bottomPanel.Controls.Add(this.delSceneButton);
			this.bottomPanel.Controls.Add(this.addSceneButton);
			this.bottomPanel.Controls.Add(this.delSourceButton);
			this.bottomPanel.Controls.Add(this.addSourceButton);
			this.bottomPanel.Controls.Add(this.sourceListBox);
			this.bottomPanel.Controls.Add(this.sceneListBox);
			this.bottomPanel.Location = new System.Drawing.Point(12, 346);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(694, 121);
			this.bottomPanel.TabIndex = 1;
			// 
			// delSceneButton
			// 
			this.delSceneButton.Location = new System.Drawing.Point(84, 98);
			this.delSceneButton.Name = "delSceneButton";
			this.delSceneButton.Size = new System.Drawing.Size(75, 23);
			this.delSceneButton.TabIndex = 5;
			this.delSceneButton.Text = "DelScene";
			this.delSceneButton.UseVisualStyleBackColor = true;
			this.delSceneButton.Click += new System.EventHandler(this.delSceneButton_Click);
			// 
			// addSceneButton
			// 
			this.addSceneButton.Location = new System.Drawing.Point(3, 98);
			this.addSceneButton.Name = "addSceneButton";
			this.addSceneButton.Size = new System.Drawing.Size(75, 23);
			this.addSceneButton.TabIndex = 4;
			this.addSceneButton.Text = "AddScene";
			this.addSceneButton.UseVisualStyleBackColor = true;
			this.addSceneButton.Click += new System.EventHandler(this.addSceneButton_Click);
			// 
			// delSourceButton
			// 
			this.delSourceButton.Location = new System.Drawing.Point(246, 98);
			this.delSourceButton.Name = "delSourceButton";
			this.delSourceButton.Size = new System.Drawing.Size(75, 23);
			this.delSourceButton.TabIndex = 3;
			this.delSourceButton.Text = "DelSource";
			this.delSourceButton.UseVisualStyleBackColor = true;
			this.delSourceButton.Click += new System.EventHandler(this.delSourceButton_Click);
			// 
			// addSourceButton
			// 
			this.addSourceButton.Location = new System.Drawing.Point(165, 98);
			this.addSourceButton.Name = "addSourceButton";
			this.addSourceButton.Size = new System.Drawing.Size(75, 23);
			this.addSourceButton.TabIndex = 2;
			this.addSourceButton.Text = "AddSource";
			this.addSourceButton.UseVisualStyleBackColor = true;
			this.addSourceButton.Click += new System.EventHandler(this.addSourceButton_Click);
			// 
			// sourceListBox
			// 
			this.sourceListBox.FormattingEnabled = true;
			this.sourceListBox.Location = new System.Drawing.Point(165, 0);
			this.sourceListBox.Name = "sourceListBox";
			this.sourceListBox.Size = new System.Drawing.Size(156, 95);
			this.sourceListBox.TabIndex = 1;
			this.sourceListBox.SelectedIndexChanged += new System.EventHandler(this.sourceListBox_SelectedIndexChanged);
			this.sourceListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sourceListBox_MouseDown);
			// 
			// sceneListBox
			// 
			this.sceneListBox.FormattingEnabled = true;
			this.sceneListBox.Location = new System.Drawing.Point(3, 0);
			this.sceneListBox.Name = "sceneListBox";
			this.sceneListBox.Size = new System.Drawing.Size(156, 95);
			this.sceneListBox.TabIndex = 0;
			this.sceneListBox.SelectedIndexChanged += new System.EventHandler(this.sceneListBox_SelectedIndexChanged);
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(718, 479);
			this.Controls.Add(this.bottomPanel);
			this.Controls.Add(this.mainViewPanel);
			this.Name = "TestForm";
			this.Text = "libobs-sharp-test";
			this.Load += new System.EventHandler(this.TestForm_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
			this.bottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		public System.Windows.Forms.Panel mainViewPanel;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Button delSceneButton;
		private System.Windows.Forms.Button addSceneButton;
		private System.Windows.Forms.Button delSourceButton;
		private System.Windows.Forms.Button addSourceButton;
		private System.Windows.Forms.ListBox sourceListBox;
		private System.Windows.Forms.ListBox sceneListBox;
    }
}

