namespace Maxiploit
{
    partial class frmMain
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
            this.btnVSRO = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnVSRO
            // 
            this.btnVSRO.Location = new System.Drawing.Point(106, 102);
            this.btnVSRO.Name = "btnVSRO";
            this.btnVSRO.Size = new System.Drawing.Size(75, 23);
            this.btnVSRO.TabIndex = 0;
            this.btnVSRO.Text = "button1";
            this.btnVSRO.UseVisualStyleBackColor = true;
            this.btnVSRO.Click += new System.EventHandler(this.btnVSRO_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(926, 548);
            this.Controls.Add(this.btnVSRO);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maxiploit";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVSRO;
    }
}

