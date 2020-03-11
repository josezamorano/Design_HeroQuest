using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HeroQuest.Library.EnemyWeaponsFactory
{
    class EnemyFireball : IEnemyFireball
    {
        public int DamagePower { get; set; }
        protected IEnemyShooter _enemy;
        public List<ITarget> _allTargets;
        private bool IsWeaponReadyToStrike = false;
        private Random random = new Random();
        private static int _speedOfDisplacementFireball = 20;
        private int speedDisplacementToLeftX = _speedOfDisplacementFireball * -1;
        private int speedDisplacementToRightX = _speedOfDisplacementFireball * 1;

        private List<System.Windows.Controls.Image> AllImagesShootingLeftWeapon = new List<System.Windows.Controls.Image>();
        private List<System.Windows.Controls.Image> AllImagesShootingRightWeapon = new List<System.Windows.Controls.Image>();
        IImageManager _imageManager;
        IStrikeStrategyInvoker _strikeStrategyInvoker;
        IWeaponHelper _weaponHelper;
        public EnemyFireball(IImageManager imageManager, IStrikeStrategyInvoker strikeStrategyInvoker, IWeaponHelper weaponHelper)
        {
            _imageManager = imageManager;
            _strikeStrategyInvoker = strikeStrategyInvoker;
            _weaponHelper = weaponHelper;
            DamagePower = 7;

            _allTargets = new List<ITarget>();
        }

        public bool Strike(IEnemy shooter)
        {
            IsWeaponReadyToStrike = true;
            _enemy = shooter;
            _allTargets.Add(shooter.Target);
            AllImagesShootingLeftWeapon = shooter.AllFireballLeftShootingImages;
            AllImagesShootingRightWeapon = shooter.AllFireballRightShootingImages;
            var initialLocationPoint = new Point(0, 0);

            //... here we have to make them left and righ weapons
            var shootingLeftInitialLocationSuccess = _weaponHelper.SetInitialLocation_For_ShootingWeapon(shooter.ShooterImage, initialLocationPoint, AllImagesShootingLeftWeapon);
            var shootingRightInitialLocationSuccess = _weaponHelper.SetInitialLocation_For_ShootingWeapon(shooter.ShooterImage, initialLocationPoint, AllImagesShootingRightWeapon);

            if(shootingLeftInitialLocationSuccess && shootingRightInitialLocationSuccess)
            {
                return true;
            }
            return false;
        }

        public void SetNotificationTimer(NotificationTimerEnum notificationTimer)
        {
            if (IsWeaponReadyToStrike && notificationTimer == NotificationTimerEnum.TimerON)
            {
                var newPointToLeft = new Point(speedDisplacementToLeftX, 0);
                Process_WeaponStraightShootingAction(AllImagesShootingLeftWeapon, newPointToLeft);
                var newPointToRight = new Point(speedDisplacementToRightX, 0);
                Process_WeaponStraightShootingAction(AllImagesShootingRightWeapon, newPointToRight);
            }
        }

        public virtual void Process_WeaponStraightShootingAction(List<System.Windows.Controls.Image> allShootingImages, System.Drawing.Point newPoint)
        {
            var selectedStrikeStrategy = _strikeStrategyInvoker.GetStrikeStrategy(Enums.StrikeStrategiesEnum.Shoot);
            var shootingStrategy = (IStraightShootStrikeStrategy)selectedStrikeStrategy.StrikeStrategyType;
            foreach (var arrowImage in allShootingImages)
            {
                if (arrowImage.Visibility == System.Windows.Visibility.Visible)
                {
                    shootingStrategy.ShootStraightStrike(arrowImage, newPoint);
                }
                WeaponStrikeEnemy(DamagePower, arrowImage, _allTargets);
            }
        }

        public virtual void WeaponStrikeEnemy(int weaponDamagingPoints, System.Windows.Controls.Image weaponImage, List<ITarget> allActiveTargets)
        {
            //re do this part to benefit the enemy
            foreach (var target in allActiveTargets)
            {
                if (target.GetHealthPoints() > 0)
                {
                    var weaponHitEnemy = _imageManager.ImagesCollisionDetected_Adaptor(weaponImage, target.TargetImage);
                    if (weaponHitEnemy)
                    {
                        target.ReduceHealth(weaponDamagingPoints);
                        _enemy.EnemyIncreasePointForWeaponStrikingTarget(weaponDamagingPoints);
                    }
                }
            }
        }
    }
}
