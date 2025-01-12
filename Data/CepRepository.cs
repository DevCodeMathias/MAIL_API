using Dapper;
using Npgsql;
using mail_api.InternalInterface;
using mail_api.Model;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace mail_api.Data
{
    public class CepRepository : ICepRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CepRepository> _logger;

       
        public CepRepository(IConfiguration configuration, ILogger<CepRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

       
        public async Task<CepInfo> GetAdressByCep(string cep)
        {
            const string sql = @"
                SELECT Cep, Logradouro, Complemento, Bairro, Localidade, Uf, Ibge, Gia, Ddd, Siafi
                FROM cepinfo
                WHERE Cep = @Cep";

            try
            {
               
                await using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var result = await connection.QuerySingleOrDefaultAsync<CepInfo>(sql, new { Cep = cep }).ConfigureAwait(false);
                    return result;
                }
            }
            catch (Exception ex)
            {
              
                Console.WriteLine("Error fetching data from the database", ex);
                throw new Exception("Error fetching data from the database", ex);
            }
        }

        public async Task<bool> Create(CepInfo cep)
        {
            const string sql = @"
                INSERT INTO cepinfo (Cep, Logradouro, Complemento, Bairro, Localidade, Uf, Ibge, Gia, Ddd, Siafi) 
                VALUES (@Cep, @Logradouro, @Complemento, @Bairro, @Localidade, @Uf, @Ibge, @Gia, @Ddd, @Siafi)";

            try
            {
            
                await using (var connection = new NpgsqlConnection(_connectionString))
                {
                    var result = await connection.ExecuteAsync(sql, new
                    {
                        cep.Cep,
                        cep.Logradouro,
                        cep.Complemento,
                        cep.Bairro,
                        cep.Localidade,
                        cep.Uf,
                        Ibge = (int)cep.Ibge,
                        cep.Gia,
                        ddd = (int)cep.Ibge,
                        Siafi = (int)cep.Siafi   
                    }).ConfigureAwait(false);

                    return result > 0; 
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error connecting to the database: {ex.Message}");
                throw new Exception("Error inserting data into the database", ex);
            }
        }
    }
}
