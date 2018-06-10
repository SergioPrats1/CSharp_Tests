using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LeyHont
{
    class PoliticalPartieVisualComponent
    {
        public TextBox PartieName;
        public TextBox NumberOfVotes;
        public Label PartieLabel;
        public Label VotesLabel;

        private int position { get; set; }

        private long NumberOfVotesNum;
        private string PartieNameStr;
        public Boolean Initialized;

        public PoliticalPartieVisualComponent(int pos)
        {
            position = pos;

            PartieLabel = new Label();
            PartieLabel.Location = new Point(80, position);
            PartieLabel.Size = new Size( 40,  25);
            PartieLabel.Text = "Partido";

            PartieName = new TextBox();
            PartieName.Location = new Point(240, position);
            PartieName.Size = new Size(120, 25);
            PartieName.Text = "";
            PartieName.Name = "Name" + position;

            VotesLabel = new Label();
            VotesLabel.Location = new Point(440, position);
            VotesLabel.Size = new Size(40, 25);
            VotesLabel.Text = "Votos";

            NumberOfVotes = new TextBox();
            NumberOfVotes.Location = new Point(600, position);
            NumberOfVotes.Size = new Size(120, 25);
            NumberOfVotes.Text = "";

            // To identify each of the parties (each of the elements in the collection). 
            PartieName.Name = "Name" + position;

        }

        public string GetPartieBoxName()
        {
            return "Name" + position;
        }

        // result = falso implica que la validación ha fallado.
        public bool CheckNumberOfVotes()
        {
            Boolean result = true;
            Initialized = false;

            if (PartieName.Text != "")
            {
                PartieNameStr = PartieName.Text;
                Initialized = true;
                result = Int64.TryParse(NumberOfVotes.Text, out NumberOfVotesNum);
                if (NumberOfVotesNum < 0)
                    { result = false; } 
            }
            //MessageBox.Show(PartieName.Text);
            //MessageBox.Show(string.Format("{0}", NumberOfVotesInt));
            //MessageBox.Show(result.ToString());

            return result;
        }

        public void RegisterComponents(Form f)
        {
            f.Controls.Add(PartieLabel);
            f.Controls.Add(PartieName);
            f.Controls.Add(VotesLabel);
            f.Controls.Add(NumberOfVotes);
        }

        public string _PartiName()
        {
            return PartieNameStr;
        }

        public long _NumberOfVotes()
        {
            return NumberOfVotesNum;
        }

        
    }
}
