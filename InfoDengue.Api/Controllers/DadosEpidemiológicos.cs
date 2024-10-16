using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


[ApiController]
[Route("Controller")]
public class DadosEpidemiologicosController : ControllerBase
{
    private readonly DengueApiService _dengueApiService;

    public DadosEpidemiologicosController(DengueApiService dengueApiService)
    {
        _dengueApiService = dengueApiService;
    }

    /// <summary>
    /// Obtém dados epidemiológicos filtrados por município, data de início, data de fim e tipo de arbovirose.
    /// Consulta a API externa de dados epidemiológicos e retorna os resultados com base nos filtros fornecidos.
    /// Retorna um status 200 com os dados se encontrados, ou um status 404 se nenhum dado for encontrado.
    /// </summary>
    /// <param name="municipio">Nome do município para filtrar os dados.</param>
    /// <param name="dataInicio">Data de início da semana epidemiológica para o filtro.</param>
    /// <param name="dataFim">Data de término da semana epidemiológica para o filtro.</param>
    /// <param name="arbovirose">Tipo de arbovirose a ser filtrado (ex.: dengue, zika, chikungunya).</param>
    /// <returns>Retorna um código de status 200 com os dados epidemiológicos filtrados ou um status 404 se nenhum dado for encontrado.</returns>
    [HttpGet("municipios")]
    public async Task<IActionResult> GetDadosEpidemiologicos(
        [FromQuery] string municipio,
        [FromQuery] string dataInicio,
        [FromQuery] string dataFim,
        [FromQuery] string arbovirose)
    {
        var dados = await _dengueApiService.GetDadosEpidemiologicosAsync(municipio, dataInicio, dataFim, arbovirose);

        if (dados == null || dados.Count == 0)
        {
            return NotFound(new { message = "Nenhum dado encontrado para os parâmetros fornecidos." });
        }

        return Ok(dados);  
    }


    /// <summary>
    /// Obtém os dados epidemiológicos para o município identificado pelo código IBGE.
    /// Retorna um status 200 com os dados se encontrados, ou um status 404 se nenhum dado for encontrado.
    /// </summary>
    /// <param name="codigoIbge">Código IBGE do município para o qual os dados epidemiológicos serão consultados.</param>
    /// <returns>Retorna um código de status 200 com os dados epidemiológicos ou um status 404 se nenhum dado for encontrado.</returns>
    [HttpGet("municipios/{codigoIbge}")]
    public async Task<IActionResult> GetDadosPorCodigoIbge(int codigoIbge)
    {
        var dados = await _dengueApiService.GetDadosPorCodigoIbgeAsync(codigoIbge);

        if (dados == null || dados.Count == 0)
        {
            return NotFound(new { message = "Nenhum dado encontrado para o código IBGE fornecido." });
        }

        return Ok(dados);
    }


    /// <summary>
    /// Obtém os dados epidemiológicos com base nos parâmetros fornecidos e retorna os resultados correspondentes.
    /// Retorna um status 200 com os dados se encontrados, ou um status 404 se nenhum dado for encontrado.
    /// </summary>
    /// <param name="codigoIbge">Código IBGE do município para o qual os dados epidemiológicos serão consultados.</param>
    /// <param name="dataInicio">Data de início da semana epidemiológica para o filtro.</param>
    /// <param name="dataFim">Data de término da semana epidemiológica para o filtro.</param>
    /// <param name="arbovirose">Tipo de arbovirose a ser filtrado (ex.: dengue, zika, chikungunya).</param>
    /// <returns>Retorna um código de status 200 com os dados epidemiológicos filtrados ou um status 404 se nenhum dado for encontrado.</returns>
    [HttpGet("municipios")]
    public async Task<IActionResult> GetDadosFiltrados([FromQuery] int codigoIbge, [FromQuery] string dataInicio, [FromQuery] string dataFim, [FromQuery] string arbovirose)
    {
        var dados = await _dengueApiService.GetDadosFiltradosAsync(codigoIbge, dataInicio, dataFim, arbovirose);

        if (dados == null || dados.Count == 0)
        {
            return NotFound(new { message = "Nenhum dado encontrado para os parâmetros fornecidos." });
        }

        return Ok(dados);
    }


    /// <summary>
    /// Obtém a quantidade máxima e mínima de casos epidemiológicos por semana.
    /// Consulta os dados epidemiológicos e retorna a semana com o maior e o menor número de casos confirmados.
    /// Retorna um status 200 com os dados se encontrados, ou um status 404 se nenhum dado for encontrado.
    /// </summary>
    /// <returns>Retorna um código de status 200 com os dados de casos máximos e mínimos por semana ou um status 404 se nenhum dado for encontrado.</returns>
    [HttpGet("municipios/semana/maxmin")]
    public async Task<IActionResult> GetMaxMinCasosPorSemana()
    {
        var dados = await _dengueApiService.GetMaxMinCasosPorSemanaAsync();

        if (dados == null || dados.Count == 0)
        {
            return NotFound(new { message = "Nenhum dado encontrado." });
        }

        return Ok(dados);
    }


    /// <summary>
    /// Obtém o total de casos epidemiológicos agrupados por tipo de arbovirose.
    /// Consulta os dados epidemiológicos e retorna a soma total de casos confirmados para cada tipo de arbovirose.
    /// Retorna um status 200 com os dados se encontrados, ou um status 404 se nenhum dado for encontrado.
    /// </summary>
    /// <returns>Retorna um código de status 200 com o total de casos por tipo de arbovirose ou um status 404 se nenhum dado for encontrado.</returns>
    [HttpGet("municipios/total/arboviroses")]
    public async Task<IActionResult> GetTotalCasosPorArbovirose()
    {
        var dados = await _dengueApiService.GetTotalCasosPorArboviroseAsync();

        if (dados == null || dados.Count == 0)
        {
            return NotFound(new { message = "Nenhum dado encontrado." });
        }

        return Ok(dados);
    }


    /// <summary>
    /// Obtém os logs de acesso dos usuários.
    /// Consulta os registros de acesso dos usuários e retorna uma lista dos logs.
    /// Retorna um status 200 com os dados se encontrados.
    /// </summary>
    /// <returns>Retorna um código de status 200 com os logs de acesso dos usuários.</returns>
    [HttpGet("logs/acesso")]
    public async Task<IActionResult> GetUserAccessLogs()
    {
        var logs = await _dengueApiService.GetUserAccessLogsAsync();
        return Ok(logs);
    }

    /// <summary>
    /// Obtém os logs de inclusão dos dados epidemiológicos.
    /// Consulta os registros de inclusão dos dados epidemiológicos e retorna uma lista dos logs.
    /// Retorna um status 200 com os dados se encontrados.
    /// </summary>
    /// <returns>Retorna um código de status 200 com os logs de inclusão dos dados epidemiológicos.</returns>
    [HttpGet("logs/inclusao")]
    public async Task<IActionResult> GetEpidemiologicalDataLogs()
    {
        var logs = await _dengueApiService.GetEpidemiologicalDataLogsAsync();
        return Ok(logs);
    }
}