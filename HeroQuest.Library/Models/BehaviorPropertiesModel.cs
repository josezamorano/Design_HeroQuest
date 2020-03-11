using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using System.Windows;

namespace HeroQuest.Library.Models
{
    public class BehaviorPropertiesModel
    {
        public EnemyNameEnum EnemyName { get; set; }
        //Optional Property Required for RANDOM Behavior Only
        public MovementDirectionEnum? MovementDirection { get; set; }

        //Optional Properties Required for TARGETED Behavior only
        public Point? Target { get; set; }
        public Point? Chaser { get; set; }
    }
}
