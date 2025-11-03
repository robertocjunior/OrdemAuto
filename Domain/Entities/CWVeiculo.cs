using Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class CWVeiculo
    {
        [Key]
        public int nCdVeiculo {  get; set; }
        public string sNmVeiculo { get; set; }
        public string sCor { get; set; }
        public string sTipo { get; set; }
        public DateTime tDtAno { get; set; }
        public string sPlaca { get; set; }
        public string sMecanico { get; set; }
    }
}
