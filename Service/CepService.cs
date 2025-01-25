using mail_api.Data;
using mail_api.Domain.DTO;
using mail_api.Domain.Model;
using Newtonsoft.Json;
using mail_api.Domain.@interface;
using Microsoft.Extensions.Logging;

namespace mail_api.Service
{
    public class CepService : ICepService
    {

        private readonly HttpClient _httpClient;
        private readonly ICepRepository _cepRepository;
        private readonly ILogger<CepService> _logger;

        public CepService(HttpClient httpClient, ICepRepository repository)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this._cepRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


       
        private async Task<CepInfo> FetchAddressByCep(cepRequest cepRequest)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cepRequest.Cep}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CepInfo>(json);
                }

                throw new Exception("Address not found for the provided CEP.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpRequestException occurred while fetching address for CEP {cepRequest.Cep}: {ex.Message}");
                throw new ("Error obtaining address by CEP.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error occurred while fetching address for CEP {cepRequest.Cep}: {ex.Message}");

                throw new Exception("Unexpected error processing request.", ex);
            }
        }

        public async Task<CepInfo> GetByCep(string cep)
        {

            CepInfo address = await _cepRepository.GetAdressByCep(cep);
            return address;
        }
        
     
        public async Task<bool> PostAddressByCep(cepRequest cepRequest)
        {
            try
            {
                
                CepInfo addressData = await FetchAddressByCep(cepRequest);

                
                CepInfo existingCep = await _cepRepository.GetAdressByCep(cepRequest.Cep);
                if (existingCep != null)
                {
                   
                    return false;
                }

              
                bool result = await _cepRepository.Create(addressData);

               
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while processing or saving address for CEP {cepRequest.Cep}: {ex.Message}");
                throw new Exception("Error creating or fetching address.", ex);
            }
        }
     
    }
}
