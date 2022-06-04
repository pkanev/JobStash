using AutoMapper;
using JobStash.Application.Common.Mappings;
using JobStash.Domain.Entities;

namespace JobStash.Application.Ads.Queries.Dtos;

public record AdDto : IMapFrom<Ad>
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string CompanyWebPage { get; set; } = null!;
    public string WebPage { get; set; } = null!;
    public ICollection<Tuple<string, int>> Technologies { get; set; } = new List<Tuple<string, int>>();
    public bool Published { get; set; }
    public bool Expired { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Ad, AdDto>()
            .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company.Name))
            .ForMember(d => d.WebPage, opt => opt.MapFrom(s => s.WebPage.ToString()))
            .ForMember(d => d.Technologies, opt => opt.MapFrom(s => s.Technologies.Select(t => new Tuple<string, int>(t.Name, t.Id))));
    }
}
