using AutoMapper;
using Finans.DTO;
using Finans.Models;

namespace Finans.Profiles
{
    public class ContasPagarProfile : Profile
    {
        public ContasPagarProfile()
        {
            CreateMap<ContasPagarDto, ContasPagar>();
            CreateMap<ContaPagaDto, ContasPagar>();
        }
    }
}
