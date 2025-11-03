using Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class CWPecas
    {
        [Key]
        public int nCdPeca {  get; set; }
        public string sNmPeca { get; set; }
        public string sCor { get; set; }
        public string sModelo { get; set; }
        public DateTime tDtAno { get; set; }
        public double sValor { get; set; }
    }
}
