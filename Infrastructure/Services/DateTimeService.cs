using JobStash.Application.Common.Interfaces;

namespace JobStash.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}