using AutoMapper;
using InfoDengue.Api.Requests;
using InfoDengue.Api.Response;
using InfoDengue.Api.Responses;
using InfoDengue.Infra.Data.Interfaces;
using InfoDengue.Infra.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PerfilController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; // Assumindo que você está usando AutoMapper

    public PerfilController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Cria um novo perfil.
    /// </summary>
    /// <param name="request">Dados do perfil a ser criado.</param>
    /// <returns>Retorna um código de status 201 se o perfil for criado com sucesso.</returns>
    [HttpPost]
    public IActionResult Post(PerfilPostRequest request)
    {
        try
        {
            var perfil = _mapper.Map<Perfil>(request);
            perfil.IdPerfil = Guid.NewGuid();

            _unitOfWork.PerfilRepository.Inserir(perfil);

            var response = _mapper.Map<PerfilResponse>(perfil);
            return CreatedAtAction(nameof(GetById), new { id = perfil.IdPerfil }, response);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Atualiza um perfil existente.
    /// </summary>
    /// <param name="request">Dados do perfil a serem atualizados.</param>
    /// <returns>Retorna um código de status 200 se o perfil for atualizado com sucesso.</returns>
    [HttpPut]
    public IActionResult Put(PerfilPutRequest request)
    {
        try
        {
            var perfil = _unitOfWork.PerfilRepository.ObterPorId(request.IdPerfil);
            if (perfil == null)
                return NotFound(new { message = "Perfil não encontrado." });

            perfil = _mapper.Map<Perfil>(request);
            _unitOfWork.PerfilRepository.Alterar(perfil);

            var response = _mapper.Map<PerfilResponse>(perfil);
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Exclui um perfil pelo ID.
    /// </summary>
    /// <param name="idPerfil">ID do perfil a ser excluído.</param>
    /// <returns>Retorna um código de status 200 se o perfil for excluído com sucesso.</returns>
    [HttpDelete("{idPerfil}")]
    public IActionResult Delete(Guid idPerfil)
    {
        try
        {
            var perfil = _unitOfWork.PerfilRepository.ObterPorId(idPerfil);
            if (perfil == null)
                return NotFound(new { message = "Perfil não encontrado." });

            _unitOfWork.PerfilRepository.Excluir(perfil);

            var response = _mapper.Map<PerfilResponse>(perfil);
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Obtém todos os perfis.
    /// </summary>
    /// <returns>Retorna uma lista de perfis.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {          
            var perfis = _unitOfWork.PerfilRepository.Consultar();
            var lista = _mapper.Map<List<PerfilResponse>>(perfis);

            if (lista.Count > 0)
                return Ok(lista);
            else
                return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Obtém um perfil por ID.
    /// </summary>
    /// <param name="idPerfil">ID do perfil a ser obtido.</param>
    /// <returns>Retorna o perfil correspondente ao ID informado.</returns>
    [HttpGet("{idPerfil}")]
    public IActionResult GetById(Guid idPerfil)
    {
        try
        {
            var perfil = _unitOfWork.PerfilRepository.ObterPorId(idPerfil);
            if (perfil == null)
                return NotFound(new { message = "Perfil não encontrado." });

            var response = _mapper.Map<PerfilResponse>(perfil);
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }         
}
