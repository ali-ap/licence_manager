using System;
using AutoMapper;
using Licence.Service.Common.Model.Request;
using Licence.Service.Common.Model.Response;

namespace Licence.Service.Common.Mapper
{

    public class LicenceProfile : Profile
    {
        public LicenceProfile()
        {
            CreateMap<RegisterNewLicenceBindingModel,Domain.Licence>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.GetHashCode().ToString()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

       


        }
    }
}