namespace TaskbarDimmer
{
	partial class SettingsForm
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
			this.cbStartAutomatically = new System.Windows.Forms.CheckBox();
			this.eventLog1 = new System.Diagnostics.EventLog();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.taskbarEditor6 = new TaskbarDimmer.TaskbarEditor();
			this.taskbarEditor5 = new TaskbarDimmer.TaskbarEditor();
			this.taskbarEditor4 = new TaskbarDimmer.TaskbarEditor();
			this.taskbarEditor3 = new TaskbarDimmer.TaskbarEditor();
			this.taskbarEditor2 = new TaskbarDimmer.TaskbarEditor();
			this.taskbarEditor1 = new TaskbarDimmer.TaskbarEditor();
			this.btnExitProgram = new System.Windows.Forms.Button();
			this.btnOpenDataFolder = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
			this.SuspendLayout();
			// 
			// cbStartAutomatically
			// 
			this.cbStartAutomatically.AutoSize = true;
			this.cbStartAutomatically.Location = new System.Drawing.Point(12, 12);
			this.cbStartAutomatically.Name = "cbStartAutomatically";
			this.cbStartAutomatically.Size = new System.Drawing.Size(155, 17);
			this.cbStartAutomatically.TabIndex = 3;
			this.cbStartAutomatically.Text = "Start Program Automatically";
			this.cbStartAutomatically.UseVisualStyleBackColor = true;
			this.cbStartAutomatically.CheckedChanged += new System.EventHandler(this.cbStartAutomatically_CheckedChanged);
			// 
			// eventLog1
			// 
			this.eventLog1.SynchronizingObject = this;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 20);
			this.label1.TabIndex = 5;
			this.label1.Text = "Taskbar 1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 175);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 20);
			this.label2.TabIndex = 7;
			this.label2.Text = "Taskbar 2";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 293);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 20);
			this.label3.TabIndex = 9;
			this.label3.Text = "Taskbar 3";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(470, 293);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 20);
			this.label4.TabIndex = 15;
			this.label4.Text = "Taskbar 6";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(470, 175);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(79, 20);
			this.label5.TabIndex = 13;
			this.label5.Text = "Taskbar 5";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(470, 57);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 20);
			this.label6.TabIndex = 11;
			this.label6.Text = "Taskbar 4";
			// 
			// taskbarEditor6
			// 
			this.taskbarEditor6.Location = new System.Drawing.Point(470, 316);
			this.taskbarEditor6.Name = "taskbarEditor6";
			this.taskbarEditor6.Size = new System.Drawing.Size(452, 92);
			this.taskbarEditor6.TabIndex = 14;
			// 
			// taskbarEditor5
			// 
			this.taskbarEditor5.Location = new System.Drawing.Point(470, 198);
			this.taskbarEditor5.Name = "taskbarEditor5";
			this.taskbarEditor5.Size = new System.Drawing.Size(452, 92);
			this.taskbarEditor5.TabIndex = 12;
			// 
			// taskbarEditor4
			// 
			this.taskbarEditor4.Location = new System.Drawing.Point(470, 80);
			this.taskbarEditor4.Name = "taskbarEditor4";
			this.taskbarEditor4.Size = new System.Drawing.Size(452, 92);
			this.taskbarEditor4.TabIndex = 10;
			// 
			// taskbarEditor3
			// 
			this.taskbarEditor3.Location = new System.Drawing.Point(12, 316);
			this.taskbarEditor3.Name = "taskbarEditor3";
			this.taskbarEditor3.Size = new System.Drawing.Size(452, 92);
			this.taskbarEditor3.TabIndex = 8;
			// 
			// taskbarEditor2
			// 
			this.taskbarEditor2.Location = new System.Drawing.Point(12, 198);
			this.taskbarEditor2.Name = "taskbarEditor2";
			this.taskbarEditor2.Size = new System.Drawing.Size(452, 92);
			this.taskbarEditor2.TabIndex = 6;
			// 
			// taskbarEditor1
			// 
			this.taskbarEditor1.Location = new System.Drawing.Point(12, 80);
			this.taskbarEditor1.Name = "taskbarEditor1";
			this.taskbarEditor1.Size = new System.Drawing.Size(452, 92);
			this.taskbarEditor1.TabIndex = 4;
			// 
			// btnExitProgram
			// 
			this.btnExitProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExitProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.btnExitProgram.Location = new System.Drawing.Point(629, 410);
			this.btnExitProgram.Name = "btnExitProgram";
			this.btnExitProgram.Size = new System.Drawing.Size(146, 23);
			this.btnExitProgram.TabIndex = 94;
			this.btnExitProgram.Text = "Exit Program";
			this.btnExitProgram.UseVisualStyleBackColor = false;
			this.btnExitProgram.Click += new System.EventHandler(this.btnExitProgram_Click);
			// 
			// btnOpenDataFolder
			// 
			this.btnOpenDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenDataFolder.Location = new System.Drawing.Point(16, 410);
			this.btnOpenDataFolder.Name = "btnOpenDataFolder";
			this.btnOpenDataFolder.Size = new System.Drawing.Size(146, 23);
			this.btnOpenDataFolder.TabIndex = 93;
			this.btnOpenDataFolder.Text = "Open Data Folder";
			this.btnOpenDataFolder.UseVisualStyleBackColor = true;
			this.btnOpenDataFolder.Click += new System.EventHandler(this.btnOpenDataFolder_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(781, 410);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(146, 23);
			this.btnOK.TabIndex = 102;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(939, 445);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnOpenDataFolder);
			this.Controls.Add(this.btnExitProgram);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.taskbarEditor6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.taskbarEditor5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.taskbarEditor4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.taskbarEditor3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.taskbarEditor2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.taskbarEditor1);
			this.Controls.Add(this.cbStartAutomatically);
			this.Name = "SettingsForm";
			this.Text = "TaskbarDimmer Settings";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.CheckBox cbStartAutomatically;
		private System.Diagnostics.EventLog eventLog1;
		private System.Windows.Forms.Label label4;
		private TaskbarEditor taskbarEditor6;
		private System.Windows.Forms.Label label5;
		private TaskbarEditor taskbarEditor5;
		private System.Windows.Forms.Label label6;
		private TaskbarEditor taskbarEditor4;
		private System.Windows.Forms.Label label3;
		private TaskbarEditor taskbarEditor3;
		private System.Windows.Forms.Label label2;
		private TaskbarEditor taskbarEditor2;
		private System.Windows.Forms.Label label1;
		private TaskbarEditor taskbarEditor1;
		private System.Windows.Forms.Button btnOpenDataFolder;
		private System.Windows.Forms.Button btnExitProgram;
		private System.Windows.Forms.Button btnOK;
	}
}