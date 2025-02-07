﻿using mail_api.Domain.DTO;
using mail_api.Domain.Model;

namespace mail_api.Domain.Interfaces 
{
    public interface ICepService
    {
        Task<bool> PostAddressByCep(cepRequest cepRequest);
        Task<CepInfo> GetByCep(string cep);
    }
}