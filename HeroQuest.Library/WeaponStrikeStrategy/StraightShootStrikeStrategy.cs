using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;
using System.Windows.Controls;

namespace HeroQuest.Library.WeaponStrikeStrategy
{
    public class StraightShootStrikeStrategy : IStraightShootStrikeStrategy
    {
        IImageManager _imageManager;
        public StraightShootStrikeStrategy(IImageManager imageManger)
        {
            _imageManager = imageManger;
        }

        public StrikeStrategiesEnum StrikeStrategy { get; set; }
        public Image ShootStraightStrike(Image shootingImage, System.Drawing.Point locationPoint)
        {
            _imageManager.SetNewLocationImage(shootingImage, locationPoint);
            return shootingImage;
        }
    }
}
