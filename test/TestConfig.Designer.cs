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
			this.OKButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.DropDownListService = new System.Windows.Forms.ComboBox();
			this.DropDownListServer = new System.Windows.Forms.ComboBox();
			this.DropDownBoxAudio = new System.Windows.Forms.ComboBox();
			this.DrowDownBoxVideoAdapter = new System.Windows.Forms.ComboBox();
			this.TextBoxStreamKey = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(136, 174);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(75, 23);
			this.OKButton.TabIndex = 0;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.Location = new System.Drawing.Point(217, 174);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(75, 23);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// DropDownListService
			// 
			this.DropDownListService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DropDownListService.FormattingEnabled = true;
			this.DropDownListService.Location = new System.Drawing.Point(92, 12);
			this.DropDownListService.Name = "DropDownListService";
			this.DropDownListService.Size = new System.Drawing.Size(200, 21);
			this.DropDownListService.TabIndex = 2;
			// 
			// DropDownListServer
			// 
			this.DropDownListServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DropDownListServer.FormattingEnabled = true;
			this.DropDownListServer.Location = new System.Drawing.Point(92, 39);
			this.DropDownListServer.Name = "DropDownListServer";
			this.DropDownListServer.Size = new System.Drawing.Size(200, 21);
			this.DropDownListServer.TabIndex = 3;
			// 
			// DropDownBoxAudio
			// 
			this.DropDownBoxAudio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DropDownBoxAudio.FormattingEnabled = true;
			this.DropDownBoxAudio.Location = new System.Drawing.Point(92, 146);
			this.DropDownBoxAudio.Name = "DropDownBoxAudio";
			this.DropDownBoxAudio.Size = new System.Drawing.Size(200, 21);
			this.DropDownBoxAudio.TabIndex = 4;
			// 
			// DrowDownBoxVideoAdapter
			// 
			this.DrowDownBoxVideoAdapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DrowDownBoxVideoAdapter.FormattingEnabled = true;
			this.DrowDownBoxVideoAdapter.Location = new System.Drawing.Point(92, 119);
			this.DrowDownBoxVideoAdapter.Name = "DrowDownBoxVideoAdapter";
			this.DrowDownBoxVideoAdapter.Size = new System.Drawing.Size(200, 21);
			this.DrowDownBoxVideoAdapter.TabIndex = 5;
			// 
			// TextBoxStreamKey
			// 
			this.TextBoxStreamKey.Location = new System.Drawing.Point(92, 66);
			this.TextBoxStreamKey.Name = "TextBoxStreamKey";
			this.TextBoxStreamKey.Size = new System.Drawing.Size(200, 20);
			this.TextBoxStreamKey.TabIndex = 6;
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
			this.Controls.Add(this.TextBoxStreamKey);
			this.Controls.Add(this.DrowDownBoxVideoAdapter);
			this.Controls.Add(this.DropDownBoxAudio);
			this.Controls.Add(this.DropDownListServer);
			this.Controls.Add(this.DropDownListService);
			this.Controls.Add(this.CancelButton);
			this.Controls.Add(this.OKButton);
			this.Name = "TestConfig";
			this.Text = "TestConfig";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.ComboBox DropDownListService;
		private System.Windows.Forms.ComboBox DropDownListServer;
		private System.Windows.Forms.ComboBox DropDownBoxAudio;
		private System.Windows.Forms.ComboBox DrowDownBoxVideoAdapter;
		private System.Windows.Forms.TextBox TextBoxStreamKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
	}
}