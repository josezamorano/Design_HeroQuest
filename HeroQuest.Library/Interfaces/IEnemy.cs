using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface IEnemy: IEnemyShooter
    {
        EnemyNameEnum EnemyName { get; set; }
        System.Windows.Controls.Image EnemyImage { get; set; }
        MovementDirectionEnum MovementDirection { get; set; }

        void SetBehaviorType(BehaviorTypeEnum behaviorType);
        void SetHealth(HealthStatesEnum healthState);
        void SetMovementStrategy(MovementStrategyEnum movementStrategy);
        
       
        int GetTotalAccummulatedStrikePoints();

        void IncreaseHealth(int pointsStrength);
        void ReduceHealth(int damagePoints);
        int GetHealthPoints();
        
        void SetPlayerLocation_ReceiveNotification(System.Windows.Point locationPoint);
        void Move(MovementDirectionEnum movementDirection);
    }
}
