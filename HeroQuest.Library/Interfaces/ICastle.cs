using HeroQuest.Common.Models;
using System.Windows.Shapes;

namespace HeroQuest.Library.Interfaces
{
    public interface ICastle
    {
        Rectangle GetCastleInnerLimits();
        Rectangle GetCastleOuterLimits();
        ImageObjectShapeModel GetCanvasLocationAndDimensionsInnerLimits();
        ImageObjectShapeModel GetCanvasLocationAndDimensionsOuterLimits();
    }
}
