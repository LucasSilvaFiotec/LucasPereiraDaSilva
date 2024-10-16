using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengue.Infra.Entities
{
    public class Perfil
    {
        public Guid IdPerfil { get; set; } 
        public string Nome { get; set; } 
        public string Descricao { get; set; } 
    }
}
