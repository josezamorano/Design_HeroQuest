using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface IBehaviorInvoker
    {
        IBehavior GetSelectedBehavior(BehaviorTypeEnum behaviorType);
    }
}
