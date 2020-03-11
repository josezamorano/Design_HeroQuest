using HeroQuest.Library.DependencyInjection;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeroQuest.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        IGameDashboard _gameDashboard;
        private List<Image> AllEnemiesImages;
        private DispatcherTimer DispatcherTimer_Wizard;
        private DispatcherTimer DispatcherTimer_Bat;
        private int initialGameLevel = 1;
        
        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(this.Form1_KeyUp);
            _gameDashboard = ContainerConfig.GetInstance<IGameDashboard>();
            ImagesRegistration();
            //First Register Enemies
            _gameDashboard.RegisterEnemies();
            _gameDashboard.RegisterEnemyWeapons();
            //Then Register the Player and Add Enemies to the Player
            _gameDashboard.RegisterHeroPlayer();
            Rectangle castleInnerDimmensions = _gameDashboard.GetCastleInnerDimmensionsInUIWindow();
            Rectangle castleOuterDimmensions = _gameDashboard.GetCastleOuterDimmensionsInUIWindow();
            Canvas1.Children.Add(castleInnerDimmensions);
            Canvas1.Children.Add(castleOuterDimmensions);
            SetInitialEnemiesBehavior();
            DefineTimers();
            _gameDashboard.SetGameLevel_Template(initialGameLevel);
        }
        #region Initial Dialog Notification
        private void SetInitialEnemiesBehavior()
        {
            MessageBoxResult dialogDashboard = MessageBox.Show("Select Yes if you wish your " +
                                                " enemies to chase you, otherwise they will move randomly. ",
                                                "Select your type of game",
                                                MessageBoxButton.YesNo);
            switch (dialogDashboard)
            {
                case MessageBoxResult.Yes:
                    _gameDashboard.SetEnemiesBehavior(BehaviorTypeEnum.Targeted);
                    break;

                case MessageBoxResult.No:
                    _gameDashboard.SetEnemiesBehavior(BehaviorTypeEnum.Random);
                    break;
            }
        }
        #endregion
        
        #region SetTimers
        private void DefineTimers()
        {
            _gameDashboard.DispatcherTimer_HeroPlayer = CreateTimer_HeroPlayer();
            DispatcherTimer_Bat = CreateTimer_BatEnemy();
            _gameDashboard.DispatcherTimer_Bat = DispatcherTimer_Bat;
            DispatcherTimer_Wizard = CreateTimer_WizardEnemy();
            _gameDashboard.DispatcherTimer_Wizard = DispatcherTimer_Wizard;
            _gameDashboard.DispatcherTimer_ShootingArrow = CreateTimer_ShootingArrow();
            _gameDashboard.DispatcherTimer_ControlShootingWeaponsVisibilityState = CreateTimer_ControlShootingWeaponsVisibilityState();
            _gameDashboard.DispatcherTimer_EnemyFireball = CreateTimer_EnemyFireball();
        }
        #endregion
        
        #region Register Images
        private void ImagesRegistration()
        {
            _gameDashboard.MainWindowImage = Form1DungeonImage;
            _gameDashboard.HeroPlayerImage = HeroImage;

            _gameDashboard.BluePotionImage = BluePotionImage;
            _gameDashboard.RedPotionImage = RedPotionImage;

            _gameDashboard.SwordWeaponImage = SwordWeaponImage;
            _gameDashboard.MaceWeaponImage = MaceWeaponImage;
            _gameDashboard.BowAndArrowWeaponImage = BowWeaponImage;

            _gameDashboard.BatEnemyImage = BatImage;
            _gameDashboard.WizardEnemyImage = WizardImage;

            AllEnemiesImages = new List<Image>() { BatImage, WizardImage, };
            _gameDashboard.AllWeaponImages = new List<Image>() { MaceWeaponImage, BowWeaponImage, SwordWeaponImage };
            _gameDashboard.AllPotionImages = new List<Image>() { BluePotionImage, RedPotionImage };

            _gameDashboard.AllSwordSwingImages = new List<Image>() { SwordSwingLeft, SwordSwingAngleLeftUp, SwordSwingUp, SwordSwingAngleRightUp, SwordSwingRight };
            _gameDashboard.AllMaceSwingImages = new List<Image>() { MaceImageSwingLeft, MaceImageSwingAngleLeftUp, MaceImageSwingUp, MaceImageSwingAngleRightUp, MaceImageSwingRight };
            
            _gameDashboard.AllArrowsShootingLeft = new List<Image>() { ArrowShootLeft0, ArrowShootLeft1, ArrowShootLeft2, ArrowShootLeft3, ArrowShootLeft4, ArrowShootLeft5, ArrowShootLeft6, ArrowShootLeft7, ArrowShootLeft8, ArrowShootLeft9 };
            _gameDashboard.AllArrowsShootingRight = new List<Image>() { ArrowShootRight0, ArrowShootRight1, ArrowShootRight2, ArrowShootRight3, ArrowShootRight4, ArrowShootRight5, ArrowShootRight6, ArrowShootRight7, ArrowShootRight8, ArrowShootRight9 };
            _gameDashboard.AllFireballLeftImages = new List<Image>() { FireballImageLeft0, FireballImageLeft1, FireballImageLeft2, FireballImageLeft3, FireballImageLeft4, FireballImageLeft5 };
            _gameDashboard.AllFireballRightImages = new List<Image>() { FireballImageRight0, FireballImageRight1, FireballImageRight2, FireballImageRight3, FireballImageRight4, FireballImageRight5 };
        }
