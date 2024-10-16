using AutoMapper;
using InfoDengue.Api.Request;
using InfoDengue.Infra.Entities;

namespace InfoDengue.Api.Mappings
{
    public class RequestToEntityMap : Profile
    {
        public RequestToEntityMap()
        {      
            CreateMap<FuncionarioPostRequest, Funcionario>();
            CreateMap<FuncionarioPutRequest, Funcionario>();
        }
    }
}