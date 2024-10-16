using InfoDengue.Infra.Data.Contexts;
using InfoDengue.Infra.Entities;
using InfoDengue.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengue.Infra.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        //atributo
        private readonly SqlServerContext _context;

        //construtor para injeção de dependência
        public PerfilRepository(SqlServerContext context)
        {
            _context = context;
        }

        public void Alterar(Perfil entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        
        public void Excluir(Perfil entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Inserir(Perfil entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public List<Perfil> Consultar()
        {
            return _context.Perfil
                            .OrderBy(p => p.Nome)
                            .ToList();
        }

        public Perfil ObterPorId(Guid id)
        {
            return _context.Perfil
                .FirstOrDefault(p => p.IdPerfil.Equals(id));
        }

        public Perfil ObterPorNome(string nome)
        {
            return _context.Perfil
                .FirstOrDefault(p => p.Nome.Equals(nome));
        }
    }
}
