using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HeroQuest.Library.MovementStrategy
{
    public class ZigZagStrategy : IZigZagStrategy
    {
        public MovementStrategyEnum MovementStrategyName { get; set; }

        Point newLocation = new Point();
        private int _standardMovement = 5;
        private int _maximumDistance = 15;
        private int zigZag_Up = 0;
        private int zigZag_Down = 0;
        private int axisCenterPoint_Y = 0;
        private int negativeIndicator = -1;
        private int positiveIndicator = 1;

        private int zigZag_Left = 0;
        private int zigZag_Right = 0;
        private int axisCenterPoint_X = 0;
        private Random random = new Random();

        List<IBehavior> _behaviors = new List<IBehavior>();
        public ZigZagStrategy()
        {
            MovementStrategyName = MovementStrategyEnum.ZigZagMovement;
        }
        
        public Point GetMovement(MovementDirectionEnum movementDirection)
        {
            var randomDistance = random.Next(1, _maximumDistance);
            //if LEFT OR RIGHT MOVEMENT then zig zag is up and down
            //                          *                       *
            //                    *                         *
            //<<<<<<<<<---------------------------------------------------------------->>>>>
            //           *       *           *          *
            //      *
            //   

            if (movementDirection == MovementDirectionEnum.MovingRight)
            {
                var movementToTheRight = _standardMovement * 1;
                newLocation = Process_ZigZag_UpAndDown(movementToTheRight, randomDistance);
            }

            if (movementDirection == MovementDirectionEnum.MovingLeft)
            {
                var movementToTheLeft = _standardMovement * -1;
                newLocation = Process_ZigZag_UpAndDown(movementToTheLeft, randomDistance);
            }

            // if UP OR DOWN MOVEMENT then zig zag left and right,
            //        |       *
            //     *  |
            //        |   *
            //     *  |
            // *      |
            //   *    |
            //        |   *
            //        |        *
            //        |    *
            if (movementDirection == MovementDirectionEnum.MovingUp)
            {
                var movementUp = _standardMovement * (-1);
                newLocation = Process_ZigZag_LeftAndRight(movementUp, randomDistance);
            }

            if (movementDirection == MovementDirectionEnum.MovingDown)
            {
                var movementDown = _standardMovement * (1);
                newLocation = Process_ZigZag_LeftAndRight(movementDown, randomDistance);
            }
            if(movementDirection == MovementDirectionEnum.MovingDiagonal)
            {
                //We keep things as they originally are and we do not re-set the stored previous
                //direction so if the image is constantly moving in a previously setup direction
                //when moving diagonally will keep its movement angle
                int variationX = 0, variationY = 0; 
                newLocation.X = newLocation.X + variationX;
                newLocation.Y = newLocation.Y + variationY;
            }
            if(movementDirection == MovementDirectionEnum.NotMoving)
            {
                //The image will keep static with not movement at all.
                newLocation.X = 0;
                newLocation.Y = 0;
            }

            return newLocation;
        }

        private Point Process_ZigZag_UpAndDown(int standardMovement, int randomDistance)
        {
            newLocation.X = 0; newLocation.Y = 0;
            if (zigZag_Up == 0)
            {
                zigZag_Up++;
                zigZag_Down = 0;
                return GetNewLocationPoint_Modify_Y(standardMovement, randomDistance, negativeIndicator);
            }

            if (zigZag_Down == 0)
            {
                zigZag_Down++;
                zigZag_Up = 0;
                return GetNewLocationPoint_Modify_Y(standardMovement, randomDistance, positiveIndicator);
            }
            return newLocation;
        }

        private Point Process_ZigZag_LeftAndRight(int standardMovement, int randomDistance)
        {
            newLocation.X = 0; newLocation.Y = 0;
            if (zigZag_Left == 0)
            {
                zigZag_Left++;
                zigZag_Right = 0;
                return GetNewLocationPoint_Modify_X(standardMovement, randomDistance, negativeIndicator);
            }

            if (zigZag_Right == 0)
            {
                zigZag_Right++;
                zigZag_Left = 0;
                return GetNewLocationPoint_Modify_X(standardMovement, randomDistance, positiveIndicator);
            }
            return newLocation;
        }

        private Point GetNewLocationPoint_Modify_Y(int standardMovement, int randomDistance, int upDownIndicator)
        {
            newLocation.X = standardMovement;
            newLocation.Y = +(randomDistance + axisCenterPoint_Y) * (upDownIndicator);
            axisCenterPoint_Y = randomDistance;
            return newLocation;
        }

        private Point GetNewLocationPoint_Modify_X(int standardMovement, int randomDistance, int upDownIndicator)
        {
            newLocation.X = (randomDistance + axisCenterPoint_X) * (upDownIndicator);
            newLocation.Y = standardMovement;
            axisCenterPoint_X = randomDistance;
            return newLocation;
        }
    }
}
