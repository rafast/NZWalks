using AutoMapper;
using NZWalks.Models.Domain;

namespace NZWalks.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Region, Models.DTOs.Region>()
                .ReverseMap();
            CreateMap<Models.DTOs.AddRegionRequest, Region>();
            CreateMap<Models.DTOs.UpdateRegionRequest, Region>();
        }
    }
}
