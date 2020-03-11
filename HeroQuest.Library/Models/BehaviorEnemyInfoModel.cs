using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Models
{
    public class BehaviorEnemyInfoModel
    {
        public EnemyNameEnum EnemyName { get; set; }
        public int ConsecutiveMovements { get; set; }
        public int IterationCounter { get; set; }
    }
}
