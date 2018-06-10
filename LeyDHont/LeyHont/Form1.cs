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

        private int seats;
        private long census;
        private long blanks;
        private long invalid;
        private int num_parties;
        private float threshold;

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

            seats = 0;
            census = 0;
            invalid = 0;
            blanks = 0;
            num_parties = 0;

            InitializeComponent();

            SetControls();
        }
 

        private void AnadePartido_Click(object sender, EventArgs e)
        {
            PoliticalPartieVisualComponent Partie = new PoliticalPartieVisualComponent(pos);
            PoliticalParties.Add(Partie);
            SetControls();
            pos += line_height;

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

            if (tbSeats.Text == "")
            {
                return "Set the number of seats";
            }
            else
            {
                result = Int32.TryParse(tbSeats.Text, out seats);
                if (result == false || seats <= 0)
                {
                    return "The number of seats is not valid";
                }
            }

            if (tbThreshold.Text == "")
            {
                return "Set a minimmum percentage of votes (0-100)";
            }
            else
            {
                result = float.TryParse(tbThreshold.Text.ToString(), out threshold);
                if (result == false || threshold < 0 || threshold > 100)
                {
                    return "The minimmum percentage number of votes to get representation is not valid, it should be a number between 1 and 100";
                }
            }

            if (tbCensus.Text != "")
            {
                result = Int64.TryParse(tbCensus.Text, out census);
                if (result == false || census < 0)
                {
                    return "The number of votes is not valid";
                }
            }

            if (tbBlanks.Text != "")
            {
                result = Int64.TryParse(tbBlanks.Text, out blanks);
                if (result == false || blanks < 0)
                {
                    return "The number of blank votes is not valid";
                }
            }

            if (tbInvalids.Text != "")
            {
                result = Int64.TryParse(tbInvalids.Text, out invalid);
                if (result == false || invalid < 0)
                {
                    return "The number of invalid votes is not valid";
                }
            }

            foreach (PoliticalPartieVisualComponent p in PoliticalParties)
            {
                if (p.CheckNumberOfVotes() == false)
                {
                    //ErrMsg = "El número de votos \"" + p.NumberOfVotes.Text + "\" del partido " + p.PartieName.Text + " no es válido";
                    ErrMsg = "Party " + p.PartyName.Text + " has an invalid number of votes: \"" + p.NumberOfVotes.Text + "\"";
                    break;
                }

                num_parties += Convert.ToInt32(p.Initialized);
            }

            if (ErrMsg == ""  && num_parties == 0)
            {
                ErrMsg = "There are no parties declared";
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
                    LHC = new LeyHontCalculator(seats, threshold, census, blanks, invalid );
                    foreach (PoliticalPartieVisualComponent p in PoliticalParties.Where(p=> p.Initialized == true))
                    {
                        LHC.AddPoliticalParty(p._PartiName(), p._NumberOfVotes());
                    }
                    LHC.PartiesHaveBeenAdded();
                    LHC.GetResults();

                    form2 = new Form2(LHC);

                }
                // Show the result after having done the calculations. 
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
