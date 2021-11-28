using AutoMapper;
using MetaData.Entities;
using MVCApplication.Models.Cliente;

namespace MVCApplication.Infrastructure
{
    public class AutoMapperGenericProfiler : Profile
    {
        public AutoMapperGenericProfiler()
        {
            this.CreateMap<ClienteInsertViewModel, Cliente>();
            this.CreateMap<Cliente, ClienteQueryViewModel>();
        }
    }
}
