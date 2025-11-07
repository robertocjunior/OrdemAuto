using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CWOrdemServico
    {
        [Key]
        public int nCdOrdemServico { get; set; }

        [Required]
        public string sDsOrdem { get; set; }

        public DateTime tDtOrdem { get; set; }
        public DateTime tDtRetorno { get; set; }

        [ForeignKey(nameof(Prestador))]
        public int nCdPrestador { get; set; }
        public CWParceiroNegocio Prestador { get; set; }

        [ForeignKey(nameof(Seguradora))]
        public int nCdSeguradora { get; set; }
        public CWParceiroNegocio Seguradora { get; set; }

        [ForeignKey(nameof(Veiculo))]
        public int? nCdVeiculo { get; set; }
        public CWVeiculo Veiculo { get; set; }
        public string sDsObservacao { get; set; }
        public double dVlTotal { get; set; }
        public ICollection<CWOrdemServicoItem> Itens { get; set; } = new List<CWOrdemServicoItem>();
    }
}
