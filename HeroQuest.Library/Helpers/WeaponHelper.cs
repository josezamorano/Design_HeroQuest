using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Interfaces;
using System.Collections.Generic;

namespace HeroQuest.Library.Helpers
{
    public class WeaponHelper : IWeaponHelper
    {
        IImageManager _imageManager;
        IStrikeStrategyInvoker _strikeStrategyInvoker;
        public WeaponHelper(IImageManager imageManager, IStrikeStrategyInvoker strikeStrategyInvoker)
        {
            _imageManager = imageManager;
            _strikeStrategyInvoker = strikeStrategyInvoker;
        }

        public bool SetInitialLocation_For_ShootingWeapon(System.Windows.Controls.Image shooterImage, System.Drawing.Point initialLocationPoint, List<System.Windows.Controls.Image> weapon_AllShootingImages)
        {
            foreach (var weaponShootingImage in weapon_AllShootingImages)
            {
                if (weaponShootingImage.Visibility == System.Windows.Visibility.Hidden)
                {
                    weaponShootingImage.Visibility = System.Windows.Visibility.Visible;
                    _imageManager.SetNewLocationForParentAndChildImage(shooterImage, initialLocationPoint, weaponShootingImage);
                    return true;
                }
            }
            return false;
        }

        public void Process_WeaponStraightShootingAction(IShooter shooter,int weaponDamagePower, List<IEnemy> allEnemies, List<System.Windows.Controls.Image> allShootingArrowImages, System.Drawing.Point newPoint)
        {
            var selectedStrikeStrategy = _strikeStrategyInvoker.GetStrikeStrategy(Enums.StrikeStrategiesEnum.Shoot);
            var shootingStrategy = (IStraightShootStrikeStrategy)selectedStrikeStrategy.StrikeStrategyType;
            foreach (var arrowImage in allShootingArrowImages)
            {
                if (arrowImage.Visibility == System.Windows.Visibility.Visible)
                {
                    shootingStrategy.ShootStraightStrike(arrowImage, newPoint);
                }
                WeaponStrikeTarget(shooter, weaponDamagePower, arrowImage, allEnemies);
            }
        }

        public void WeaponStrikeTarget(IShooter shooter, int weaponDamagingPoints, System.Windows.Controls.Image weaponImage, List<IEnemy> allActiveEnemies)
        {
            foreach (var target in allActiveEnemies)
            {
                if (target.GetHealthPoints() > 0)
                {
                    var weaponHitEnemy = _imageManager.ImagesCollisionDetected_Adaptor(weaponImage, target.EnemyImage);
                    if (weaponHitEnemy)
                    {
                        target.ReduceHealth(weaponDamagingPoints);
                        //hero increases total strike points
                        shooter.IncreasePointForStrikingTarget(weaponDamagingPoints);
                    }
                }
            }
        }
    }
}
