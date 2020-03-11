using HeroQuest.Library.Enums;
using System.Windows.Controls;

namespace HeroQuest.Library.Interfaces
{
    public interface ISwingStrikeStrategy
    {
        StrikeStrategiesEnum StrikeStrategy { get; set; }
        Image SwingStrike(Image referenceImage, System.Drawing.Point locationPoint, Image orbitingImage);
    }  
}