#endregion

        #region Dispatcher_Timers 
        
        private DispatcherTimer CreateTimer_HeroPlayer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick_HeroPlayer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            return dispatcherTimer;
        }

        private void DispatcherTimer_Tick_HeroPlayer(object sender, EventArgs e)
        {
            _gameDashboard.RegisterNotificationDispatcherTimer_HeroPlayer(NotificationTimerEnum.TimerON);
            var playerNotification = _gameDashboard.GetHeroPlayerLabelNotification();
            
            PlayerLabel.Content = $"Hero: Energy: {playerNotification.HealthPoints} - Total Strike Points: {playerNotification.TotalStrikePoints}";
            ProcessPlayerHealthNotification(playerNotification.HealthPoints);
            SetPlayerNextLevelOrExit();
        }
        
        private DispatcherTimer CreateTimer_BatEnemy()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick_BatEnemy;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            return dispatcherTimer;
        }

        private void DispatcherTimer_Tick_BatEnemy(object sender, EventArgs e)
        {
            _gameDashboard.RegisterNotificationDispatcherTimer_Bat(NotificationTimerEnum.TimerON);
            var batNotification = _gameDashboard.GetBatLabelNotification();
            BatLabel.Content = $"Bat: Energy: {batNotification.HealthPoints} - Total Strike Points: {batNotification.TotalStrikePoints}";

            if (batNotification.HealthPoints <= 0)
            {
                _gameDashboard.SetTimerOnOff(DispatcherTimer_Bat, false);
                _gameDashboard.SetSingleImageVisibilityAndLocation(BatImage, new System.Drawing.Point(200, 400), Visibility.Hidden);
            }
            SetPlayerNextLevelOrExit();
        }

        private DispatcherTimer CreateTimer_WizardEnemy()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick_WizardEnemy;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            return dispatcherTimer;
        }

        private void DispatcherTimer_Tick_WizardEnemy(object sender, EventArgs e)
        {
            _gameDashboard.RegisterNotificationDispatcherTimer_Wizard(NotificationTimerEnum.TimerON);
            var wizardNotification = _gameDashboard.GetWizardLabelNotification();
            WizardLabel.Content = $"Wizard: Energy: {wizardNotification.HealthPoints} - Total Strike Points: {wizardNotification.TotalStrikePoints}";

            if (wizardNotification.HealthPoints <= 0)
            {
                _gameDashboard.SetTimerOnOff(DispatcherTimer_Wizard, false);
                _gameDashboard.SetSingleImageVisibilityAndLocation(WizardImage, new System.Drawing.Point(600, 400), Visibility.Hidden);
            }
            SetPlayerNextLevelOrExit();
        }
        
        private DispatcherTimer CreateTimer_ShootingArrow()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick_ShootingArrow;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            return dispatcherTimer;
        }

        private void DispatcherTimer_Tick_ShootingArrow(object sender, EventArgs e)
        {
            _gameDashboard.RegisterNotificationDispatcherTimer_Arrow(NotificationTimerEnum.TimerON);
        }

        private DispatcherTimer CreateTimer_ControlShootingWeaponsVisibilityState()
        {
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_ControlShootingWeaponsVisibilityState;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            return dispatcherTimer;
        }

        int arrowLeftInitialLocationX = 565, arrowLeftInitialLocationY = 550;
        int enemyFireballInitialLocationX = 590, enemyFireballInitialLocationY = 570;
        int leftLimit = 0, rightLimit = 1250;
        private void DispatcherTimer_ControlShootingWeaponsVisibilityState(object sender, EventArgs e)
        {
            SetShootingWeaponVisibilityState(_gameDashboard.AllArrowsShootingLeft, Visibility.Visible, Visibility.Hidden, arrowLeftInitialLocationX, arrowLeftInitialLocationY);
            SetShootingWeaponVisibilityState(_gameDashboard.AllArrowsShootingRight, Visibility.Visible, Visibility.Hidden, arrowLeftInitialLocationX, arrowLeftInitialLocationY);
            SetShootingWeaponVisibilityState(_gameDashboard.AllFireballLeftImages, Visibility.Visible, Visibility.Hidden, enemyFireballInitialLocationX, enemyFireballInitialLocationY);
            SetShootingWeaponVisibilityState(_gameDashboard.AllFireballRightImages, Visibility.Visible, Visibility.Hidden, enemyFireballInitialLocationX, enemyFireballInitialLocationY);
        }

        private void SetShootingWeaponVisibilityState(List<Image> weaponImages,Visibility initialVisibility, Visibility endingVisibility, int finalLocationX, int finalLocationY)
        {
            foreach (var weapon in weaponImages)
            {
                if (weapon.Visibility == initialVisibility)
                {
                    var positionWeapon = Canvas.GetLeft(weapon);
                    if (positionWeapon >= rightLimit || positionWeapon <= leftLimit)
                    {
                        Canvas.SetLeft(weapon, finalLocationX);
                        Canvas.SetTop(weapon, finalLocationY);
                        weapon.Visibility = endingVisibility;
                    }
                }
            }
        } 

        private DispatcherTimer CreateTimer_EnemyFireball()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick_EnemyFireball;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            return dispatcherTimer;
        }
        
        private void DispatcherTimer_Tick_EnemyFireball(object sender, EventArgs e)
        {
            _gameDashboard.RegisterNotificationDispatcherTimer_EnemyFireball( NotificationTimerEnum.TimerON);
        }
        #endregion

        #region MessageBox Notifications
        private bool canDisplayMessageBox = true;
        private void ProcessPlayerHealthNotification(int healthPoints)
        {
            if (healthPoints <= 0)
            {
                if (canDisplayMessageBox)
                {
                    canDisplayMessageBox = false;
                    int levelStopGame = 0;
                    _gameDashboard.SetGameLevel_Template(levelStopGame);
                    MessageBoxResult dialog = MessageBox.Show("PLAYER LOST Do you want to play again?", "GAME OVER", MessageBoxButton.YesNo);
                    if (dialog == MessageBoxResult.Yes)
                    {
                        int indexInitialLevel = 1;
                        _gameDashboard.SetGameLevel_Template(indexInitialLevel);
                    }
                    if (dialog == MessageBoxResult.No)
                    {
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                    canOpenMessageBox = true;
                }
            }
        }

        private bool canOpenMessageBox = true;
        private void SetPlayerNextLevelOrExit()
        {
            var allEnemiespoints = _gameDashboard.GetAllEnemiesSummatoryHealthPoints();
            if (allEnemiespoints <= 0)
             {
                if (canOpenMessageBox)
                {
                    canOpenMessageBox = false;
                    var dialog = MessageBox.Show("You defeated all your enemies. Do you want to want to go to the next Level", "You Won!", MessageBoxButton.YesNo);
                    if (dialog == MessageBoxResult.No)
                    {
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                     
                    }
                    if (dialog == MessageBoxResult.Yes)
                    {
                        if (_gameDashboard.AreAllLevelsExecuted())
                        {
                            var finalDialog = MessageBox.Show("You Won all levels", "Winner!!", MessageBoxButton.OK);
                            if (finalDialog == MessageBoxResult.OK)
                            {
                                System.Diagnostics.Process.GetCurrentProcess().Kill();
                            }
                        }
                        else
                        {
                            var currentIndex = _gameDashboard.GetIndexGameLevel();
                            var allLevelsCounted = _gameDashboard.GetGameLevelsCounted();
                            var nextIndex = currentIndex + 1;
                            if (nextIndex <= allLevelsCounted)
                            {
                                _gameDashboard.SetGameLevel_Template(nextIndex);
                            }
                        }
                    }
                    canOpenMessageBox = true;
                }
            }
        }
        #endregion

        #region Keyboard_Keys
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _gameDashboard.Form1_KeyDown_Event(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            _gameDashboard.Form1_KeyUp_Event();

            SwordSwingLeft.Visibility = Visibility.Hidden;
            SwordSwingAngleLeftUp.Visibility = Visibility.Hidden;
            SwordSwingUp.Visibility = Visibility.Hidden;
            SwordSwingAngleRightUp.Visibility = Visibility.Hidden;
            SwordSwingRight.Visibility = Visibility.Hidden;

            MaceImageSwingLeft.Visibility = Visibility.Hidden;
            MaceImageSwingAngleLeftUp.Visibility = Visibility.Hidden;
            MaceImageSwingUp.Visibility = Visibility.Hidden;
            MaceImageSwingAngleRightUp.Visibility = Visibility.Hidden;
            MaceImageSwingRight.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
