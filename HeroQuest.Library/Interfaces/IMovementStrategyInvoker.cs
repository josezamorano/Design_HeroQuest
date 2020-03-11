using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface IMovementStrategyInvoker
    {
        IMovementStrategy GetMovementStrategy(MovementStrategyEnum movementStrategy);
    }
}
