using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Drawing;

namespace HeroQuest.Library.MovementStrategy
{
    class LinearStrategy : ILinearStrategy
    {
        public MovementStrategyEnum MovementStrategyName { get; set; }

        public LinearStrategy()
        {
            MovementStrategyName = MovementStrategyEnum.LinearMovement;
        }

        private readonly int defaultDistance = 1;
        Point newLocation = new Point();
        public Point GetMovement(MovementDirectionEnum movementDirectionEnum)
        {
            switch (movementDirectionEnum)
            {
                case MovementDirectionEnum.NotMoving:
                    newLocation.X = 0;
                    newLocation.Y = 0;
                    break;

                case MovementDirectionEnum.MovingLeft:
                    newLocation.X = defaultDistance * -1;
                    newLocation.Y = 0;
                    break;

                case MovementDirectionEnum.MovingRight:
                    newLocation.X = defaultDistance;
                    newLocation.Y = 0;
                    break;

                case MovementDirectionEnum.MovingUp:
                    newLocation.X = 0;
                    newLocation.Y = defaultDistance * -1;
                    break;

                case MovementDirectionEnum.MovingDown:
                    newLocation.X = 0;
                    newLocation.Y = defaultDistance;
                    break;
                    
                default: break;
            }
            return newLocation;
        }
    }
}
