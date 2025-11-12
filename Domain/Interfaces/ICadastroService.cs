using Domain.Entities;
using Domain.ViewModel;

namespace Domain.Interfaces
{
    public interface ICadastroService
    {
        Task<DTOPecasResponse> ConsultarPecas(int id);
        Task<DTOConsultarTodos> ConsultarTodos();
        Task<List<DTOVeiculoResponse>> PesquisarVeiculos();
        Task AdicionarPecas(DTOPecasResponse PesquisarVeiculosdtoParceiro);
        Task EditarPecas(DTOPecasResponse dtoParceiro);
        Task<DTOVeiculoResponse> ConsultarVeiculo(int id);
        Task AdicionarVeiculo(DTOVeiculoResponse dTOVeiculo);
        Task EditarVeiculo(DTOVeiculoResponse dTOVeiculo);
    }
}
