using AutoMapper;
using Finans.DTO;
using Finans.Models;

namespace Finans.Profiles
{
    public class ImoveisProfile : Profile
    {
        public ImoveisProfile()
        {
            CreateMap<Endereco, Imovel>();
            CreateMap<PostImovelDTO, Imovel>();
            CreateMap<Imovel, Imovel>();
        }
    }
}
