using AutoMapper;
using JobStash.Application.Common.Mappings;
using JobStash.Domain.Entities;

namespace JobStash.Application.Ads.Queries.Dtos;

public record AdBriefDto : IMapFrom<Ad>
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string WebPage { get; set; } = null!;
    public bool Published { get; set; }
    public bool Expired { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Ad, AdBriefDto>()
            .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company.Name))
            .ForMember(d => d.WebPage, opt => opt.MapFrom(s => s.WebPage.ToString()));
    }
}
