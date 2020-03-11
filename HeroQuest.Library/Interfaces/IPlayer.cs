using HeroQuest.Common.Enums;
using HeroQuest.Library.Enums;
using System.Collections.Generic;

namespace HeroQuest.Library.Interfaces
{
    public interface IPlayer : IShooter, ITarget
    {
        void SetHealth(HealthStatesEnum healthState);
        void SetMovementStrategy(MovementStrategyEnum movementStrategy);
        void RegisterWPFMainWindow(System.Windows.Controls.Image mainWindow);
        
        int GetTotalAccummulatedStrikePoints();
        void SetSpeedMovementByHealthState(IHealthState healthState);
        void PlayerMove(MovementDirectionEnum movementDirection);
        void PickupItem(System.Windows.Controls.Image image);
        bool DropWeapon_RemoveObserver();
        List<IWeapon> GetWeapons();
        void AddEnemy_SubscribeObserver(IEnemy enemy);
        void RemoveEnemy_UnsubscribeObserver(IEnemy enemy);
        void RemoveEnemy_UnsubscribeAllObservers();
        void PublishPlayerLocationPoint_NotifyObservers();
    }
}
