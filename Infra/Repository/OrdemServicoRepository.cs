using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infra.Contexts;
using System.Linq;

namespace Infra.Repository
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly AnalyzerDbContext _context;
        private readonly ICadastroRepository _cadastroRepository;

        public OrdemServicoRepository(AnalyzerDbContext context, ICadastroRepository cadastroRepository)
        {
            _context = context;
            _cadastroRepository = cadastroRepository;   
        }
        public async Task<List<CWOrdemServico>> Pesquisar()
        {
            return await _context.OrdemServico
                .Include(o => o.Prestador)
                .Include(o => o.Seguradora)
                .Include(o => o.Veiculo)
                .Include(o => o.Itens)
                    .ThenInclude(i => i.Peca)
                .ToListAsync();
        }
        public async Task<CWOrdemServico> Consultar(int id)
        {
            return await _context.OrdemServico
                .Include(o => o.Prestador)
                .Include(o => o.Seguradora)
                .Include(o => o.Veiculo)
                .Include(o => o.Itens)
                    .ThenInclude(i => i.Peca)
                .FirstOrDefaultAsync(o => o.nCdOrdemServico == id) ?? new CWOrdemServico();
        }
        public async Task Adicionar(CWOrdemServico cWOrdemServico)
        {

            cWOrdemServico.Prestador = await _context.ParceiroNegocios.FindAsync(cWOrdemServico.nCdPrestador) ?? new();
            cWOrdemServico.Seguradora = await _context.ParceiroNegocios.FindAsync(cWOrdemServico.nCdSeguradora) ?? new();
            cWOrdemServico.Itens = await _cadastroRepository.GarantirPecas(cWOrdemServico.Itens.ToList());

            _context.Entry(cWOrdemServico.Prestador).State = EntityState.Unchanged;
            _context.Entry(cWOrdemServico.Seguradora).State = EntityState.Unchanged;

            await _context.OrdemServico.AddAsync(cWOrdemServico);
            await _context.SaveChangesAsync();
        }
        public async Task Editar(CWOrdemServico cwOrdemServico)
        {
            var ordemExistente = await _context.OrdemServico
                .Include(o => o.Itens)
                .FirstOrDefaultAsync(o => o.nCdOrdemServico == cwOrdemServico.nCdOrdemServico);

            if (ordemExistente == null)
                return;

            ordemExistente.sDsOrdem = cwOrdemServico.sDsOrdem;
            ordemExistente.tDtOrdem = cwOrdemServico.tDtOrdem;
            ordemExistente.tDtRetorno = cwOrdemServico.tDtRetorno;
            ordemExistente.nCdVeiculo = cwOrdemServico.nCdVeiculo;
            ordemExistente.nCdPrestador = cwOrdemServico.nCdPrestador;
            ordemExistente.nCdSeguradora = cwOrdemServico.nCdSeguradora;
            ordemExistente.sDsObservacao = cwOrdemServico.sDsObservacao;
            ordemExistente.dVlTotal = cwOrdemServico.dVlTotal;

            var itensCorrigidos = await _cadastroRepository.GarantirPecas(cwOrdemServico.Itens.ToList());

            _context.OrdemServicoItem.RemoveRange(ordemExistente.Itens);

            foreach (var item in itensCorrigidos)
            {
                item.nCdOrdemServico = ordemExistente.nCdOrdemServico;
                _context.OrdemServicoItem.Add(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AdicionarItem(CWOrdemServicoItem item)
        {
            await _context.OrdemServicoItem.AddAsync(item);
            await _context.SaveChangesAsync();
        }
    }
}