using Domain.Entities.Domain.Enums;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CWOrdemServicoItem
    {
        [Key]
        public int nCdItemOrdemServico { get; set; }

        [ForeignKey(nameof(OrdemServico))]
        public int nCdOrdemServico { get; set; }
        public CWOrdemServico OrdemServico { get; set; }

        [ForeignKey(nameof(Peca))]
        public int nCdPeca { get; set; }
        public CWPecas Peca { get; set; }
        public string sDsReparo { get; set; }
        public double dVlEstimado { get; set; }
        public double dVlReal { get; set; }
        public eStatusItemOrdemServico eStatus { get; set; }
    }

    namespace Domain.Enums
    {
        public enum eStatusItemOrdemServico
        {
            Pendente = 0,
            EmAndamento = 1,
            Concluido = 2, 
            Cancelado = 3
        }
    }

}
