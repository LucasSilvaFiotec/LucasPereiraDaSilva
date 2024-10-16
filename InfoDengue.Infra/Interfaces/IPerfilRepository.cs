using InfoDengue.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengue.Infra.Interfaces
{
    public interface IPerfilRepository : IBaseRepository<Perfil>
    {
        Perfil ObterPorNome(string nome); 
    }
}
