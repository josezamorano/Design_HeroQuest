using HeroQuest.Common.Interfaces;
using HeroQuest.Library.Enums;
using HeroQuest.Library.Interfaces;

namespace HeroQuest.Library.WeaponStrikeStrategy
{ 
    public class SwingStrikeStrategy : ISwingStrikeStrategy
    {
        IImageManager _imageManager;
        public SwingStrikeStrategy(IImageManager imageManager)
        {
            _imageManager = imageManager;
        }

        public StrikeStrategiesEnum StrikeStrategy { get { return StrikeStrategiesEnum.Swing; } set { } }
        
        public System.Windows.Controls.Image SwingStrike(System.Windows.Controls.Image referenceImage, System.Drawing.Point locationPoint , System.Windows.Controls.Image orbitingImage)
        {
            _imageManager.SetNewLocationForParentAndChildImage(referenceImage, locationPoint, orbitingImage);
            return orbitingImage;
        }
    }
}
