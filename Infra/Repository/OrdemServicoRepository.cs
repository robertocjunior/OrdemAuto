using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infra.Contexts;
using System.Linq;

namespace Infra.Repositories
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly AnalyzerDbContext _context;

        public OrdemServicoRepository(AnalyzerDbContext context)
        {
            _context = context;
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

            if (ordemExistente == null || ordemExistente.nCdOrdemServico == 0)
                return;

            ordemExistente.sDsOrdem = cwOrdemServico.sDsOrdem;
            ordemExistente.tDtOrdem = cwOrdemServico.tDtOrdem;
            ordemExistente.tDtRetorno = cwOrdemServico.tDtRetorno;
            ordemExistente.nCdPrestador = cwOrdemServico.nCdPrestador;
            ordemExistente.nCdSeguradora = cwOrdemServico.nCdSeguradora;
            ordemExistente.sDsObservacao = cwOrdemServico.sDsObservacao;
            ordemExistente.dVlTotal = cwOrdemServico.dVlTotal;

            _context.OrdemServicoItem.RemoveRange(ordemExistente.Itens);
            await _context.OrdemServicoItem.AddRangeAsync(cwOrdemServico.Itens);

            _context.OrdemServico.Update(ordemExistente);
            await _context.SaveChangesAsync();
        }
    }
}