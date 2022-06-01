using Domain.Entities;
using Domain.Events.Ads;

namespace Domain.UnitTests;

public class AdTests
{
    [Fact]
    public void NewAdHasEmptyTechnologiesCollection()
    {
        Ad ad = new Ad();
        Assert.Empty(ad.Technologies);
    }

    [Fact]
    public void PublishingAdShouldEmitPublishedEvent()
    {
        var ad = new Ad();
        ad.Published = true;

        var events = ad.DomainEvents;
        var publishEvent = events.FirstOrDefault();
        Assert.Equal(1, events.Count);
        Assert.NotNull(publishEvent);
        Assert.IsType<AdPublishedEvent>(publishEvent);
    }

    [Fact]
    public void PublishingAdTwiceShouldEmitPublishedEventOnce()
    {
        var ad = new Ad();
        ad.Published = true;
        ad.Published = true;

        var events = ad.DomainEvents;
        var publishEvent = events.FirstOrDefault();
        
        Assert.Equal(1, events.Count);
        Assert.NotNull(publishEvent);
        Assert.IsType<AdPublishedEvent>(publishEvent);
    }

    [Fact]
    public void ExpiredAdShouldEmitExpiredEvent()
    {
        var ad = new Ad();
        ad.Expired = true;

        var events = ad.DomainEvents;
        var expiredEvent = events.FirstOrDefault();
        Assert.Equal(1, events.Count);
        Assert.NotNull(expiredEvent);
        Assert.IsType<AdExpiredEvent>(expiredEvent);
    }

    [Fact]
    public void ExpitedAdTwiceShouldEmitExpiredEventOnce()
    {
        var ad = new Ad();
        ad.Expired = true;

        var events = ad.DomainEvents;
        var expiredEvent = events.FirstOrDefault();

        Assert.Equal(1, events.Count);
        Assert.NotNull(expiredEvent);
        Assert.IsType<AdExpiredEvent>(expiredEvent);
    }
}
