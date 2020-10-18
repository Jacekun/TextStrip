namespace TextStrip
{
    partial class frmSimple
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
            this.txtOriginal = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOriginal
            // 
            this.txtOriginal.BackColor = System.Drawing.Color.Gray;
            this.txtOriginal.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtOriginal.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOriginal.ForeColor = System.Drawing.Color.Black;
            this.txtOriginal.Location = new System.Drawing.Point(0, 0);
            this.txtOriginal.Multiline = true;
            this.txtOriginal.Name = "txtOriginal";
            this.txtOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOriginal.Size = new System.Drawing.Size(512, 518);
            this.txtOriginal.TabIndex = 0;
            this.txtOriginal.Text = "Original Text in Here";
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.DimGray;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtOutput.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(657, 0);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(537, 518);
            this.txtOutput.TabIndex = 1;
            this.txtOutput.Text = "Output";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.Font = new System.Drawing.Font("Calibri", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(518, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 81);
            this.button1.TabIndex = 2;
            this.button1.Text = "=>";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1194, 518);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtOriginal);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOriginal;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button button1;
    }
}

