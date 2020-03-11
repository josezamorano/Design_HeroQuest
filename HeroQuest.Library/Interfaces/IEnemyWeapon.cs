using HeroQuest.Library.Enums;

namespace HeroQuest.Library.Interfaces
{
    public interface IEnemyWeapon
    {
        int DamagePower { get; set; }
        bool Strike(IEnemy shooter);
        void SetNotificationTimer(NotificationTimerEnum notificationTimer);
    }
}
