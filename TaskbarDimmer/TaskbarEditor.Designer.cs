namespace TaskbarDimmer
{
	partial class TaskbarEditor
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
			this.lblLightness = new System.Windows.Forms.Label();
			this.tbLightness = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.cbPosition = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.nudSize = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.tbLightness)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
			this.SuspendLayout();
			// 
			// lblLightness
			// 
			this.lblLightness.AutoSize = true;
			this.lblLightness.Location = new System.Drawing.Point(64, 27);
			this.lblLightness.Name = "lblLightness";
			this.lblLightness.Size = new System.Drawing.Size(13, 13);
			this.lblLightness.TabIndex = 5;
			this.lblLightness.Text = "0";
			// 
			// tbLightness
			// 
			this.tbLightness.Location = new System.Drawing.Point(3, 43);
			this.tbLightness.Maximum = 100;
			this.tbLightness.Minimum = 1;
			this.tbLightness.Name = "tbLightness";
			this.tbLightness.Size = new System.Drawing.Size(445, 45);
			this.tbLightness.TabIndex = 4;
			this.tbLightness.Value = 1;
			this.tbLightness.Scroll += new System.EventHandler(this.tbLightness_Scroll);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Lightness:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Position:";
			// 
			// cbPosition
			// 
			this.cbPosition.FormattingEnabled = true;
			this.cbPosition.Location = new System.Drawing.Point(56, 3);
			this.cbPosition.Name = "cbPosition";
			this.cbPosition.Size = new System.Drawing.Size(121, 21);
			this.cbPosition.TabIndex = 1;
			this.cbPosition.SelectedIndexChanged += new System.EventHandler(this.cbPosition_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(198, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Size:";
			// 
			// nudSize
			// 
			this.nudSize.Location = new System.Drawing.Point(234, 4);
			this.nudSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudSize.Name = "nudSize";
			this.nudSize.Size = new System.Drawing.Size(84, 20);
			this.nudSize.TabIndex = 8;
			this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
			// 
			// TaskbarEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.nudSize);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbPosition);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblLightness);
			this.Controls.Add(this.tbLightness);
			this.Controls.Add(this.label1);
			this.Name = "TaskbarEditor";
			this.Size = new System.Drawing.Size(452, 92);
			((System.ComponentModel.ISupportInitialize)(this.tbLightness)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblLightness;
		private System.Windows.Forms.TrackBar tbLightness;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ComboBox cbPosition;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudSize;
	}
}
