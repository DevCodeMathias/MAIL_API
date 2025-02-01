using mail_api.Domain.Model;

namespace mail_api.Domain.Interfaces
{
    public interface ICepRepository
    {
        Task<CepInfo> GetAdressByCep(string cep);
        Task<bool> Create(CepInfo cep);
    }
}
