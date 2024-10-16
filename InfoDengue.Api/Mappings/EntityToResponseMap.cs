using AutoMapper;
using InfoDengue.Api.Response;
using InfoDengue.Infra.Entities;

namespace InfoDengue.Api.Mappings
{
    public class EntityToResponseMap : Profile
    {
        public EntityToResponseMap()
        {
            CreateMap<Funcionario, FuncionarioResponse>();
        }
    }
}
