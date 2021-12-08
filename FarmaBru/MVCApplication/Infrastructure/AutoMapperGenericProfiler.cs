using AutoMapper;
using MetaData.Entities;
using API.Models.Cliente;

namespace API.Infrastructure
{
    public class AutoMapperGenericProfiler : Profile
    {
        public AutoMapperGenericProfiler()
        {
            this.CreateMap<ClienteInsertViewModel, Cliente>();
            this.CreateMap<Cliente, ClienteQueryViewModel>();
            this.CreateMap<Cliente, ClienteUpdateViewModel>();
            this.CreateMap<ClienteUpdateViewModel, Cliente>();
        }
    }
}
