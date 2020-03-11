using HeroQuest.Common.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroQuest.Library.MovementStrategy
{
    public class MovementDirectionManager: IMovementDirectionManager
    {
        private int lowerLimit = 1;
        Random random = new Random();
        public MovementDirectionEnum GetOppositeDirection(MovementDirectionEnum movementDirection)
        {
            switch (movementDirection)
            {
                case MovementDirectionEnum.MovingDown:
                    return MovementDirectionEnum.MovingUp;

                case MovementDirectionEnum.MovingUp:
                    return MovementDirectionEnum.MovingDown;

                case MovementDirectionEnum.MovingLeft:
                    return MovementDirectionEnum.MovingRight;

                case MovementDirectionEnum.MovingRight:
                    return MovementDirectionEnum.MovingLeft;
            }
            return MovementDirectionEnum.NotMoving;
        }

        public MovementDirectionEnum GetRandomDirection()
        {
            var allMovements = Enum.GetValues(typeof(MovementDirectionEnum)).Cast<MovementDirectionEnum>().ToList();
            var enumResult = random.Next(lowerLimit, (allMovements.Count-1));
            var index = enumResult;
            var selectedMovement = allMovements[index];
            return selectedMovement;
        }

        public MovementDirectionEnum GetNonRepeatingRandomDirection(MovementDirectionEnum movement)
        {
            List<MovementDirectionEnum> allMovements = Enum.GetValues(typeof(MovementDirectionEnum)).Cast<MovementDirectionEnum>().ToList();
            allMovements.Remove(movement);
            var index = random.Next(lowerLimit, (allMovements.Count-1));
            var selectedMovement = allMovements[index];
            return selectedMovement;
        }
    }
}
