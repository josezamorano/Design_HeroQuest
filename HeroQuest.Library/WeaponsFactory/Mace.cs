using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using HeroQuest.Library.Models;
using System.Collections.Generic;
using System.Windows;

namespace HeroQuest.Library.WeaponsFactory
{
    public class Mace : Weapon, IMace 
    {
        IWeaponHelper _weaponHelper;
        public Mace(IImageManager imageManager, IStrikeStrategyInvoker strikeStrategyInvoker, IWeaponHelper weaponHelper):base( imageManager, strikeStrategyInvoker)
        {
            _strikeStrategyInvoker = strikeStrategyInvoker;
            _weaponHelper = weaponHelper;

            DisplaceWeaponToHeroHandX = -2;
            DisplaceWeaponToHeroHandY = 5;
            WeaponName = WeaponsListEnum.Mace;
            DamagePower = 5;
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
            var SwingMaceStrategy = (ISwingStrikeStrategy)selectedStrategy.StrikeStrategyType;
            List<SwingStrikeModel> allSwingMaceStrikes = new List<SwingStrikeModel>();
            if(allSwingMaceStrikes.Count == 0)
            {
                var pivotLocation30Degrees = new System.Drawing.Point(90, 5);
                var pivotLocation75Degrees = new System.Drawing.Point(75, 70);
                var pivotLocation90Degrees = new System.Drawing.Point(-5, 100);
                var pivotLocation120Degrees = new System.Drawing.Point(-30, 80);
                var pivotLocation60Degrees = new System.Drawing.Point(-40, -5);

                var SwingStrike30Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation30Degrees, SwingImage = shooter.AllMaceSwingImages[0] };
                var SwingStrike75Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation75Degrees, SwingImage = shooter.AllMaceSwingImages[1] };
                var SwingStrike90Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation90Degrees, SwingImage = shooter.AllMaceSwingImages[2] };
                var SwingStrike120Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation120Degrees, SwingImage = shooter.AllMaceSwingImages[3] };
                var SwingStrike160Degrees = new SwingStrikeModel() { SwingPointImageLocation = pivotLocation60Degrees, SwingImage = shooter.AllMaceSwingImages[4] };

                allSwingMaceStrikes = new List<SwingStrikeModel>() { SwingStrike30Degrees, SwingStrike75Degrees, SwingStrike90Degrees, SwingStrike120Degrees, SwingStrike160Degrees };
            }

            if (allSwingMaceStrikes.Count > 0)
            {
                foreach(var maceSwingStrike in allSwingMaceStrikes)
                {
                    maceSwingStrike.SwingImage.Visibility = Visibility.Visible;
                    SwingMaceStrategy.SwingStrike(shooter.ShooterImage, maceSwingStrike.SwingPointImageLocation, maceSwingStrike.SwingImage);

                    _weaponHelper.WeaponStrikeTarget(_shooter,DamagePower, maceSwingStrike.SwingImage, _allTargets);
                }
            }
            return true;
        }
    }
}
