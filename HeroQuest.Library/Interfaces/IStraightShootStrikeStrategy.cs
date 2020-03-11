using HeroQuest.Library.Enums;
using System.Windows.Controls;

namespace HeroQuest.Library.Interfaces
{
    public interface IStraightShootStrikeStrategy
    {
        StrikeStrategiesEnum StrikeStrategy { get; set; }
        Image ShootStraightStrike(Image shootingImage, System.Drawing.Point locationPoint);
    }
}
