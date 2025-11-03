using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infra.Contexts;
using System;
using Microsoft.VisualBasic;

namespace Infra.Repositories
{
    public class ParceiroRepository : IParceiroRepository
    {
        private readonly AnalyzerDbContext _context;

        public ParceiroRepository(AnalyzerDbContext context)
        {
            _context = context;
        }
        public async Task<List<CWParceiroNegocio>> Pesquisar()
        {
            return await _context.ParceiroNegocios.ToListAsync();
        }
        public async Task<CWParceiroNegocio> Consultar(int id)
        {
            return await _context.ParceiroNegocios.FindAsync(id) ?? new CWParceiroNegocio();
        }
        public async Task Adicionar(CWParceiroNegocio cWParceiro)
        {
            await _context.ParceiroNegocios.AddAsync(cWParceiro);
            await _context.SaveChangesAsync();
        }
        public async Task Editar(CWParceiroNegocio cwParceiro)
        {
            CWParceiroNegocio cwPaceiroCadastrado = await _context.ParceiroNegocios.FindAsync(cwParceiro.nCdParceiro) ?? new(); 
            
            if(cwPaceiroCadastrado.nCdParceiro > 0)
            {
                cwPaceiroCadastrado.sNmParceiro = cwParceiro.sNmParceiro; 
                cwPaceiroCadastrado.sTelefone = cwParceiro.sTelefone; 
                cwPaceiroCadastrado.sEmail = cwParceiro.sEmail; 
                cwPaceiroCadastrado.sCpfCnpj = cwParceiro.sCpfCnpj; 
                cwPaceiroCadastrado.eTipo = cwParceiro.eTipo;

                _context.ParceiroNegocios.Update(cwParceiro);
                await _context.SaveChangesAsync();
            }
        }
    }
}
