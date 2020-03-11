using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System.Collections.Generic;
using System.Windows;

namespace HeroQuest.Library.WeaponsFactory
{
    public class Sword :Weapon, ISword
    {

        private List<System.Windows.Controls.Image> AllSwordSwingImages = new List<System.Windows.Controls.Image>();
        IWeaponHelper _weaponHelper;
        public Sword(IImageManager imageManager, IStrikeStrategyInvoker strikeStrategyInvoker, IWeaponHelper weaponHelper) :base(  imageManager, strikeStrategyInvoker)
        {
            _strikeStrategyInvoker = strikeStrategyInvoker;
            _weaponHelper = weaponHelper;
            DisplaceWeaponToHeroHandX = 45;
            DisplaceWeaponToHeroHandY = 5;
            WeaponName = WeaponsListEnum.Sword;
            DamagePower = 3;
        }

        public override bool Strike(IPlayer shooter)
        {
            //-------------------------------------
            //TO DO: implement swing Right to Left 
            //.....................................
            _player = shooter;
            _shooter = shooter;
            _allTargets = shooter.GetAllEnemies();
            var selectedStrategy = _strikeStrategyInvoker.GetStrikeStrategy(StrikeStrategiesEnum.Swing);
            var SwingSwordStrategy = (ISwingStrikeStrategy)selectedStrategy.StrikeStrategyType;

            var pivotLocation30Degrees = new System.Drawing.Point(100, -5);
            var pivotLocation75Degrees = new System.Drawing.Point(40, 40);
            var pivotLocation90Degrees = new System.Drawing.Point(15, 65);
            var pivotLocation120Degrees = new System.Drawing.Point(-35, 40);
            var pivotLocation160Degrees = new System.Drawing.Point(-40, -5);

            List<SwingStrikeModel> allSwingSwordStrikes = new List<SwingStrikeModel>();
            if(allSwingSwordStrikes.Count == 0)
            {
                var swingSrike30Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation30Degrees, SwingImage = shooter.AllSwordSwingImages[0] };
                var SwingStrike75Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation75Degrees, SwingImage = shooter.AllSwordSwingImages[1] };
                var SwingStrike90Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation90Degrees, SwingImage = shooter.AllSwordSwingImages[2] };
                var SwingStrike120Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation120Degrees, SwingImage = shooter.AllSwordSwingImages[3] };
                var SwingStrike160Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation160Degrees, SwingImage = shooter.AllSwordSwingImages[4] };

                allSwingSwordStrikes = new List<SwingStrikeModel>() { swingSrike30Degrees, SwingStrike75Degrees, SwingStrike90Degrees, SwingStrike120Degrees, SwingStrike160Degrees };
            }
            if (allSwingSwordStrikes.Count > 0)
            {
                foreach(var swordSwingStrike in allSwingSwordStrikes)
                {
                    swordSwingStrike.SwingImage.Visibility = Visibility.Visible;
                    SwingSwordStrategy.SwingStrike(shooter.ShooterImage, swordSwingStrike.SwingPointImageLocation,swordSwingStrike.SwingImage);

                    _weaponHelper.WeaponStrikeTarget(shooter, DamagePower,swordSwingStrike.SwingImage, _allTargets);
                }
                return true;
            }
            return false;
        }
    }
}
