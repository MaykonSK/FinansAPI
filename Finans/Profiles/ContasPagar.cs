using AutoMapper;
using Finans.DTO;

namespace Finans.Profiles
{
    public class ContasPagar : Profile
    {
        public ContasPagar()
        {
            CreateMap<ContasPagar, ContasPagarDto>();
            CreateMap<ContasPagarDto, ContasPagar>();
        }
    }
}
