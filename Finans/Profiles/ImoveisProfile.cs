using AutoMapper;
using Finans.Models;

namespace Finans.Profiles
{
    public class ImoveisProfile : Profile
    {
        public ImoveisProfile()
        {
            CreateMap<Endereco, Imovel>();
        }
    }
}
