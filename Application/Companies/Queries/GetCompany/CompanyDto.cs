using AutoMapper;
using JobStash.Application.Common.Mappings;
using JobStash.Domain.Entities;

namespace JobStash.Application.Companies.Queries.GetCompany;

public record CompanyDto : IMapFrom<Company>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? WebPage { get; set; }
    public string? CareersPage { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Company, CompanyDto>()
            .ForMember(d => d.WebPage, opt => opt.MapFrom(s => s.WebPage != null ? s.WebPage.ToString() : String.Empty))
            .ForMember(d => d.CareersPage, opt => opt.MapFrom(s => s.CareersPage != null ? s.CareersPage.ToString() : String.Empty));
    }
}
