using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class DTOPecasResponse
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public double Valor { get; set; }
        public DateTime Ano { get; set; }
    }
}
