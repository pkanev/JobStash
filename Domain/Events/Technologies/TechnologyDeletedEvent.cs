using JobStash.Domain.Common;
using JobStash.Domain.Entities;

namespace JobStash.Domain.Events.Technologies
{
    public class TechnologyDeletedEvent : BaseEvent
    {
        public Technology Technology { get; }

        public TechnologyDeletedEvent(Technology technology)
        {
            Technology = technology;
        }
    }
}
