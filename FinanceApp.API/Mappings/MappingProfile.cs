using AutoMapper;
using FinanceApp.API.Models.Categoria;
using FinanceApp.API.Models.MetodoPago;
using FinanceApp.API.Models.Tipo;
using FinanceApp.API.Models.Usuario;
using FinanceApp.Domain.Entities;

namespace FinanceApp.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Categoria
            CreateMap<CategoriaCreate, Categoria>();
            CreateMap<CategoriaUpdate, Categoria>();

            //Tipo
            CreateMap<TipoCreate, Tipo>();  
            CreateMap<TipoUpdate, Tipo>();

            //MetodoPago
            CreateMap<MetodoPagoCreate, MetodoPago>();
            CreateMap<MetodoPagoUpdate, MetodoPago>();


            //Usuario
            CreateMap<UsuarioCreate, Usuario>();
            CreateMap<UsuarioUpdate, Usuario>();
        }
    }
}
