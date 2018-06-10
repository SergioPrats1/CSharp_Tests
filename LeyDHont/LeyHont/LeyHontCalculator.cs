using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeyHont
{

    public struct PartidoPolitico
    {
        public string NombrePartido;
        public long NumeroVotos;
        public Boolean PasaUmbral;
        public int NumeroEscanos;
        //public int[] CoeficientesHont;
    }

    struct CoeficientesHont
    {
        public string NombrePartido;
        public double Cociente;
    }

    public class LeyHontCalculator
    {
        public List<PartidoPolitico> PartidosPoliticos { get; set; }

        CoeficientesHont[] Coeficientes;
        private List<CoeficientesHont> CoeficientesEscano { get; set; }
        

        private int Escanos;
        private float Umbral_Porc;
        private long Censo;
        private long VotosBlancos;
        private long VotosNulos;
        private long VotosValidos;
        private long Votos;
        private long VotosPartidos;
        private int NumeroPartidos;
        private int NumeroPartidosPasanUmbral;
        private long VotosUmbral;
        //private Boolean ResultadoCalculado;
        private Boolean ResultadosRegistrados;
        private double Porc_Participacion;
        private double Porc_Votos_Validos;

        private long dim_array;

        public LeyHontCalculator(int _Escanos, float _Umbral_Porc, long _Censo, long _VotosBlancos, long _VotosNulos)
        {
            Escanos = _Escanos;
            Umbral_Porc = _Umbral_Porc;
            Censo = _Censo;
            VotosBlancos = _VotosBlancos;
            VotosNulos = _VotosNulos;
            VotosPartidos = 0;
            NumeroPartidos = 0;
            NumeroPartidosPasanUmbral = 0;
            //ResultadoCalculado = false;
            ResultadosRegistrados = false;

            PartidosPoliticos = new List<PartidoPolitico>();
        }

        public String AnadePartido(string Nombre, long Votos)
        {
            PartidoPolitico p = new PartidoPolitico();

            if (ResultadosRegistrados == true)
            {
                return "Los partidos ya han sido registrados, imposible añadir más partidos.";
            }

            p.NombrePartido = Nombre;
            p.NumeroVotos = Votos;
            //p.CoeficientesHont = new int[Escanos];

            PartidosPoliticos.Add(p);

            return "";
        }

        public void PartidosAnadidos()
        {
            ResultadosRegistrados = true;
        }

    // Votos empleados para calcular el umbral de participación: votos de los partidos más votos blancos;
        private void VotosAgregados()
        {
            VotosPartidos = 0;
            foreach (PartidoPolitico p in PartidosPoliticos)
            {
                VotosPartidos += p.NumeroVotos;
            }
            VotosValidos = VotosPartidos + VotosBlancos;
            Votos = VotosValidos + VotosNulos;
            VotosUmbral = Convert.ToInt64 ((Umbral_Porc * VotosValidos / 100));

            Porc_Participacion = Votos / Censo;
        }

        private void PartidosPasanUmbral()
        {
            int n = 0;
            PartidoPolitico _p;

            for(n=0; n < PartidosPoliticos.Count; n++)
            {
                _p = PartidosPoliticos[n];
                if (_p.NumeroVotos >= VotosUmbral)
                {
                    _p.PasaUmbral = true;
                    PartidosPoliticos[n] = _p;
                    NumeroPartidosPasanUmbral += 1;
                }
            }
            // Calculamos la dimensión del array a saco, memoria no me falta :P.
            dim_array = NumeroPartidosPasanUmbral * Escanos;

            // Memoria no me falta, pero Hooligan dispuestos a petar el programa tampoco :P
            if (dim_array > 1000000)
            {
                throw new InsufficientMemoryException();
            }
            Coeficientes = new CoeficientesHont[dim_array];
        }

        private void CalculaCoeficientesPartidos()
        {
            int n;
            int m;
            long l = 0;
            PartidoPolitico _p;

            for (n = 0; n < PartidosPoliticos.Count; n++)
            {
                _p = PartidosPoliticos[n];
                if (!_p.PasaUmbral)
                {
                    continue;
                }

                for (m = 1; m <= Escanos; m++)
                {
                    Coeficientes[l].NombrePartido = _p.NombrePartido;
                    Coeficientes[l].Cociente = Convert.ToDouble((_p.NumeroVotos / m));
                    l++;
                }

            }

            var CoeficientesOrdenados = Coeficientes.OrderByDescending(CoeficientesHont => CoeficientesHont.Cociente).ToList();

            //CoeficientesEscano = CoeficientesOrdenados.GetRange(0,Convert.ToInt32(Escanos-1)); 
            CoeficientesEscano = CoeficientesOrdenados.GetRange(0, Convert.ToInt32(Escanos));
        }

        private void AsignaEscanos()
        {
            // Se que no es lo más eficiente pero me permitiré ser cafre esta vez:
            int n ;
            PartidoPolitico _p;

            for (n = 0; n < PartidosPoliticos.Count; n++)
            {
                _p = PartidosPoliticos[n];
                if (!_p.PasaUmbral)
                {
                    continue;
                }
                _p.NumeroEscanos = CoeficientesEscano.Where( x => x.NombrePartido.Equals(_p.NombrePartido)).Count();

                PartidosPoliticos[n] = _p;
            }
        }

        public string CalcularResultados()
        {
            if (ResultadosRegistrados == false)
            {
                return "Termine de registrar los resultados de los partidos primero";
            }

            VotosAgregados();

            PartidosPasanUmbral();

            CalculaCoeficientesPartidos();

            AsignaEscanos();

            NumeroPartidos = PartidosPoliticos.Count;            

            if (Censo > 0)
            {
                Porc_Participacion = Votos / Censo;
                Porc_Votos_Validos = VotosValidos / Censo;
            }

            return "";
        }

        public string GetGeneralComments()
        {
            string Description;

            Description = "Ha habido " + Convert.ToString(Votos) + " votos sobre un censo de " + Convert.ToString(Censo) + " personas \n";
            Description = Description + " La participación ha sido del " + Convert.ToString(Porc_Participacion) + "% \n";
            Description = Description + " El umbral para optar a representación son " + Convert.ToString(VotosUmbral) + " votos \n";
            Description = Description + Convert.ToString(NumeroPartidosPasanUmbral) + " Partidos han pasado el umbral mínimo de votos";

            return Description; 
        }
    }
}
