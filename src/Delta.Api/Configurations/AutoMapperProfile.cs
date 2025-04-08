using AutoMapper;
using Delta.Api.Models;

namespace Delta.Api.Configurations
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Categoria, Business.Models.Categoria>().ReverseMap();
            CreateMap<Models.Produto, Business.Models.Produto>().ReverseMap();
            //CreateMap<Vendedor, VendedorViewModel>().ReverseMap();

        }
    }
}
