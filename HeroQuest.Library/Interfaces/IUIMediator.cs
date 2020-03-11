using HeroQuest.Library.Enums;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;

namespace HeroQuest.Library.Interfaces
{
    public interface IUIMediator
    {
        void RegisterNotificationDispatcherTimer_HeroPlayer(IPlayer player,NotificationTimerEnum notificationTimer);
        void RegisterNotificationDispatcherTimer_Arrow(IPlayer player, NotificationTimerEnum notificationTimer);
        void RegisterNotificationDispatcherTimer_Bat(IPlayer player, IBat bat, NotificationTimerEnum notificationTomern);
        void RegisterNotificationDispatcherTimer_Wizard(IPlayer player, IWizard wizard, NotificationTimerEnum notificationTimer);
        void RegisterNotificationDispatcherTimer_EnemyFireball(IEnemyFireball enemyFireball, NotificationTimerEnum notificationTimer);
        
        void Form1_KeyDown_Event(KeyEventArgs e, IPlayer player, List<Image> allWeaponsImages, List<Image> allPotionsImages);
        void Form1_KeyUp_Event(IPlayer player);
    }
}
