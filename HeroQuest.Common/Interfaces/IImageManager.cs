using HeroQuest.Common.Enums;
using HeroQuest.Common.Models;
using System.Windows.Controls;

namespace HeroQuest.Common.Interfaces
{
    public interface IImageManager
    {
        void RegisterWPFMainWindow(Image mainWindow);
        MovementDirectionEnum ImageIsBeyondBoundaries_Adaptor(ImageObjectShapeModel rectangleShape, Image image);
        MovementDirectionEnum ImageRectangleHitSideBoundaries(ImageObjectShapeModel rectangleShape1, ImageObjectShapeModel rectangleShape2);
        bool ImagesCollisionDetected_Adaptor(Image image1, Image image2);
        bool ImagesRectangleShapeCollisionDetected(ImageObjectShapeModel image1RectangleShape, ImageObjectShapeModel image2RectangleShape);
        System.Windows.Point GetImageLocation(Image referenceImage);
        System.Drawing.Point SetNewLocationImage(Image image, System.Drawing.Point newPosition);
        void SetNewLocationForParentAndChildImage(Image parentImage, System.Drawing.Point newPositionParentImage, Image childImage);
        System.Drawing.Point ModifyLocationPointValues(System.Drawing.Point locationPoint, int newValue);
    }
}
