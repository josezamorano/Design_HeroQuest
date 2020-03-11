using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;

namespace HeroQuest.Library.WeaponsFactory
{
    public class BowAndArrow : Weapon, IBowAndArrow
    {
        private List<System.Windows.Controls.Image> AllImagesShootingArrowLeft = new List<System.Windows.Controls.Image>();
        private List<System.Windows.Controls.Image> AllImagesShootingArrowRight = new List<System.Windows.Controls.Image>();
        private int _arrowShootingInitialPotitionX;
        private int _arrowShootingInitialPotitionY;
        private static int _speedOfDisplacementArrow = 10;
        private int speedDisplacementToLeftX = _speedOfDisplacementArrow * -1;
        private int speedDisplacementToRightX = _speedOfDisplacementArrow * 1;
        private Key _storedArrowKeyDirection;
        private System.Windows.Controls.Image _playerImage;
        IWeaponHelper _weaponHelper;
        public BowAndArrow(IImageManager imageManager, IStrikeStrategyInvoker strikeStrategyInvoker,IWeaponHelper weaponHelper) : base(imageManager, strikeStrategyInvoker)
        {
            _weaponHelper = weaponHelper;

            WeaponName = WeaponsListEnum.Bow;
            DisplaceWeaponToHeroHandX = 30;
            DisplaceWeaponToHeroHandY = 10;
            DamagePower = 7;
            _arrowShootingInitialPotitionX = 10;
            _arrowShootingInitialPotitionY = 10;
           
        }

        public override bool Strike(IPlayer player)
        {
            _player = player;
            _shooter = player;
            _playerImage = player.ShooterImage;
            _allTargets = player.GetAllEnemies();
            _storedArrowKeyDirection = player.ShootingDirection_Player ;
            AllImagesShootingArrowLeft = player.AllArrowShootingLeftImages;
            AllImagesShootingArrowRight = player.AllArrowShootingRightImages;

            IsWeaponReadyForStrike = true;
            var initialLocationPoint = new Point(_arrowShootingInitialPotitionX, _arrowShootingInitialPotitionY);

            switch (_storedArrowKeyDirection)
            {
                case Key.Left:
                    var shootingLeftInitialLocationSuccess =_weaponHelper.SetInitialLocation_For_ShootingWeapon(_playerImage, initialLocationPoint, AllImagesShootingArrowLeft);
                    return shootingLeftInitialLocationSuccess;

                case Key.Right:
                    var shootingRightInitialLocationSuccess = _weaponHelper.SetInitialLocation_For_ShootingWeapon(_playerImage, initialLocationPoint, AllImagesShootingArrowRight);
                    return shootingRightInitialLocationSuccess;
            }
            return false;
        }

        public override void SetNotificationTimer(NotificationTimerEnum notificationTimer)
        {
            if (IsWeaponReadyForStrike && notificationTimer == NotificationTimerEnum.TimerON)
            {
                var newPointToLeft = new Point(speedDisplacementToLeftX, 0);
                _weaponHelper.Process_WeaponStraightShootingAction(_shooter,DamagePower, _allTargets, AllImagesShootingArrowLeft, newPointToLeft);
                var newPointToRight = new Point(speedDisplacementToRightX, 0);
                _weaponHelper.Process_WeaponStraightShootingAction(_shooter, DamagePower, _allTargets, AllImagesShootingArrowRight, newPointToRight);
            }
        }
    }
}
