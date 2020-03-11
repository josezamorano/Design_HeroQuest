using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System.Windows;

namespace HeroQuest.Library.BehavioralMovement
{
    public class TargetedBehavior : ITargetedBehavior
    {
        public BehaviorTypeEnum BehaviorTypeName { get; set; }

        private int horizontalVerification = 0;
        private int verticalVerification = 0;
        public TargetedBehavior()
        {
            BehaviorTypeName = BehaviorTypeEnum.Targeted;
        }

        public MovementDirectionEnum ApplyBehavior(BehaviorPropertiesModel behaviorProperties)
        {
            if (behaviorProperties.Target.HasValue && behaviorProperties.Chaser.HasValue)
            {
                Point targetLocation = (Point)behaviorProperties.Target;
                Point chaserLocation = (Point)behaviorProperties.Chaser;
                if(horizontalVerification == 0)
                {
                    horizontalVerification++;
                    verticalVerification = 0;

                    //Movement To the Left
                    if(targetLocation.X < chaserLocation.X)
                    {
                        return MovementDirectionEnum.MovingLeft;
                    }

                    //Movement To the Right
                    if (targetLocation.X > chaserLocation.X)
                    {
                        return MovementDirectionEnum.MovingRight;
                    }
                }
                if(verticalVerification == 0)
                {
                    verticalVerification++;
                    horizontalVerification = 0;
                    //Movement Up
                    if(targetLocation.Y < chaserLocation.Y)
                    {
                        return MovementDirectionEnum.MovingUp;
                    }
                    //Movement Down
                    if(targetLocation.Y > chaserLocation.Y)
                    {
                        return MovementDirectionEnum.MovingDown;
                    }
                }
            }
            return MovementDirectionEnum.NotMoving;
        }
    }
}
