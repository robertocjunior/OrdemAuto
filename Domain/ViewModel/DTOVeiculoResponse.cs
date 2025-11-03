using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class DTOVeiculoResponse
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        public string Mecanico { get; set; }
        public DateTime Ano { get; set; }
    }
}
