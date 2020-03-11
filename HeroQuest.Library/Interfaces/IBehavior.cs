using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Models;

namespace HeroQuest.Library.Interfaces
{
    public interface IBehavior
    {
        BehaviorTypeEnum BehaviorTypeName { get; set; }
        MovementDirectionEnum ApplyBehavior(BehaviorPropertiesModel behaviorProperties);
    }
}
