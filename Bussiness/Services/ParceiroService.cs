using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using Domain.Enums;
using StockService.Domain.Utility;
using System.Net.Http;
using System.Net.Http.Json;
namespace Bussiness.Services
{
    public class ParceiroService : IParceiroService
    {
        private readonly IParceiroRepository _repository;
    
        public ParceiroService(IParceiroRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<DTOParceiroNegocioResponse>> Pesquisar()
        {
            List<DTOParceiroNegocioResponse> dTOParceiroNegocioResponse = new List<DTOParceiroNegocioResponse>();
            dTOParceiroNegocioResponse = MapearDTO(await _repository.Pesquisar());
            return dTOParceiroNegocioResponse;
        }
        public async Task<DTOParceiroNegocioResponse> Consultar(int id) {
            DTOParceiroNegocioResponse dTOParceiroNegocioResponse = new DTOParceiroNegocioResponse();
            dTOParceiroNegocioResponse = MapearDTO(await _repository.Consultar(id));
            return dTOParceiroNegocioResponse;
        }
        public async Task Editar(DTOParceiroNegocioResponse dtoParceiro)
        {
            CWParceiroNegocio cwPecas = MapearCW(dtoParceiro);
            await _repository.Editar(cwPecas);
        }
        public async Task Adicionar(DTOParceiroNegocioResponse dtoParceiro)
        {
            CWParceiroNegocio cwParceiro = MapearCW(dtoParceiro);
            await _repository.Adicionar(cwParceiro);
        }
        private DTOParceiroNegocioResponse MapearDTO(CWParceiroNegocio cwParceiro)
        {
            return new DTOParceiroNegocioResponse()
            {
               Codigo = cwParceiro.nCdParceiro,
               Nome = cwParceiro.sNmParceiro,
               Telefone = cwParceiro.sTelefone,
               Email = cwParceiro.sEmail,
               CpfCnpj = cwParceiro.sCpfCnpj,
               Ativo = cwParceiro.bFlAtivo,
               Tipo = (int)cwParceiro.eTipo               
            };
        }
        private CWParceiroNegocio MapearCW(DTOParceiroNegocioResponse dto)
        {
            return new CWParceiroNegocio()
            {
                nCdParceiro = dto.Codigo,
                sNmParceiro = dto.Nome,
                sTelefone = dto.Telefone,
                sEmail = dto.Email,
                sCpfCnpj = dto.CpfCnpj,
                bFlAtivo = dto.Ativo,
                eTipo = (enumTipoParceiro)dto.Tipo
            };
        }
        private List<DTOParceiroNegocioResponse> MapearDTO(List<CWParceiroNegocio> lstParceiro)
        {
            return lstParceiro.Select(cwParceiro => new DTOParceiroNegocioResponse
            {
                Codigo = cwParceiro.nCdParceiro,
                Nome = cwParceiro.sNmParceiro,
                Telefone = cwParceiro.sTelefone,
                Email = cwParceiro.sEmail,
                CpfCnpj = cwParceiro.sCpfCnpj,
                Ativo = cwParceiro.bFlAtivo,
                Tipo = (int)cwParceiro.eTipo
            }).ToList();
        }

    }
}