using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeyHont
{

    public struct PoliticalParty
    {
        public string PartyName;
        public long NumberOfVotes;
        public Boolean OverThreshold;
        public int NumberOfSeats;
    }

    struct DHontQuotients
    {
        public string PartyName;
        public double Quotient;
    }

    public class LeyHontCalculator
    {
        public List<PoliticalParty> PoliticalParties { get; set; }

        DHontQuotients[] Quotients;
        private List<DHontQuotients> CoeficientesEscano { get; set; }
        

        private int Seats;
        private float Threshold;
        private long Census;
        private long BlankVotes;
        private long InvalidVotes;
        private long ValidVotes;
        private long Votes;
        private long PartiesVotes;
        private int NumParties;
        private int NumPartiesOverThreshold;
        private long ThresholdVotes;
        private Boolean ResultsRegistered;
        private double ParticipationPercentage;
        private double ValidVotesPercentage;

        private long dim_array;

        public LeyHontCalculator(int _Seats, float _Threshold, long _Census, long _BlankVotes, long _InvalidVotes)
        {
            Seats = _Seats;
            Threshold = _Threshold;
            Census = _Census;
            BlankVotes = _BlankVotes;
            InvalidVotes = _InvalidVotes;
            PartiesVotes = 0;
            NumParties = 0;
            NumPartiesOverThreshold = 0;
            ResultsRegistered = false;

            PoliticalParties = new List<PoliticalParty>();
        }

        public String AddPoliticalParty(string Nombre, long Votes)
        {
            PoliticalParty p = new PoliticalParty();

            if (ResultsRegistered == true)
            {
                return "Parties have been registered, no more parties can be added.";
            }

            p.PartyName = Nombre;
            p.NumberOfVotes = Votes;

            PoliticalParties.Add(p);

            return "";
        }

        public void PartiesHaveBeenAdded()
        {
            ResultsRegistered = true;
        }

    // Votes used to get the threshold of votes: parties votes plus blank votes;
        private void AggregatedVotes()
        {
            PartiesVotes = 0;
            foreach (PoliticalParty p in PoliticalParties)
            {
                PartiesVotes += p.NumberOfVotes;
            }
            ValidVotes = PartiesVotes + BlankVotes;
            Votes = ValidVotes + InvalidVotes;
            ThresholdVotes = Convert.ToInt64 ((Threshold * ValidVotes / 100));

            ParticipationPercentage = Math.Round( (100 * (double)Votes / (double)Census), 1, MidpointRounding.AwayFromZero);
        }

        private void PartiesOverThreshold()
        {
            int n = 0;
            PoliticalParty _p;

            for(n=0; n < PoliticalParties.Count; n++)
            {
                _p = PoliticalParties[n];
                if (_p.NumberOfVotes >= ThresholdVotes)
                {
                    _p.OverThreshold = true;
                    PoliticalParties[n] = _p;
                    NumPartiesOverThreshold += 1;
                }
            }

            // Let's not care to much on memory :)
            dim_array = NumPartiesOverThreshold * Seats;

            // Let's not be Hooligans :).
            if (dim_array > 1000000)
            {
                throw new InsufficientMemoryException();
            }
            Quotients = new DHontQuotients[dim_array];
        }

        private void PartiesDHontCoeficients()
        {
            int n;
            int m;
            long l = 0;
            PoliticalParty _p;

            for (n = 0; n < PoliticalParties.Count; n++)
            {
                _p = PoliticalParties[n];
                if (!_p.OverThreshold)
                {
                    continue;
                }

                for (m = 1; m <= Seats; m++)
                {
                    Quotients[l].PartyName = _p.PartyName;
                    Quotients[l].Quotient = Convert.ToDouble((_p.NumberOfVotes / m));
                    l++;
                }

            }

            var CoeficientesOrdenados = Quotients.OrderByDescending(CoeficientesHont => CoeficientesHont.Quotient).ToList();

            //CoeficientesEscano = CoeficientesOrdenados.GetRange(0,Convert.ToInt32(Seats-1)); 
            CoeficientesEscano = CoeficientesOrdenados.GetRange(0, Convert.ToInt32(Seats));
        }

        private void AssignSeats()
        {
            // Not the most efficient way to do it
            int n ;
            PoliticalParty _p;

            for (n = 0; n < PoliticalParties.Count; n++)
            {
                _p = PoliticalParties[n];
                if (!_p.OverThreshold)
                {
                    continue;
                }
                _p.NumberOfSeats = CoeficientesEscano.Where( x => x.PartyName.Equals(_p.PartyName)).Count();

                PoliticalParties[n] = _p;
            }
        }

        public string GetResults()
        {
            if (ResultsRegistered == false)
            {
                return "Please, finish first the enrollment of parties.";
            }

            AggregatedVotes();

            PartiesOverThreshold();

            PartiesDHontCoeficients();

            AssignSeats();

            NumParties = PoliticalParties.Count;            

            if (Census > 0)
            {
                ValidVotesPercentage = ValidVotes / Census;
            }

            return "";
        }

        public string GetGeneralComments()
        {
            string Description;

            /*Description = "Ha habido " + Convert.ToString(Votos) + " votos sobre un censo de " + Convert.ToString(Census) + " personas \n";
            Description = Description + "La participación ha sido del " + Convert.ToString(ParticipationPercentage) + "% \n";
            Description = Description + "El umbral para optar a representación son " + Convert.ToString(ThresholdVotes) + " votos \n";
            Description = Description + Convert.ToString(NumPartiesOverThreshold) + " Partidos han pasado el umbral mínimo de votos";*/

            Description = "There have been " + Convert.ToString(Votes) + " on a census of " + Convert.ToString(Census) + " people \n";
            Description = Description + "Participation has been the " + Convert.ToString(ParticipationPercentage) + "% \n";
            Description = Description + "The threshold to get representants is " + Convert.ToString(ThresholdVotes) + " votes \n";
            Description = Description + Convert.ToString(NumPartiesOverThreshold) + " Parties have passed the threshold of votes";

            return Description; 
        }
    }
}
