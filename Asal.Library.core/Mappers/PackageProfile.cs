using Asal.Library.Abstraction.ViewModels;
using Asal.Library.core.Models.Data;
using AutoMapper;

namespace Asal.Library.core.Mappers
{
    public class PackageProfile : Profile
    {
        public PackageProfile()
        {
            CreateMap<PackageDetail, PackageDetailViewModel>()
                .ReverseMap();
        }
    }
}
