using mail_api.Domain.DTO;
using mail_api.Domain.Model;

namespace mail_api.Domain.@interface
{
    public interface ICepService
    {
        Task<bool> PostAddressByCep(cepRequest cepRequest);
        Task<CepInfo> GetByCep(string cep);
    }
}
