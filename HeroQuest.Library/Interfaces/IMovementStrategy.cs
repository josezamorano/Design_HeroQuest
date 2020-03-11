using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using System.Drawing;

namespace HeroQuest.Library.Interfaces
{
    public interface IMovementStrategy
    {
        MovementStrategyEnum MovementStrategyName { get; set; }
        Point GetMovement(MovementDirectionEnum movementDirection);
    }
}
