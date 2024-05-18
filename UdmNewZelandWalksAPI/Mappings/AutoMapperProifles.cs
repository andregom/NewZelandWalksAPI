using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using UdmNewZelandWalksAPI.Models.Domain;
using UdmNewZelandWalksAPI.Models.DTO;

namespace UdmNewZelandWalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }
}
