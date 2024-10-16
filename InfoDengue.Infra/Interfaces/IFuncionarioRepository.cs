using InfoDengue.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengue.Infra.Interfaces
{
    public interface IFuncionarioRepository : IBaseRepository<Funcionario>
    {       
        Funcionario ObterPorCpf(string cpf);

        Funcionario ObterPorMatricula(string matricula);

        List<Funcionario> ObterPorNome(string nome);
    }
}