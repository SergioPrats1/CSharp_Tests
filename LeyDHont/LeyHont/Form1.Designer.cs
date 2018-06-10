namespace LeyHont
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.bAddParties = new System.Windows.Forms.Button();
            this.bResults = new System.Windows.Forms.Button();
            this.tbCensus = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBlanks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbInvalids = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSeats = new System.Windows.Forms.TextBox();
            this.tbThreshold = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bAddParties
            // 
            this.bAddParties.Location = new System.Drawing.Point(240, 29);
            this.bAddParties.Name = "bAddParties";
            this.bAddParties.Size = new System.Drawing.Size(129, 33);
            this.bAddParties.TabIndex = 0;
            this.bAddParties.Text = "Add Politic Party";
            this.bAddParties.UseVisualStyleBackColor = true;
            this.bAddParties.Click += new System.EventHandler(this.AnadePartido_Click);
            // 
            // bResults
            // 
            this.bResults.Location = new System.Drawing.Point(443, 29);
            this.bResults.Name = "bResults";
            this.bResults.Size = new System.Drawing.Size(129, 33);
            this.bResults.TabIndex = 1;
            this.bResults.Text = "Get Results";
            this.bResults.UseVisualStyleBackColor = true;
            this.bResults.Click += new System.EventHandler(this.GetResults_Click);
            // 
            // tbCensus
            // 
            this.tbCensus.Location = new System.Drawing.Point(600, 80);
            this.tbCensus.Name = "tbCensus";
            this.tbCensus.Size = new System.Drawing.Size(144, 20);
            this.tbCensus.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(440, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Census (Optional)";
            // 
            // tbBlanks
            // 
            this.tbBlanks.Location = new System.Drawing.Point(240, 141);
            this.tbBlanks.Name = "tbBlanks";
            this.tbBlanks.Size = new System.Drawing.Size(144, 20);
            this.tbBlanks.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Blank Votes (optional)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Invalid Votes (optional)";
            // 
            // tbInvalids
            // 
            this.tbInvalids.Location = new System.Drawing.Point(600, 141);
            this.tbInvalids.Name = "tbInvalids";
            this.tbInvalids.Size = new System.Drawing.Size(144, 20);
            this.tbInvalids.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Number of seats";
            // 
            // tbSeats
            // 
            this.tbSeats.Location = new System.Drawing.Point(240, 77);
            this.tbSeats.Name = "tbSeats";
            this.tbSeats.Size = new System.Drawing.Size(144, 20);
            this.tbSeats.TabIndex = 9;
            // 
            // tbThreshold
            // 
            this.tbThreshold.Location = new System.Drawing.Point(460, 110);
            this.tbThreshold.Name = "tbThreshold";
            this.tbThreshold.Size = new System.Drawing.Size(37, 20);
            this.tbThreshold.TabIndex = 11;
            this.tbThreshold.Text = "3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(252, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Minimmum percentage needed to get representation";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(503, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "%";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 294);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbThreshold);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbSeats);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbInvalids);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbBlanks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCensus);
            this.Controls.Add(this.bResults);
            this.Controls.Add(this.bAddParties);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bAddParties;
        private System.Windows.Forms.Button bResults;
        private System.Windows.Forms.TextBox tbCensus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbBlanks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbInvalids;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSeats;
        private System.Windows.Forms.TextBox tbThreshold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

