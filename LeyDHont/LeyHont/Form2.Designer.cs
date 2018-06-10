namespace LeyHont
{
    partial class Form2
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
            this.lResultsSummary = new System.Windows.Forms.Label();
            this.bBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lResultsSummary
            // 
            this.lResultsSummary.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lResultsSummary.Location = new System.Drawing.Point(79, 9);
            this.lResultsSummary.Name = "lResultsSummary";
            this.lResultsSummary.Size = new System.Drawing.Size(714, 93);
            this.lResultsSummary.TabIndex = 0;
            this.lResultsSummary.Text = "Election results summary";
            // 
            // bBack
            // 
            this.bBack.Image = global::LeyHont.Properties.Resources.Flecha_Izda;
            this.bBack.Location = new System.Drawing.Point(2, -2);
            this.bBack.Name = "bBack";
            this.bBack.Size = new System.Drawing.Size(48, 34);
            this.bBack.TabIndex = 1;
            this.bBack.UseVisualStyleBackColor = true;
            this.bBack.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 320);
            this.Controls.Add(this.bBack);
            this.Controls.Add(this.lResultsSummary);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lResultsSummary;
        private System.Windows.Forms.Button bBack;
    }
}