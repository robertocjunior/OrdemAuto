using Domain.Entities;
using Domain.ViewModel;

namespace Domain.Interfaces
{
    public interface IParceiroService
    {
        Task<List<DTOParceiroNegocioResponse>> Pesquisar();
        Task<DTOParceiroNegocioResponse> Consultar(int id);
        Task Adicionar(DTOParceiroNegocioResponse dtoParceiro);
        Task Editar(DTOParceiroNegocioResponse dtoParceiro);

    }
}
