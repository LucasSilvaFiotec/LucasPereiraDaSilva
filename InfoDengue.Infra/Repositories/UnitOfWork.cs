using InfoDengue.Infra.Data.Contexts;
using InfoDengue.Infra.Data.Interfaces;
using InfoDengue.Infra.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengue.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //atributo
        private readonly SqlServerContext _context;
        private IDbContextTransaction _transaction;

        //construtor para injeção de dependência
        public UnitOfWork(SqlServerContext context)
        {
            _context = context;
        }

        public IFuncionarioRepository FuncionarioRepository => throw new NotImplementedException();
       
        public IPerfilRepository PerfilRepository => throw new NotImplementedException();

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Roolback()
        {
            _transaction.Rollback();
        }
    }
}
