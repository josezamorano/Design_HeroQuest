using HeroQuest.Common.Enums;
namespace HeroQuest.Library.Interfaces
{
    public interface IMovementDirectionManager
    {
        MovementDirectionEnum GetOppositeDirection(MovementDirectionEnum movementDirection);
        MovementDirectionEnum GetRandomDirection();
        MovementDirectionEnum GetNonRepeatingRandomDirection(MovementDirectionEnum movement);
    }
}
