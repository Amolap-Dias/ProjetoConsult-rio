using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultório
{
    class Consulta
    {
        public int idConsulta { get; set; }
        public String Motivo { get; set; }
        public DateTime dt_Consulta { get; set; }
        public String Diagnostico { get; set; }
        public String Receita { get; set; }
        public DateTime dt_Retorno {get;set;}
        public String Motivo_retorno { get; set; }
        public String cmb_Paciente { get; set; }
        public String cmb_Dentista { get; set; }
        public int idPaciente { get; set; }
        public int idDentista { get; set; }

    }
}
