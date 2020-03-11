using HeroQuest.Common.Interfaces;
using HeroQuest.Common.Models;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace HeroQuest.Library.GameLevelsTemplate
{
    public class GameDashboard : IGameDashboard
    {
        public DispatcherTimer DispatcherTimer_HeroPlayer { get; set; }
        public DispatcherTimer DispatcherTimer_Bat { get; set; }
        public DispatcherTimer DispatcherTimer_Wizard { get; set; }
        public DispatcherTimer DispatcherTimer_ShootingArrow { get; set; }
        public DispatcherTimer DispatcherTimer_ControlShootingWeaponsVisibilityState { get; set; }
        public DispatcherTimer DispatcherTimer_EnemyFireball { get; set; }


        public Image MainWindowImage { get; set; }
        public Image HeroPlayerImage { get; set; }

        public List<Image> AllWeaponImages { get; set; }
        public List<Image> AllPotionImages { get; set; }

        public Image MaceWeaponImage { get; set; }
        public Image SwordWeaponImage { get; set; }
        public Image BowAndArrowWeaponImage { get; set; }

        public Image BluePotionImage { get; set; }
        public Image RedPotionImage { get; set; }

        public Image BatEnemyImage { get; set; }
        public Image WizardEnemyImage { get; set; }


        public List<Image> AllSwordSwingImages { get; set; }
        public List<Image> AllMaceSwingImages { get; set; }
        public List<Image> AllArrowsShootingLeft { get; set; }
        public List<Image> AllArrowsShootingRight { get; set; }
        
        public List<Image> AllFireballLeftImages { get; set; }
        public List<Image> AllFireballRightImages { get; set; }

        private int _indexGameLevelCounter = 0;
        private IBat _bat;
        private IWizard _wizard;
        private IEnemyFireball _enemyFireball;
        private List<IEnemy> AllEnemies = new List<IEnemy>();

        ICastle _castle;
        IAllEnemiesFactory _allEnemiesFactory;
        IAllEnemyWeaponsFactory _allEnemyWeaponsFactory;
        IGameLevelInvoker _gameLevelInvoker;
        IImageManager _imageManager;
        IPlayer _player;
        IUIMediator _uiMediator;

        public GameDashboard(
            IAllEnemiesFactory allEnemiesFactory, 
            IAllEnemyWeaponsFactory allEnemyWeaponsFactory, 
            ICastle castle ,IGameLevelInvoker gameLevelInvoker, 
            IImageManager imageManager, 
            IPlayer player, 
            IUIMediator uiMediator)
        {
            _allEnemiesFactory = allEnemiesFactory;
            _allEnemyWeaponsFactory = allEnemyWeaponsFactory;
            _castle = castle;
            _gameLevelInvoker = gameLevelInvoker;
            _imageManager = imageManager;
            _player = player;
            _uiMediator = uiMediator;
        }

        public System.Windows.Shapes.Rectangle GetCastleOuterDimmensionsInUIWindow()
        {
            return _castle.GetCastleOuterLimits();
        }

        public System.Windows.Shapes.Rectangle GetCastleInnerDimmensionsInUIWindow()
        {
            return _castle.GetCastleInnerLimits();
        }

        public void RegisterHeroPlayer()
        {
            _player.ShooterImage = HeroPlayerImage;
            _player.TargetImage = HeroPlayerImage;
            _player.ShooterImage.Name = HeroPlayerImage.Name;
            _player.RegisterWPFMainWindow(MainWindowImage);
            _player.AllMaceSwingImages = AllMaceSwingImages;
            _player.AllSwordSwingImages = AllSwordSwingImages;
            _player.AllArrowShootingLeftImages = AllArrowsShootingLeft;
            _player.AllArrowShootingRightImages = AllArrowsShootingRight;

            //Add Enemies_Subscribe_Observers 
            _player.AddEnemy_SubscribeObserver(_bat);
            _player.AddEnemy_SubscribeObserver(_wizard);
        }

        public void RegisterEnemies()
        {
            _bat = (IBat)_allEnemiesFactory.CreateEnemy(EnemyNameEnum.Bat);
            _bat.EnemyImage = BatEnemyImage;
            _bat.ShooterImage = BatEnemyImage;

            _wizard = (IWizard)_allEnemiesFactory.CreateEnemy(EnemyNameEnum.Wizard);
            _wizard.EnemyImage = WizardEnemyImage;
            _wizard.ShooterImage = WizardEnemyImage;
            _wizard.Target = _player;
            _wizard.Target.TargetImage = _player.ShooterImage;
            _wizard.AllFireballLeftShootingImages = AllFireballLeftImages;
            _wizard.AllFireballRightShootingImages = AllFireballRightImages;

            AllEnemies = new List<IEnemy> { _bat, _wizard };
        }

        public void RegisterEnemyWeapons()
        {
            _enemyFireball = (IEnemyFireball)_allEnemyWeaponsFactory.CreateEnemyWeapon(EnemyWeaponsListEnum.Fireball);
        }

        public void SetEnemiesBehavior(BehaviorTypeEnum behaviorType)
        {
            foreach (var enemy in AllEnemies)
            {
                enemy.SetBehaviorType(behaviorType);
            }
        }

        public void SetGameLevel_Template(int indexGameLevel)
        {
            _indexGameLevelCounter = indexGameLevel;
            if (!AreAllLevelsExecuted())
            {
                var allIndexLevelsList = Enum.GetValues(typeof(GameLevelsEnum)).Cast<GameLevelsEnum>().ToList();
                GameLevelsEnum selectedLevel = allIndexLevelsList[indexGameLevel];
                LevelTemplate selectedGameLevel = _gameLevelInvoker.GetLevel(selectedLevel);

                if (selectedGameLevel == null) return;

                //Potions Visibility and Location
                var bluePotionLocation = (selectedGameLevel.BluePotionVisibility == Visibility.Visible) ? new System.Drawing.Point(300, 200) : new System.Drawing.Point(400, 350);
                BluePotionImage = SetSingleImageVisibilityAndLocation(BluePotionImage, bluePotionLocation, selectedGameLevel.BluePotionVisibility);

                var redPotionLocation = (selectedGameLevel.RedPotionVisibility == Visibility.Visible) ? new System.Drawing.Point(700, 255) : new System.Drawing.Point(400, 400);
                RedPotionImage = SetSingleImageVisibilityAndLocation(RedPotionImage, redPotionLocation, selectedGameLevel.RedPotionVisibility);

                //Weapons Visibility and Location
                var swordWeaponLocation = (selectedGameLevel.SwordVisibility == Visibility.Visible) ? new System.Drawing.Point(700, 300) : new System.Drawing.Point(400, 350);
                SwordWeaponImage = SetSingleImageVisibilityAndLocation(SwordWeaponImage, swordWeaponLocation, selectedGameLevel.SwordVisibility);

                var maceWeaponLocation = (selectedGameLevel.MaceVisibility == Visibility.Visible) ? new System.Drawing.Point(350, 350) : new System.Drawing.Point(400, 350);
                MaceWeaponImage = SetSingleImageVisibilityAndLocation(MaceWeaponImage, maceWeaponLocation, selectedGameLevel.MaceVisibility);

                var bowAndArrowWeaponLocation = (selectedGameLevel.BowAndArrowVisibility == Visibility.Visible) ? new System.Drawing.Point(500, 150) : new System.Drawing.Point(400, 350);
                BowAndArrowWeaponImage = SetSingleImageVisibilityAndLocation(BowAndArrowWeaponImage, bowAndArrowWeaponLocation, selectedGameLevel.BowAndArrowVisibility);

                //shooting Weapons TIMERS
                SetTimerOnOff(DispatcherTimer_ShootingArrow, selectedGameLevel.ArrowTimer);
                SetTimerOnOff(DispatcherTimer_EnemyFireball, selectedGameLevel.EnemyFireballTimer);
                SetTimerOnOff(DispatcherTimer_ControlShootingWeaponsVisibilityState, selectedGameLevel.ShootingWeaponsVisibilityTimer);

                //Player
                _player.RemoveEnemy_UnsubscribeAllObservers();
                _player.AddEnemy_SubscribeObserver(_bat);
                _player.AddEnemy_SubscribeObserver(_wizard);
                _player.SetHealth(selectedGameLevel.HeroPlayerHealthPoints);
                _player.SetMovementStrategy(selectedGameLevel.HeroPlayerMovementStrategy);
                _player.AllArrowShootingLeftImages = SetImagesNewLocationAndSetHidden(_player.AllArrowShootingLeftImages, new System.Drawing.Point(700, 250));
                _player.AllArrowShootingRightImages = SetImagesNewLocationAndSetHidden(_player.AllArrowShootingRightImages, new System.Drawing.Point(650, 250));
                SetTimerOnOff(DispatcherTimer_HeroPlayer, selectedGameLevel.HeroPlayerTimer);

                //Enemy Bat
                _bat.EnemyImage.Visibility = selectedGameLevel.BatVisibility;
                _bat.SetHealth(selectedGameLevel.BatHealthPoints);
                _bat.SetMovementStrategy(selectedGameLevel.BatMovementStrategy);
                SetTimerOnOff(DispatcherTimer_Bat, selectedGameLevel.BatTimer);

                //Enemy Wizard
                _wizard.EnemyImage.Visibility = selectedGameLevel.WizardVisibility;
                _wizard.SetHealth(selectedGameLevel.WizardHealthpoints);
                _wizard.SetMovementStrategy(selectedGameLevel.WizardMovementStrategy);
                _wizard.AllFireballLeftShootingImages = SetImagesNewLocationAndSetHidden(_wizard.AllFireballLeftShootingImages, new System.Drawing.Point(600, 250));
                _wizard.AllFireballRightShootingImages = SetImagesNewLocationAndSetHidden(_wizard.AllFireballRightShootingImages, new System.Drawing.Point(550, 250));
                SetTimerOnOff(DispatcherTimer_Wizard, selectedGameLevel.WizardTimer);

            }
        }

        public int GetIndexGameLevel()
        {
            return _indexGameLevelCounter;
        }

        public int GetAllEnemiesSummatoryHealthPoints()
        {
            int counter = 0;
            foreach(var enemy in AllEnemies)
            {
                counter += enemy.GetHealthPoints();
            }
            return counter;
        }

        public int GetGameLevelsCounted()
        {
            var result = Enum.GetValues(typeof(GameLevelsEnum)).Cast<GameLevelsEnum>().ToList();
            return result.Count();
        }

        public void Form1_KeyDown_Event(KeyEventArgs e)
        {
            _uiMediator.Form1_KeyDown_Event(e, _player, AllWeaponImages, AllPotionImages);
        }

        public void Form1_KeyUp_Event()
        {
            _uiMediator.Form1_KeyUp_Event(_player);
        }

        public LabelNotificationModel GetHeroPlayerLabelNotification()
        {
            return new LabelNotificationModel
            {
                ActiveEntities = _player.GetAllEnemies().Where(a => a.GetHealthPoints() > 0).Count(),
                HealthPoints = _player.GetHealthPoints(),
                TotalStrikePoints = _player.GetTotalAccummulatedStrikePoints(),
            };
        }

        public LabelNotificationModel GetBatLabelNotification()
        {
            return new LabelNotificationModel
            {
                ActiveEntities = 0,
                HealthPoints = _bat.GetHealthPoints(),
                TotalStrikePoints = _bat.GetTotalAccummulatedStrikePoints(),
            };
        }

        public LabelNotificationModel GetWizardLabelNotification()
        {
            return new LabelNotificationModel
            {
                ActiveEntities = 0,
                HealthPoints = _wizard.GetHealthPoints(),
                TotalStrikePoints = _wizard.GetTotalAccummulatedStrikePoints(),
            };
        }
        
        public void RegisterNotificationDispatcherTimer_HeroPlayer(NotificationTimerEnum notificationTimer)
        {
            _uiMediator.RegisterNotificationDispatcherTimer_HeroPlayer(_player, notificationTimer);
        }

        public void RegisterNotificationDispatcherTimer_Arrow(NotificationTimerEnum notificationTimer)
        {
            _uiMediator.RegisterNotificationDispatcherTimer_Arrow(_player, notificationTimer);
        }

        public void RegisterNotificationDispatcherTimer_Bat(NotificationTimerEnum notificationTimer)
        {
            _uiMediator.RegisterNotificationDispatcherTimer_Bat(_player, _bat, notificationTimer);
        }

        public void RegisterNotificationDispatcherTimer_Wizard(NotificationTimerEnum notificationTimer)
        {
            _uiMediator.RegisterNotificationDispatcherTimer_Wizard(_player, _wizard, notificationTimer);
        }

        public void RegisterNotificationDispatcherTimer_EnemyFireball(NotificationTimerEnum notificationTimer)
        {
            _uiMediator.RegisterNotificationDispatcherTimer_EnemyFireball(_enemyFireball, notificationTimer);
        }

        public Image SetSingleImageVisibilityAndLocation(Image image, System.Drawing.Point point, Visibility visibility)
        {
            Canvas.SetLeft(image, point.X);
            Canvas.SetTop(image, point.Y);
            image.Visibility = visibility;
            return image;
        }
       
        public void SetTimerOnOff(DispatcherTimer dispatcherTimer, bool switchOnOf)
        {
            if(switchOnOf == true)
            {
                dispatcherTimer.Start();
            }
            if (switchOnOf == false)
            {
                dispatcherTimer.Stop();
            }
        }

        public bool AreAllLevelsExecuted()
        {
            var allIndexLevelsList = Enum.GetValues(typeof(GameLevelsEnum)).Cast<GameLevelsEnum>().ToList();

            if (_indexGameLevelCounter == allIndexLevelsList.Count)
                return true;
            else
                return false;
        }

        #region Private Methods
        private List<Image> SetImagesNewLocationAndSetHidden(List<Image> allImages,System.Drawing.Point point)
        {
            foreach (var image in allImages)
            {
                _imageManager.SetNewLocationImage(image, point);
                image.Visibility = Visibility.Hidden;
            }
            return allImages;
        }
        #endregion
    }
}
