using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeyHont
{
    public partial class Form1 : Form
    {
        private List<PoliticalPartieVisualComponent> PoliticalParties { get; set; }
        private int pos { get; set; }
        private int line_height { get; set; }

        private int escanos;
        private long censo;
        private long blancos;
        private long nulos;
        private int num_partidos;
        private float umbral;

        private LeyHontCalculator LHC;

        private Form2 form2;

        public Form1()
        {
            var PP = new List<PoliticalPartieVisualComponent>();

            pos = 180;
            line_height = 40;
            PoliticalPartieVisualComponent Partie = new PoliticalPartieVisualComponent(pos);
            PP.Add(Partie);
            PoliticalParties = PP;
            pos = pos + line_height;

            escanos = 0;
            censo = 0;
            nulos = 0;
            blancos = 0;
            num_partidos = 0;

            InitializeComponent();

            SetControls();
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            PoliticalPartieVisualComponent Partie = new PoliticalPartieVisualComponent(pos);
            PoliticalParties.Add(Partie);
            SetControls();
            pos += line_height;
        }*/

        private void AnadePartido_Click(object sender, EventArgs e)
        {
            PoliticalPartieVisualComponent Partie = new PoliticalPartieVisualComponent(pos);
            PoliticalParties.Add(Partie);
            SetControls();
            pos += line_height;
            //pos += line_height;
            //pos += line_height;

            if (Height < pos)
            {
                Height += line_height;
                Show();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetControls()
        {
            string ControlName;

            foreach ( PoliticalPartieVisualComponent p in PoliticalParties )
            {
                ControlName = p.GetPartieBoxName();
                var Cntrl = Controls.Find(ControlName, false);
                if (Cntrl != null)
                {
                    /*Controls.Add(p.PartieName);
                    Controls.Add(p.NumberOfVotes);*/
                    p.RegisterComponents(this);
                }
            }
            Show();
        }

        private string CheckResults()
        {
            string ErrMsg = "";
            bool result;

            if (Escanos.Text == "")
            {
                return "Informa el número de escaños a repartir";
            }
            else
            {
                result = Int32.TryParse(Escanos.Text, out escanos);
                if (result == false || escanos <= 0)
                {
                    return "El número de escaños a repartir no es válido";
                }
            }

            if (Umbral.Text == "")
            {
                return "Informa un umbral de votos para obtener escaños";
            }
            else
            {
                result = float.TryParse(Umbral.Text.ToString(), out umbral);
                if (result == false || umbral <= 0 || umbral >= 100)
                {
                    return "El umbral de votos para obtener escaños no es válido, debe ser un número entre 0 y 100";
                }
            }

            if (Censo.Text != "")
            {
                result = Int64.TryParse(Censo.Text, out censo);
                if (result == false || censo < 0)
                {
                    return "El número de votantes censados no es válido";
                }
            }

            if (Blancos.Text != "")
            {
                result = Int64.TryParse(Blancos.Text, out blancos);
                if (result == false || blancos < 0)
                {
                    return "El número de votos en blanco no es válido";
                }
            }

            if (Nulos.Text != "")
            {
                result = Int64.TryParse(Nulos.Text, out nulos);
                if (result == false || nulos < 0)
                {
                    return "El número de votos nulos no es válido";
                }
            }

            foreach (PoliticalPartieVisualComponent p in PoliticalParties)
            {
                if (p.CheckNumberOfVotes() == false)
                {
                    ErrMsg = "El número de votos \"" + p.NumberOfVotes.Text + "\" del partido " + p.PartieName.Text + " no es válido";
                    break;
                }

                num_partidos += Convert.ToInt32(p.Initialized);
            }

            if (ErrMsg == ""  && num_partidos == 0)
            {
                ErrMsg = "No se ha definido ningún partido";
            }

            return ErrMsg;
        }


        private void GetResults_Click(object sender, EventArgs e)
        {
            string Error;

            Error = CheckResults();

            if ( Error == "" )
            {
                if (LHC is null)
                {
                    LHC = new LeyHontCalculator(escanos, umbral, censo, blancos, nulos );
                    foreach (PoliticalPartieVisualComponent p in PoliticalParties.Where(p=> p.Initialized == true))
                    {
                        LHC.AnadePartido(p._PartiName(), p._NumberOfVotes());
                    }
                    LHC.PartidosAnadidos();
                    LHC.CalcularResultados();

                    form2 = new Form2(LHC);

                }
                // Tras hacer los cálculos mostramos el resultado. 
                if (form2 is null)
                { form2 = new Form2(LHC); }
                
                form2.Show(); 
            }
            else
            {
                MessageBox.Show(Error);
            }

        }

    }
}
