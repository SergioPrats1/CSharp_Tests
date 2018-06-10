using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LeyHont
{
    class PoliticalPartieVisualComponentResult
    {
        public Label PartieName;
        public Label NumberOfSeats;

        private int position { get; set; }


        public PoliticalPartieVisualComponentResult(int pos, string name, int seats)
        {
            position = pos;

            PartieName = new Label();
            PartieName.Location = new Point(240, position);
            PartieName.Size = new Size(120, 25);
            PartieName.Text = name;
            PartieName.Name = "Name" + position;

            NumberOfSeats = new Label();
            NumberOfSeats.Location = new Point(600, position);
            NumberOfSeats.Size = new Size(120, 25);
            NumberOfSeats.Text = Convert.ToString(seats);

            // To identify each of the parties (each of the elements in the collection). 
            PartieName.Name = "Name" + position;

        }

        public void RegisterComponents(Form f)
        {
            f.Controls.Add(PartieName);
            f.Controls.Add(NumberOfSeats);
        }

        public string GetPartieBoxName()
        {
            return "Name" + position;
        }

    }
}
