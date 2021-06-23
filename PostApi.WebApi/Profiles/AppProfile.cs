using AutoMapper;
using PostApi.Dtos.Dtos;
using PostApi.Models.Models;

namespace PostApi.WebApi.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            this.CreateMap<Usuario, UsuarioDto>().ReverseMap();
            this.CreateMap<Usuario, UsuarioPostDto>().ReverseMap();
            this.CreateMap<Usuario, LoginDto>().ReverseMap();
            this.CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}
