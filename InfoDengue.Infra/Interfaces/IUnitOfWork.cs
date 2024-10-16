using InfoDengue.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengue.Infra.Data.Interfaces
{
    public interface IUnitOfWork
    {
        //métodos para controle de transação
        void BeginTransaction();
        void Commit();
        void Roolback();


        //métodos para acesso aos repositórios
        public IFuncionarioRepository FuncionarioRepository { get; }
        public IPerfilRepository PerfilRepository { get; }

    }
}
