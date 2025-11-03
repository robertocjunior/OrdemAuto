using Domain.Entities;
using Domain.ViewModel;

namespace Domain.Interfaces
{
    public interface ICadastroService
    {
        Task<DTOPecasResponse> ConsultarPecas(int id);
        Task AdicionarPecas(DTOPecasResponse dtoParceiro);
        Task EditarPecas(DTOPecasResponse dtoParceiro);
        Task<DTOVeiculoResponse> ConsultarVeiculo(int id);
        Task AdicionarVeiculo(DTOVeiculoResponse dTOVeiculo);
        Task EditarVeiculo(DTOVeiculoResponse dTOVeiculo);
    }
}
