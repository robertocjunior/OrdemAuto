using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class DTOParceiroNegocioResponse
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public bool Ativo { get; set; }
        public int Tipo { get; set; }
    }
}
