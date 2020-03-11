using HeroQuest.Common.Enums;
using HeroQuest.Common.Interfaces;
using HeroQuest.Common.Models;
using System.Windows;
using System.Windows.Controls;

namespace HeroQuest.Common.Managers
{
    public class ImageManager : IImageManager
    {
        private Image _mainWindow;
        public void RegisterWPFMainWindow(Image mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public MovementDirectionEnum ImageIsBeyondBoundaries_Adaptor(ImageObjectShapeModel rectangleShape, Image image)
        {
            ImageObjectShapeModel HeroImageRectangle = GetImage_CanvasLocationAndDimensions(image);
           
            var result = ImageRectangleHitSideBoundaries(rectangleShape, HeroImageRectangle);

            return result;
        }

        public MovementDirectionEnum ImageRectangleHitSideBoundaries(ImageObjectShapeModel rectangleShape1, ImageObjectShapeModel rectangleShape2)
        {
            //Castle Limits 
            var objA_x = rectangleShape1.LocationX;        
            var objA_y = rectangleShape1.LocationY;       
            var objA_x1 = objA_x + rectangleShape1.Width;
            var objA_y1 = objA_y + rectangleShape1.Height;


            var objB_x = rectangleShape2.LocationX;
            var objB_y = rectangleShape2.LocationY;
            var objB_x1 = objB_x + rectangleShape2.Width;
            var objB_y1 = objB_y + rectangleShape2.Height;

            var limitHit = MovementDirectionEnum.MovementClear_NoBoundariesReached;

            //Image hitting RIGHT Limit
            if (
                objA_x < objB_x && objB_x < objB_x1 && objB_x1 >= objA_x1
                &&
                objA_y < objB_y && objB_y < objB_y1 && objB_y1 < objA_y1
                )
            {
                limitHit = MovementDirectionEnum.MovingRight;
            }

            //Image hitting TOP Limit
            if (
                objA_x < objB_x && objB_x < objB_x1 && objB_x1 < objA_x1
                &&
                objA_y >= objB_y && objB_y < objB_y1 && objB_y1 < objA_y1
                )
            {
                limitHit = MovementDirectionEnum.MovingUp;
            }

            //Image hitting LEFT Limit
            if (
                objA_x >= objB_x && objB_x < objB_x1 && objB_x1 < objA_x1
                &&
                objA_y < objB_y && objB_y < objB_y1 && objB_y1 < objA_y1
                )
            {
                limitHit = MovementDirectionEnum.MovingLeft;
            }

            //Image hitting BOTTOM Limit
            if (
                objA_x < objB_x && objB_x < objB_x1 && objB_x1 < objA_x1
                &&
                objA_y < objB_y && objB_y < objB_y1 && objB_y1 >= objA_y1
                )
            {
                limitHit = MovementDirectionEnum.MovingDown;

            }
            return limitHit;
        }

        public bool ImagesCollisionDetected_Adaptor(Image image1, Image image2)
        {
            ImageObjectShapeModel rectangle1 = GetImageScreenLocationAndDimensions(image1);
            ImageObjectShapeModel rectangle2 = GetImageScreenLocationAndDimensions(image2);
            var result = ImagesRectangleShapeCollisionDetected(rectangle1, rectangle2);

            return result;
        }
        

        public bool ImagesRectangleShapeCollisionDetected(ImageObjectShapeModel image1RectangleShape, ImageObjectShapeModel image2RectangleShape)
        {
            var objA_x = image1RectangleShape.LocationX;
            var objA_y = image1RectangleShape.LocationY;
            var objA_x1 = objA_x + image1RectangleShape.Width;
            var objA_y1 = objA_y - image1RectangleShape.Height;
            
            var objB_x = image2RectangleShape.LocationX;
            var objB_y = image2RectangleShape.LocationY;
            var objB_x1 = objB_x + image2RectangleShape.Width;
            var objB_y1 = objB_y - image2RectangleShape.Height;

            var ManualComparissonCase1 = false;
            var ManualComparissonCase2 = false;
            var ManualComparissonCase3 = false;
            var ManualComparissonCase4 = false;
            
            if (
                ((objA_x <= objB_x && objB_x <= objA_x1 && objA_x1 <= objB_x1) && (objB_y >= objA_y && objA_y >= objA_y1 && objA_y1 >= objB_y1))
                ||
                ((objB_x <= objA_x && objA_x <= objA_x1 && objA_x1 <= objB_x1) && (objA_y >= objB_y && objB_y >= objA_y1 && objA_y1 >= objB_y1))
                ||
                ((objB_x <= objA_x && objA_x <= objB_x1 && objB_x1 <= objA_x1) && (objB_y >= objA_y && objA_y >= objA_y1 && objA_y1 >= objB_y1))
                ||
                ((objB_x <= objA_x && objA_x <= objA_x1 && objA_x1 <= objB_x1) && (objB_y >= objA_y && objA_y >= objB_y1 && objB_y1 >= objA_y1))
              )
            {
                ManualComparissonCase1 = true;
            }

            if(
                ((objA_x <= objB_x && objB_x <= objA_x1 && objA_x1 <= objB_x1) && (objA_y >= objB_y && objB_y >= objB_y1 && objB_y1 >= objA_y1))
                ||
                ((objA_x <= objB_x && objB_x <= objB_x1 && objB_x1 <= objA_x1) && (objA_y >= objB_y && objB_y >= objA_y1 && objA_y1 >= objB_y1))
                ||
                ((objB_x <= objA_x && objA_x <= objB_x1 && objB_x1 <= objA_x1) && (objA_y >= objB_y && objB_y >= objB_y1 && objB_y1 >= objA_y1))
                || 
                ((objA_x <= objB_x && objB_x <= objB_x1 && objB_x1 <= objA_x1) && (objB_y >= objA_y && objA_y >= objB_y1 && objB_y1 >= objA_y1))
                
               )
            {
                ManualComparissonCase2 = true;
            }

            if (
                ((objB_x <= objA_x && objA_x <= objA_x1 && objA_x1 <= objB_x1) && (objB_y >= objA_y && objA_y >=objA_y1 && objA_y1 >= objB_y1))
                ||
                ((objA_x <= objB_x && objB_x <= objB_x1 && objB_x1 <= objA_x1) && (objA_y >= objB_y && objB_y >= objB_y1 && objB_y1 >= objA_y1))
                )
            {
                ManualComparissonCase3 = true;
            }

            if (
               ((objA_x <= objB_x && objB_x <= objA_x1 && objA_x1 <= objB_x1) && (objB_y >= objA_y && objA_y >= objB_y1 && objB_y1 >= objA_y1))
               ||
               ((objA_x <= objB_x && objB_x <= objA_x1 && objA_x1 <= objB_x1) && (objA_y >= objB_y && objB_y >= objA_y1 && objA_y1 >= objB_y1))
               ||
               ((objB_x <= objA_x && objA_x <= objB_x1 && objB_x1 <= objA_x1) && (objB_y >= objA_y && objA_y >= objB_y1 && objB_y1 >= objA_y1))
               ||
               ((objB_x <= objA_x && objA_x <= objB_x1 && objB_x1 <= objA_x1) && (objA_y >= objB_y && objB_y >= objA_y1 && objA_y1 >= objB_y1))
             )
            {
                ManualComparissonCase4 = true;
            }

            var rectangle1 = new Rect(image1RectangleShape.LocationX, image1RectangleShape.LocationY, image1RectangleShape.Width, image1RectangleShape.Height);
            var rectangle2 = new Rect(image2RectangleShape.LocationX, image2RectangleShape.LocationY, image2RectangleShape.Width, image2RectangleShape.Height);
            rectangle1.Intersect(rectangle2);
            var syntheticComparisson = false;
            if (rectangle1 != Rect.Empty)
            {
                syntheticComparisson = true;
            }
            if(
                ManualComparissonCase1 == true || ManualComparissonCase2 == true 
             || ManualComparissonCase3 == true || ManualComparissonCase4 == true
             ||
                syntheticComparisson == true
              )
            {
                return true;
            }
            return false;
        }

        public Point GetImageLocation(Image referenceImage)
        {
            var positionMainWindow = Application.Current.MainWindow.PointToScreen(new System.Windows.Point(0, 0));
            var referenceImageAbsolutePos = referenceImage.PointToScreen(new System.Windows.Point(0, 0));
            referenceImageAbsolutePos = new Point(referenceImageAbsolutePos.X - positionMainWindow.X, referenceImageAbsolutePos.Y - positionMainWindow.Y);
            return referenceImageAbsolutePos;
        }

        public System.Drawing.Point SetNewLocationImage(Image image, System.Drawing.Point newPosition)
        {
            var imageCurrentLeftLocation = Canvas.GetLeft(image);
            var newLeftPosition = imageCurrentLeftLocation + newPosition.X;
            Canvas.SetLeft(image, newLeftPosition);
            var imageCurrentTopLocation = Canvas.GetTop(image);
            var newTopPosition = imageCurrentTopLocation + newPosition.Y;
            Canvas.SetTop(image, newTopPosition);
            return new System.Drawing.Point((int)newLeftPosition,(int)newTopPosition);
        }

        public void SetNewLocationForParentAndChildImage(Image parentImage, System.Drawing.Point newPositiontAdjustWeaponImage, System.Windows.Controls.Image childImage)
        {
            var positionMainWindow = Application.Current.MainWindow.PointToScreen(new System.Windows.Point(0, 0));
            var parentImageAbsolutePos = parentImage.PointToScreen(new System.Windows.Point(0, 0));
            parentImageAbsolutePos = new System.Windows.Point(parentImageAbsolutePos.X - positionMainWindow.X, parentImageAbsolutePos.Y - positionMainWindow.Y);

            System.Windows.Point parentImageLocationFromWindow = parentImage.TranslatePoint(new System.Windows.Point(0, 0),_mainWindow);
            
            var newChildImageLocationX = (parentImageLocationFromWindow.X == parentImageAbsolutePos.X)? parentImageLocationFromWindow.X - newPositiontAdjustWeaponImage.X : 0;
            var newChildImageLocationY = (parentImageLocationFromWindow.Y == parentImageAbsolutePos.Y)? parentImageLocationFromWindow.Y - newPositiontAdjustWeaponImage.Y : 0;
           
            Canvas.SetLeft(childImage, newChildImageLocationX);
            Canvas.SetTop(childImage, newChildImageLocationY);
        }


        public System.Drawing.Point ModifyLocationPointValues(System.Drawing.Point locationPoint, int newValue)
        {
            if (locationPoint.X > 0) { locationPoint.X = locationPoint.X + newValue; }
            if (locationPoint.X < 0) { locationPoint.X = locationPoint.X - newValue; }
            if (locationPoint.Y > 0) { locationPoint.Y = locationPoint.Y + newValue; }
            if (locationPoint.Y < 0) { locationPoint.Y = locationPoint.Y - newValue; }

            return locationPoint;
        }

        #region  private Methods
        private ImageObjectShapeModel GetImageScreenLocationAndDimensions(Image image)
        {
            var imageLocation = image.PointToScreen(new Point());
            return new ImageObjectShapeModel()
            {
                LocationX = (int)imageLocation.X,
                LocationY = (int)imageLocation.Y,
                Height = (int)image.Height,
                Width = (int)image.Width
            };
        }

        private ImageObjectShapeModel GetUIRectangleScreenLocationAndDimensions(System.Windows.Shapes.Rectangle uiRectangle)
        {
            var imageLocation = uiRectangle.PointToScreen(new Point());
            return new ImageObjectShapeModel()
            {
                LocationX = (int)imageLocation.X,
                LocationY = (int)imageLocation.Y,
                Height = (int)uiRectangle.Height,
                Width = (int)uiRectangle.Width
            };
        }

        private ImageObjectShapeModel GetImage_CanvasLocationAndDimensions(Image image)
        {
            var rectTop = Canvas.GetTop(image);
            var rectLeft = Canvas.GetLeft(image);

            return new ImageObjectShapeModel()
            {
                LocationX = (int)rectLeft,
                LocationY = (int)rectTop,
                Height = (int)image.Height,
                Width = (int)image.Width
            };
        }
        
        private void TheRectangleModel()
        {  /*
           Y
           |
           |      a(X , Y)           d(X+WIDTH, Y) 
           |         ______________
           |        |              |
           |        |              |
           |        |              |
           |        |              |
           |        |______________|
           |     b(X , Y-HEIGHT)     c(X+WIDTH, Y-HEIGHT)
       ----|--------|------------------------- X
         0 |
           |      Coordinates
           |      a = ( x,y )
           |      b = (x, y - height)
                  c = (x + width, y - height)
                  d = (x + width, y)
            */
        }
        #endregion
    }
}
