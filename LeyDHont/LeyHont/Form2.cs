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
    public partial class Form2 : Form
    {

        private List<PoliticalPartieVisualComponentResult> PoliticalParties { get; set; }
        private int pos { get; set; }
        private int line_height { get; set; }

        private LeyHontCalculator LHC;

        public Form2(LeyHontCalculator _LHC)
        {
            string Description;

            PoliticalParties = new List<PoliticalPartieVisualComponentResult>();

            LHC = _LHC;
            InitializeComponent();

            pos = 120;
            line_height = 40;

            foreach (PartidoPolitico p in LHC.PartidosPoliticos)
            {
                PoliticalPartieVisualComponentResult Partie = new PoliticalPartieVisualComponentResult(pos, p.NombrePartido, p.NumeroEscanos);
                PoliticalParties.Add(Partie);
                pos += line_height;

                if (Height < pos)
                {
                    Height += line_height;
                    Show();
                }

            }

            ResumenResultados.Text = LHC.GetGeneralComments();

            SetControls();
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void SetControls()
        {
            string ControlName;

            foreach (PoliticalPartieVisualComponentResult p in PoliticalParties)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
