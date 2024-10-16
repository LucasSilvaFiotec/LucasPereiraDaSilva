using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dapper;
using InfoDengue.Api.Models.Logs;
using InfoDengue.Api.Models.Relatorios;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;


public class DengueApiService
{
    private readonly HttpClient _client;  
    private readonly string _baseUrl;     
    private readonly string _connectionString;

    // Construtor com injeção de HttpClient e IConfiguration
    public DengueApiService(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _baseUrl = configuration["DengueApi:BaseUrl"];
        _connectionString = configuration.GetConnectionString("ProvaLucas");
    }

    public async Task<string> GetDengueDataAsync()
    {
        try
        {
            string url = "https://info.dengue.mat.br/services/api";
            HttpResponseMessage response = await _client.GetAsync(url); 

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Erro na requisição: {e.Message}");
            return null;
        }
    }

    public async Task<List<DadosEpidemiologicos>> GetDadosEpidemiologicosAsync(string municipio, string dataInicio, string dataFim, string arbovirose)
    {
        try
        {
            string url = $"{_baseUrl}?municipio={municipio}&dataInicio={dataInicio}&dataFim={dataFim}&arbovirose={arbovirose}";

            HttpResponseMessage response = await _client.GetAsync(url); 
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            // Desserializando o JSON para uma lista de objetos
            var dadosEpidemiologicos = JsonConvert.DeserializeObject<List<DadosEpidemiologicos>>(responseBody);

            return dadosEpidemiologicos;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Erro na requisição: {e.Message}");
            return null;
        }
    }

    public async Task<List<DadosEpidemiologicos>> GetDadosPorCodigoIbgeAsync(int codigoIbge)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "SELECT * FROM DadosEpidemiologicos WHERE CodigoIbge = @CodigoIbge";
            var dados = await connection.QueryAsync<DadosEpidemiologicos>(query, new { CodigoIbge = codigoIbge });
            return dados.ToList();
        }
    }

    public async Task<List<DadosEpidemiologicos>> GetDadosFiltradosAsync(int codigoIbge, string dataInicio, string dataFim, string arbovirose)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"SELECT * FROM DadosEpidemiologicos 
                      WHERE CodigoIbge = @CodigoIbge
                      AND DataSemanaInicio >= @DataInicio
                      AND DataSemanaFim <= @DataFim
                      AND Arbovirose = @Arbovirose";
            var dados = await connection.QueryAsync<DadosEpidemiologicos>(query,
                new { CodigoIbge = codigoIbge, DataInicio = dataInicio, DataFim = dataFim, Arbovirose = arbovirose });
            return dados.ToList();
        }
    }

    public async Task<List<CasosMaxMin>> GetMaxMinCasosPorSemanaAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"SELECT DataSemanaInicio, 
                             MAX(CasosConfirmados) AS MaxCasos, 
                             MIN(CasosConfirmados) AS MinCasos 
                      FROM DadosEpidemiologicos
                      GROUP BY DataSemanaInicio";
            var dados = await connection.QueryAsync<CasosMaxMin>(query);
            return dados.ToList();
        }
    }

    public async Task<List<CasosPorArbovirose>> GetTotalCasosPorArboviroseAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = @"SELECT Arbovirose, SUM(CasosConfirmados) AS TotalCasos
                      FROM DadosEpidemiologicos
                      GROUP BY Arbovirose";
            var dados = await connection.QueryAsync<CasosPorArbovirose>(query);
            return dados.ToList();
        }
    }

    public async Task<List<UserAccessLog>> GetUserAccessLogsAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "SELECT * FROM UserAccessLogs";
            var logs = await connection.QueryAsync<UserAccessLog>(query);
            return logs.ToList();
        }
    }

    public async Task<List<EpidemiologicalDataLog>> GetEpidemiologicalDataLogsAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "SELECT * FROM EpidemiologicalDataLogs";
            var logs = await connection.QueryAsync<EpidemiologicalDataLog>(query);
            return logs.ToList();
        }
    }  
    
}