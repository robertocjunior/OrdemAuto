using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Domain.Enums;

namespace Bussiness.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _repository;

        public OrdemServicoService(IOrdemServicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DTOOrdemServicoResponse>> Pesquisar()
        {
            var ordens = await _repository.Pesquisar();
            return MapearDTO(ordens);
        }

        public async Task<DTOOrdemServicoResponse> Consultar(int id)
        {
            var ordem = await _repository.Consultar(id);
            return MapearDTO(ordem);
        }

        public async Task Adicionar(DTOOrdemServicoRequest dto)
        {
            var cw = MapearCW(dto);
            await _repository.Adicionar(cw);
        }

        public async Task Editar(DTOOrdemServicoRequest dto)
        {
            var cw = MapearCW(dto);
            await _repository.Editar(cw);
        }
        private CWOrdemServico MapearCW(DTOOrdemServicoRequest dto)
        {
            return new CWOrdemServico
            {
                sDsOrdem = dto.Descricao,
                tDtOrdem = dto.DataOrdem,
                tDtRetorno = dto.DataRetorno,
                nCdPrestador = dto.CodigoPrestador,
                nCdSeguradora = dto.CodigoSeguradora,
                nCdVeiculo = dto.CodigoVeiculo,
                sDsObservacao = dto.Observacao,
                dVlTotal = dto.ValorTotal,
                Itens = dto.Itens?.Select(MapearCWItem).ToList() ?? new List<CWOrdemServicoItem>()
            };
        }
        private CWOrdemServicoItem MapearCWItem(DTOOrdemServicoItemRequest item)
        {
            return new CWOrdemServicoItem
            {
                nCdPeca = item.CodigoPeca,
                sDsReparo = item.DescricaoReparo,
                dVlEstimado = item.ValorEstimado,
                dVlReal = item.ValorReal,
                eStatus = Enum.TryParse(item.Status, out eStatusItemOrdemServico status) ? status : eStatusItemOrdemServico.Pendente
            };
        }
        private DTOOrdemServicoResponse MapearDTO(CWOrdemServico cw)
        {
            if (cw == null) return null;

            return new DTOOrdemServicoResponse
            {
                Codigo = cw.nCdOrdemServico,
                Descricao = cw.sDsOrdem,
                DataOrdem = cw.tDtOrdem,
                DataRetorno = cw.tDtRetorno,
                Prestador = cw.Prestador != null ? new DTOParceiroNegocioResponse
                {
                    Codigo = cw.Prestador.nCdParceiro,
                    Nome = cw.Prestador.sNmParceiro,
                    Telefone = cw.Prestador.sTelefone,
                    Email = cw.Prestador.sEmail,
                    CpfCnpj = cw.Prestador.sCpfCnpj,
                    Ativo = cw.Prestador.bFlAtivo,
                    Tipo = (int)cw.Prestador.eTipo
                } : null,
                Seguradora = cw.Seguradora != null ? new DTOParceiroNegocioResponse
                {
                    Codigo = cw.Seguradora.nCdParceiro,
                    Nome = cw.Seguradora.sNmParceiro,
                    Telefone = cw.Seguradora.sTelefone,
                    Email = cw.Seguradora.sEmail,
                    CpfCnpj = cw.Seguradora.sCpfCnpj,
                    Ativo = cw.Seguradora.bFlAtivo,
                    Tipo = (int)cw.Seguradora.eTipo
                } : null,
                CodigoVeiculo = cw.Veiculo.nCdVeiculo,
                Veiculo = cw.Veiculo != null ? new DTOVeiculoResponse
                {
                    Codigo = cw.Veiculo.nCdVeiculo,
                    Nome = cw.Veiculo.sNmVeiculo,
                    Ano = cw.Veiculo.tDtAno,
                    Cor = cw.Veiculo.sCor,
                    Mecanico = cw.Veiculo.sMecanico,
                    Placa = cw.Veiculo.sPlaca,
                    Tipo = cw.Veiculo.sTipo
                } : null,
                Observacao = cw.sDsObservacao,
                ValorTotal = cw.dVlTotal,
                Itens = cw.Itens?.Select(MapearDTOItem).ToList() ?? new List<DTOOrdemServicoItemResponse>()
            };
        }
        private DTOOrdemServicoItemResponse MapearDTOItem(CWOrdemServicoItem item)
        {
            if (item == null) return null;

            return new DTOOrdemServicoItemResponse
            {
                Codigo = item.nCdItemOrdemServico,
                CodigoPeca = item.nCdPeca,
                Peca = item.Peca != null ? new DTOPecasResponse
                {
                    Codigo = item.Peca.nCdPeca,
                    Modelo = item.Peca.sModelo,
                    Valor = item.Peca.sValor
                } : null,
                DescricaoReparo = item.sDsReparo,
                ValorEstimado = item.dVlEstimado,
                ValorReal = item.dVlReal,
                Status = item.eStatus.ToString()
            };
        }
        private List<DTOOrdemServicoResponse> MapearDTO(List<CWOrdemServico> lst)
        {
            return lst?.Select(MapearDTO).ToList() ?? new List<DTOOrdemServicoResponse>();
        }
    }
}
