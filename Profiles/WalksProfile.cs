using AutoMapper;
using NZWalks.Models.Domain;

namespace NZWalks.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<Walk, Models.DTOs.Walk>()
                .ReverseMap();
            CreateMap<WalkDifficulty, Models.DTOs.WalkDifficulty>()
                .ReverseMap();
            CreateMap<Models.DTOs.AddWalkRequest, Walk>();
            CreateMap<Models.DTOs.UpdateWalkRequest, Walk>();
            CreateMap<Models.DTOs.AddWalkDifficultyRequest, WalkDifficulty>();
            CreateMap<Models.DTOs.UpdateWalkDifficultyRequest, WalkDifficulty>();
        }
    }
}
