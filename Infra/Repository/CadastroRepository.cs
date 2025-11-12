using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infra.Contexts;
using System;
using Microsoft.VisualBasic;

namespace Infra.Repository
{
    public class CadastroRepository : ICadastroRepository
    {
        private readonly AnalyzerDbContext _context;

        public CadastroRepository(AnalyzerDbContext context)
        {
            _context = context;
        }
        public async Task<CWPecas> ConsultarPecas(int id)
        {
            return await _context.Pecas.FindAsync(id) ?? new();
        } 
        public async Task<List<CWPecas>> PesquisarPecas()
        {
            return await _context.Pecas.ToListAsync();
        }
        public async Task<List<CWVeiculo>> PesquisarVeiculos()
        {
            return await _context.Veiculo.ToListAsync();
        }
        public async Task AdicionarPecas(CWPecas cwPecas)
        {
            await _context.Pecas.AddAsync(cwPecas);
            await _context.SaveChangesAsync();
        }
        public async Task EditarPecas(CWPecas cwPecas)
        {
            CWPecas cwPaceiroCadastrado = await _context.Pecas.FindAsync(cwPecas.nCdPeca) ?? new(); 
            
            if(cwPaceiroCadastrado.nCdPeca > 0)
            {
                cwPaceiroCadastrado.sNmPeca = cwPecas.sNmPeca; 
                cwPaceiroCadastrado.sCor = cwPecas.sCor; 
                cwPaceiroCadastrado.tDtAno = cwPecas.tDtAno; 
                cwPaceiroCadastrado.sModelo = cwPecas.sModelo; 
                cwPaceiroCadastrado.sValor = cwPecas.sValor;

                _context.Pecas.Update(cwPecas);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<CWOrdemServicoItem>> GarantirPecas(List<CWOrdemServicoItem> itens)
        {
            var itensAtualizados = new List<CWOrdemServicoItem>();

            foreach (var item in itens)
            {
                var peca = await ConsultarPecas(item.nCdPeca);

                if (peca.nCdPeca == 0)
                {
                    peca = new CWPecas
                    {
                        sNmPeca = item.Peca.sNmPeca,
                        sModelo = item.Peca.sModelo,
                        sCor = "",
                        tDtAno = null,
                        sValor = item.dVlEstimado
                    };

                    await AdicionarPecas(peca); 
                }
                item.nCdPeca = peca.nCdPeca;

                itensAtualizados.Add(item);
            }

            return itensAtualizados;
        }

        public async Task<CWVeiculo> ConsultarVeiculo(int id)
        {
            return await _context.Veiculo.FindAsync(id) ?? new();
        }

        public async Task AdicionarVeiculo(CWVeiculo cwVeiculo)
        {
            await _context.Veiculo.AddAsync(cwVeiculo);
            await _context.SaveChangesAsync();
        }

        public async Task EditarVeiculo(CWVeiculo cwVeiculo)
        {
            CWVeiculo cwVeiculoCadastrado = await _context.Veiculo.FindAsync(cwVeiculo.nCdVeiculo) ?? new();

            if (cwVeiculoCadastrado.nCdVeiculo > 0)
            {
                cwVeiculoCadastrado.sNmVeiculo = cwVeiculo.sNmVeiculo;
                cwVeiculoCadastrado.sCor = cwVeiculo.sCor;
                cwVeiculoCadastrado.sTipo = cwVeiculo.sTipo;
                cwVeiculoCadastrado.tDtAno = cwVeiculo.tDtAno;
                cwVeiculoCadastrado.sPlaca = cwVeiculo.sPlaca;
                cwVeiculoCadastrado.sMecanico = cwVeiculo.sMecanico;

                _context.Veiculo.Update(cwVeiculoCadastrado);
                await _context.SaveChangesAsync();
            }
        }
    }
}
