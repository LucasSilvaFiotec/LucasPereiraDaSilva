using InfoDengue.Infra.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InfoDengue.Api.Request;
using InfoDengue.Infra.Repositories;
using InfoDengue.Api.Response;
using InfoDengue.Infra.Entities;

namespace ApiEmpresas.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Cria um novo registro de funcionário.
        /// Verifica se o CPF ou matrícula já estão cadastrados no sistema.
        /// Caso estejam, retorna um erro com status 422.
        /// Caso contrário, insere um novo funcionário e retorna os dados do funcionário criado com status 201.
        /// </summary>
        /// <param name="request">Objeto contendo as informações do funcionário a ser cadastrado.</param>
        /// <returns>Retorna um código de status 201 em caso de sucesso, ou um código de erro em caso de falha.</returns>
        [HttpPost]
        public IActionResult Post(FuncionarioPostRequest request)
        {
            try
            {
                if (_unitOfWork.FuncionarioRepository.ObterPorCpf(request.Cpf) != null)
                    return StatusCode(422, new { message = "O CPF informado já está cadastrado." });

                if (_unitOfWork.FuncionarioRepository.ObterPorMatricula(request.Matricula) != null)
                    return StatusCode(422, new { message = "A Matrícula informada já está cadastrado." });               

                var funcionario = _mapper.Map<Funcionario>(request);
                funcionario.IdFuncionario = Guid.NewGuid();

                _unitOfWork.FuncionarioRepository.Inserir(funcionario);

                var response = _mapper.Map<FuncionarioResponse>(funcionario);                
                return StatusCode(201, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Atualiza os dados de um funcionário existente.
        /// Verifica se o funcionário existe pelo ID informado.
        /// Verifica se o CPF ou matrícula já estão cadastrados para outro funcionário.
        /// Caso as validações sejam atendidas, atualiza os dados do funcionário e retorna o funcionário atualizado com status 200.
        /// </summary>
        /// <param name="request">Objeto contendo as novas informações do funcionário a ser atualizado.</param>
        /// <returns>Retorna um código de status 200 em caso de sucesso, ou um código de erro em caso de falha.</returns>
        [HttpPut]
        public IActionResult Put(FuncionarioPutRequest request)
        {
            try
            {
                var funcionario = _unitOfWork.FuncionarioRepository.ObterPorId(request.IdFuncionario);

                if (funcionario == null)
                    return StatusCode(422, new { message = "Funcionário não encontrado, verifique o ID informado." });

                var registroCpf = _unitOfWork.FuncionarioRepository.ObterPorCpf(request.Cpf);
                if (registroCpf != null && registroCpf.IdFuncionario != funcionario.IdFuncionario)
                    return StatusCode(422, new { message = "O CPF informado já está cadastrado para outro funcionário." });

                var registroMatricula = _unitOfWork.FuncionarioRepository.ObterPorMatricula(request.Matricula);
                if (registroMatricula != null && registroMatricula.IdFuncionario != funcionario.IdFuncionario)
                    return StatusCode(422, new { message = "A Matrícula informada já está cadastrada para outro funcionário." });                

                funcionario = _mapper.Map<Funcionario>(request);
                _unitOfWork.FuncionarioRepository.Alterar(funcionario);

                var response = _mapper.Map<FuncionarioResponse>(funcionario);

                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Exclui um funcionário existente pelo ID informado.
        /// Verifica se o funcionário existe no sistema.
        /// Caso o funcionário seja encontrado, o exclui e retorna os dados do funcionário excluído com status 200.
        /// Caso contrário, retorna um erro com status 422.
        /// </summary>
        /// <param name="idFuncionario">ID do funcionário a ser excluído.</param>
        /// <returns>Retorna um código de status 200 em caso de sucesso, ou um código de erro em caso de falha.</returns>
        [HttpDelete("{idFuncionario}")]
        public IActionResult Delete(Guid idFuncionario)
        {
            try
            {
                var funcionario = _unitOfWork.FuncionarioRepository.ObterPorId(idFuncionario);

                if (funcionario == null)
                    return StatusCode(422, new { message = "Funcionário não encontrado, verifique o ID informado." });

                _unitOfWork.FuncionarioRepository.Excluir(funcionario);

                var response = _mapper.Map<FuncionarioResponse>(funcionario);

                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Obtém a lista de todos os funcionários cadastrados no sistema.
        /// Retorna um status 200 com a lista de funcionários se houver registros.
        /// Retorna um status 204 se nenhum funcionário for encontrado.
        /// </summary>
        /// <returns>Retorna um código de status 200 com a lista de funcionários ou um status 204 se não houver funcionários cadastrados.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var funcionarios = _unitOfWork.FuncionarioRepository.Consultar();
                var lista = _mapper.Map<List<FuncionarioResponse>>(funcionarios);

                if (lista.Count > 0)
                    return StatusCode(200, lista);
                else
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Obtém os dados de um funcionário pelo ID informado.
        /// Retorna um status 200 com os dados do funcionário se ele for encontrado.
        /// Retorna um status 204 se o funcionário não for encontrado.
        /// </summary>
        /// <param name="idFuncionario">ID do funcionário a ser consultado.</param>
        /// <returns>Retorna um código de status 200 com os dados do funcionário ou um status 204 se o funcionário não for encontrado.</returns>
        [HttpGet("{idFuncionario}")]
        public IActionResult GetById(Guid idFuncionario)
        {
            try
            {
                var funcionario = _unitOfWork.FuncionarioRepository.ObterPorId(idFuncionario);

                if (funcionario != null)
                {
                    var response = _mapper.Map<FuncionarioResponse>(funcionario);
                    return StatusCode(200, response);
                }
                else
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
