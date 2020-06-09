namespace GiniTwoingConverter
{
	partial class FileSelection
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
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.txtInfo = new System.Windows.Forms.TextBox();
			this.btnTwoing = new System.Windows.Forms.Button();
			this.btnGini = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtPath
			// 
			this.txtPath.AcceptsReturn = true;
			this.txtPath.AllowDrop = true;
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(18, 50);
			this.txtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtPath.Multiline = true;
			this.txtPath.Name = "txtPath";
			this.txtPath.ReadOnly = true;
			this.txtPath.Size = new System.Drawing.Size(494, 90);
			this.txtPath.TabIndex = 0;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(524, 50);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(154, 92);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "BROWSE";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			// 
			// txtInfo
			// 
			this.txtInfo.AcceptsReturn = true;
			this.txtInfo.AcceptsTab = true;
			this.txtInfo.AllowDrop = true;
			this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInfo.Location = new System.Drawing.Point(18, 167);
			this.txtInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtInfo.Multiline = true;
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ReadOnly = true;
			this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtInfo.Size = new System.Drawing.Size(494, 200);
			this.txtInfo.TabIndex = 2;
			// 
			// btnTwoing
			// 
			this.btnTwoing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTwoing.Enabled = false;
			this.btnTwoing.Location = new System.Drawing.Point(524, 167);
			this.btnTwoing.Name = "btnTwoing";
			this.btnTwoing.Size = new System.Drawing.Size(154, 80);
			this.btnTwoing.TabIndex = 3;
			this.btnTwoing.Text = "Open Twoing File";
			this.btnTwoing.UseVisualStyleBackColor = true;
			this.btnTwoing.Click += new System.EventHandler(this.BtnTwoing_Click);
			// 
			// btnGini
			// 
			this.btnGini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGini.Enabled = false;
			this.btnGini.Location = new System.Drawing.Point(524, 287);
			this.btnGini.Name = "btnGini";
			this.btnGini.Size = new System.Drawing.Size(154, 80);
			this.btnGini.TabIndex = 4;
			this.btnGini.Text = "Open Gini File";
			this.btnGini.UseVisualStyleBackColor = true;
			this.btnGini.Click += new System.EventHandler(this.BtnGini_Click);
			// 
			// FileSelection
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(705, 391);
			this.Controls.Add(this.btnGini);
			this.Controls.Add(this.btnTwoing);
			this.Controls.Add(this.txtInfo);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtPath);
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "FileSelection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DEUCENG - ML Classification Tool";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TextBox txtInfo;
		private System.Windows.Forms.Button btnTwoing;
		private System.Windows.Forms.Button btnGini;
	}
}

