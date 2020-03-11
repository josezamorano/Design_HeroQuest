using HeroQuest.Common.Models;
using HeroQuest.Library.Enums;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeroQuest.Library.Interfaces
{
    public interface IGameDashboard
    {
        DispatcherTimer DispatcherTimer_HeroPlayer { get; set; }
        DispatcherTimer DispatcherTimer_Bat { get; set; }
        DispatcherTimer DispatcherTimer_Wizard { get; set; }
        DispatcherTimer DispatcherTimer_ShootingArrow { get; set; }
        DispatcherTimer DispatcherTimer_ControlShootingWeaponsVisibilityState { get; set; }
        DispatcherTimer DispatcherTimer_EnemyFireball { get; set; }

        Image MainWindowImage { get; set; }
        Image HeroPlayerImage { get; set; }

        Image MaceWeaponImage { get; set; }
        Image SwordWeaponImage { get; set; }
        Image BowAndArrowWeaponImage { get; set; }

        Image BluePotionImage { get; set; }
        Image RedPotionImage { get; set; }

        Image BatEnemyImage { get; set; }
        Image WizardEnemyImage { get; set; }

        List<Image> AllWeaponImages { get; set; }
        List<Image> AllPotionImages { get; set; }
        
        List<Image> AllSwordSwingImages { get; set; }
        List<Image> AllMaceSwingImages { get; set; }
        List<Image> AllArrowsShootingLeft { get; set; }
        List<Image> AllArrowsShootingRight { get; set; }

        List<Image> AllFireballLeftImages { get; set; }
        List<Image> AllFireballRightImages { get; set; }

        Rectangle GetCastleInnerDimmensionsInUIWindow();
        Rectangle GetCastleOuterDimmensionsInUIWindow();

        void RegisterHeroPlayer();
        void RegisterEnemies();
        void RegisterEnemyWeapons();
        void SetEnemiesBehavior(BehaviorTypeEnum behaviorType);
        void SetGameLevel_Template(int indexGameLevel);

        int GetIndexGameLevel();
        int GetAllEnemiesSummatoryHealthPoints();
        int GetGameLevelsCounted();

        void Form1_KeyDown_Event(KeyEventArgs e);
        void Form1_KeyUp_Event();

        LabelNotificationModel GetHeroPlayerLabelNotification();
        LabelNotificationModel GetBatLabelNotification();
        LabelNotificationModel GetWizardLabelNotification();

        void RegisterNotificationDispatcherTimer_HeroPlayer(NotificationTimerEnum notificationTimer);
        void RegisterNotificationDispatcherTimer_Arrow(NotificationTimerEnum notificationTimer);
        void RegisterNotificationDispatcherTimer_Bat(NotificationTimerEnum notificationTimer);
        void RegisterNotificationDispatcherTimer_Wizard(NotificationTimerEnum notificationTimer);
        void RegisterNotificationDispatcherTimer_EnemyFireball(NotificationTimerEnum notificationTimer);

        Image SetSingleImageVisibilityAndLocation(Image image, System.Drawing.Point point, Visibility visibility);
        void SetTimerOnOff(DispatcherTimer dispatcherTimer, bool switchOnOf);
        bool AreAllLevelsExecuted();
    }
}
