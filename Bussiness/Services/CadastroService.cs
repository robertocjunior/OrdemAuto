using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModel;
using Domain.Enums;
using StockService.Domain.Utility;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
namespace Bussiness.Services
{
    public class CadastroService : ICadastroService
    {
        private readonly ICadastroRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IParceiroService _parceiroService;
    
        public CadastroService(ICadastroRepository repository, IParceiroService parceiroService)
        {
            _repository = repository;
            _parceiroService = parceiroService; 
        }
        public async Task<DTOPecasResponse> ConsultarPecas(int id) {
            DTOPecasResponse DTOPecasResponse = new DTOPecasResponse();
            DTOPecasResponse = MapearDTO(await _repository.ConsultarPecas(id));
            return DTOPecasResponse;
        }
        public async Task<List<DTOVeiculoResponse>> PesquisarVeiculos()
        {
            return MapearDTOVeiculos(await _repository.PesquisarVeiculos());
        }
        public async Task<DTOConsultarTodos> ConsultarTodos()
        {
            DTOConsultarTodos dto = new DTOConsultarTodos();
            List<DTOParceiroNegocioResponse> dtoParceiro = await _parceiroService.Pesquisar();
            List<DTOPecasResponse> dtoPecas = MapearDTOPecas(await _repository.PesquisarPecas());
            List<DTOVeiculoResponse> dtoVeiculo = MapearDTOVeiculos(await _repository.PesquisarVeiculos());

            dto.pecas = dtoPecas;
            dto.veiculo = dtoVeiculo;
            dto.prestadora = dtoParceiro.Where(x => x.Tipo == (int)enumTipoParceiro.Prestador).ToList();
            dto.seguradora = dtoParceiro.Where(x => x.Tipo == (int)enumTipoParceiro.Seguradora).ToList();
 
            return dto;
        }
        public async Task AdicionarPecas(DTOPecasResponse dtoParceiro)
        {
            CWPecas cwPecas = MapearCW(dtoParceiro);
            await _repository.AdicionarPecas(cwPecas);
        }
        public async Task EditarPecas(DTOPecasResponse dtoParceiro)
        {
            CWPecas cwPecas = MapearCW(dtoParceiro);
            await _repository.EditarPecas(cwPecas);
        }
        public async Task<DTOVeiculoResponse> ConsultarVeiculo(int id)
        {
            DTOVeiculoResponse DTOVeiculoResponse = new DTOVeiculoResponse();
            CWVeiculo cwVeiculo = await _repository.ConsultarVeiculo(id);
            DTOVeiculoResponse = MapearDTO(cwVeiculo);
            return DTOVeiculoResponse;
        }
        public async Task AdicionarVeiculo(DTOVeiculoResponse dtoVeiculo)
        {
            CWVeiculo cwVeiculo = MapearCW(dtoVeiculo);
            await _repository.AdicionarVeiculo(cwVeiculo);
        }
        public async Task EditarVeiculo(DTOVeiculoResponse dtoVeiculo)
        {
            CWVeiculo cwVeiculo = MapearCW(dtoVeiculo);
            await _repository.EditarVeiculo(cwVeiculo);
        }
        private DTOPecasResponse MapearDTO(CWPecas cwPecas)
        {
            return new DTOPecasResponse()
            {
               Codigo = cwPecas.nCdPeca,
               Nome = cwPecas.sNmPeca,
               Cor = cwPecas.sCor,
                Ano = cwPecas?.tDtAno,
            Modelo = cwPecas.sModelo,
               Valor = cwPecas.sValor,
            };
        }
        private CWPecas MapearCW(DTOPecasResponse dto)
        {
            return new CWPecas()
            {
                nCdPeca = dto.Codigo,
                sNmPeca = dto.Nome,
                sCor = dto.Cor,
                tDtAno = dto.Ano,
                sModelo = dto.Modelo,
                sValor = dto.Valor,
            };
        }
        private List<DTOPecasResponse> MapearDTOPecas(List<CWPecas> lstPecas)
        {
            return lstPecas.Select(cwPecas => new DTOPecasResponse
            {
                Codigo = cwPecas.nCdPeca,
                Nome = cwPecas.sNmPeca,
                Cor = cwPecas.sCor,
                Ano = cwPecas.tDtAno,
                Modelo = cwPecas.sModelo,
                Valor = cwPecas.sValor,
            }).ToList();
        }
        private CWVeiculo MapearCW(DTOVeiculoResponse dto)
        {
            return new CWVeiculo()
            {
                nCdVeiculo = dto.Codigo,
                sMecanico = dto.Mecanico,
                sCor = dto.Cor,
                sPlaca = dto.Placa,
                sNmVeiculo = dto.Nome,
                sTipo = dto.Tipo,
                tDtAno = dto.Ano,
            };
        }
        private List<DTOVeiculoResponse> MapearDTOVeiculos(List<CWVeiculo> lstVeiculos)
        {
            return lstVeiculos.Select(cwPecas => new DTOVeiculoResponse
            {
                Codigo = cwPecas.nCdVeiculo,
                Nome = cwPecas.sNmVeiculo,
                Cor = cwPecas.sCor,
                Ano = cwPecas?.tDtAno,
                Tipo = cwPecas.sTipo,
                Placa = cwPecas.sPlaca,
                Mecanico = cwPecas.sMecanico,
            }).ToList();
        }
        private DTOVeiculoResponse MapearDTO(CWVeiculo cwPecas)
        {
            return new DTOVeiculoResponse()
            {
                Codigo = cwPecas.nCdVeiculo,
                Nome = cwPecas.sNmVeiculo,
                Cor = cwPecas.sCor,
                Ano = cwPecas?.tDtAno,
                Tipo = cwPecas.sTipo,
                Placa = cwPecas.sPlaca,
                Mecanico = cwPecas.sMecanico,
            };
        }
    }
}