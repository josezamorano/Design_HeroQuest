using HeroQuest.Library.Enums;
using System.Collections.Generic;

namespace HeroQuest.Library.Interfaces
{
    public interface IWeapon
    {
        WeaponsListEnum WeaponName { get; set; }
        bool IsHeldByPlayer { get; set; }
        int DamagePower { get; set; }
        System.Windows.Controls.Image ImageWeapon { get; set; }
        bool IsWeaponReadyForStrike { get; set; }

        bool Strike(IPlayer shooter);
        void SetNotificationTimer(NotificationTimerEnum notificationTimer);
        void SetWeaponMovementWhenIsCarriedByPlayer_GetNotification(System.Windows.Controls.Image PlayerImage);
    }
}
