using Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class CWParceiroNegocio
    {
        [Key]
        public int nCdParceiro {  get; set; }
        public string sNmParceiro { get; set; }
        public enumTipoParceiro eTipo {get;set;}
        public string sTelefone { get; set; }
        public string sEmail { get; set; }
        public string sCpfCnpj { get; set; }
        public bool bFlAtivo { get; set; }
    }
}
