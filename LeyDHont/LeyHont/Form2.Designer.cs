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
            this.ResumenResultados = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ResumenResultados
            // 
            this.ResumenResultados.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ResumenResultados.Location = new System.Drawing.Point(79, 9);
            this.ResumenResultados.Name = "ResumenResultados";
            this.ResumenResultados.Size = new System.Drawing.Size(714, 93);
            this.ResumenResultados.TabIndex = 0;
            this.ResumenResultados.Text = "Resumen resultados electorales.";
            // 
            // button1
            // 
            this.button1.Image = global::LeyHont.Properties.Resources.Flecha_Izda;
            this.button1.Location = new System.Drawing.Point(2, -2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 34);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 320);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ResumenResultados);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ResumenResultados;
        private System.Windows.Forms.Button button1;
    }
}