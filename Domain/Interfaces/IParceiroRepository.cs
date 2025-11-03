using Domain.Entities;
using Domain.ViewModel;

namespace Domain.Interfaces
{
    public interface IParceiroRepository
    {
        Task<List<CWParceiroNegocio>> Pesquisar();
        Task<CWParceiroNegocio> Consultar(int id);
        Task Adicionar(CWParceiroNegocio cWParceiro);
        Task Editar(CWParceiroNegocio cWParceiro);
    }
}
