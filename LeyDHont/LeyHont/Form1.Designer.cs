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
            this.Partidos = new System.Windows.Forms.Button();
            this.Resultados = new System.Windows.Forms.Button();
            this.Censo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Blancos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Nulos = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Escanos = new System.Windows.Forms.TextBox();
            this.Umbral = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Partidos
            // 
            this.Partidos.Location = new System.Drawing.Point(240, 29);
            this.Partidos.Name = "Partidos";
            this.Partidos.Size = new System.Drawing.Size(129, 33);
            this.Partidos.TabIndex = 0;
            this.Partidos.Text = "Añadir partido";
            this.Partidos.UseVisualStyleBackColor = true;
            this.Partidos.Click += new System.EventHandler(this.AnadePartido_Click);
            // 
            // Resultados
            // 
            this.Resultados.Location = new System.Drawing.Point(443, 29);
            this.Resultados.Name = "Resultados";
            this.Resultados.Size = new System.Drawing.Size(129, 33);
            this.Resultados.TabIndex = 1;
            this.Resultados.Text = "Calcular resultados";
            this.Resultados.UseVisualStyleBackColor = true;
            this.Resultados.Click += new System.EventHandler(this.GetResults_Click);
            // 
            // Censo
            // 
            this.Censo.Location = new System.Drawing.Point(600, 80);
            this.Censo.Name = "Censo";
            this.Censo.Size = new System.Drawing.Size(144, 20);
            this.Censo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(440, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Censo total (opcional)";
            // 
            // Blancos
            // 
            this.Blancos.Location = new System.Drawing.Point(240, 141);
            this.Blancos.Name = "Blancos";
            this.Blancos.Size = new System.Drawing.Size(144, 20);
            this.Blancos.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Votos en blanco (opcional)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Votos nulos (opcional)";
            // 
            // Nulos
            // 
            this.Nulos.Location = new System.Drawing.Point(600, 141);
            this.Nulos.Name = "Nulos";
            this.Nulos.Size = new System.Drawing.Size(144, 20);
            this.Nulos.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Número de escaños";
            // 
            // Escanos
            // 
            this.Escanos.Location = new System.Drawing.Point(240, 77);
            this.Escanos.Name = "Escanos";
            this.Escanos.Size = new System.Drawing.Size(144, 20);
            this.Escanos.TabIndex = 9;
            // 
            // Umbral
            // 
            this.Umbral.Location = new System.Drawing.Point(460, 110);
            this.Umbral.Name = "Umbral";
            this.Umbral.Size = new System.Drawing.Size(37, 20);
            this.Umbral.TabIndex = 11;
            this.Umbral.Text = "3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(273, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Porcentaje minimo de votos para obtener representación";
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
            this.Controls.Add(this.Umbral);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Escanos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Nulos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Blancos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Censo);
            this.Controls.Add(this.Resultados);
            this.Controls.Add(this.Partidos);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Partidos;
        private System.Windows.Forms.Button Resultados;
        private System.Windows.Forms.TextBox Censo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Blancos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Nulos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Escanos;
        private System.Windows.Forms.TextBox Umbral;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

