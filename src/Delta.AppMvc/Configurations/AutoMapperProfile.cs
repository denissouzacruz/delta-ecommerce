using AutoMapper;
using Delta.AppMvc.ViewModel;
using Delta.Business.Models;

namespace Delta.AppMvc.Configurations
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //Quando a aplicação subir, este mapeamento já é definido
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            //CreateMap<Vendedor, VendedorViewModel>().ReverseMap();

        }
    }
}
