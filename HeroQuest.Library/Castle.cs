using HeroQuest.Common.Models;
using HeroQuest.Library.Interfaces;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeroQuest.Library
{
    public class Castle : ICastle
    {
        Rectangle rectangle1 = new Rectangle();
        Rectangle rectangle2 = new Rectangle();
        private int _innerWidth = 995;
        private int _innerHeight = 338;
        private int _innerLocationX = 165;
        private int _innerLocationY = 103;

        private int _outerWidth = 1258;
        private int _outerHeight = 500;
        private int _outerLocationX = 30;
        private int _outerLocationY = 20;

        public Rectangle GetCastleInnerLimits()
        {
            rectangle1.Stroke = new SolidColorBrush(Colors.Red);
            rectangle1.Width = _innerWidth;
            rectangle1.Height = _innerHeight;
            Canvas.SetLeft(rectangle1, _innerLocationX);
            Canvas.SetTop(rectangle1, _innerLocationY);
            return rectangle1;
        }

        public Rectangle GetCastleOuterLimits()
        {
            rectangle2.Stroke = new SolidColorBrush(Colors.Blue);
            rectangle2.Width = _outerWidth;
            rectangle2.Height = _outerHeight;
            Canvas.SetLeft(rectangle2, _outerLocationX);
            Canvas.SetTop(rectangle2, _outerLocationY);
            
            return rectangle2;
        }

        public ImageObjectShapeModel GetCanvasLocationAndDimensionsInnerLimits()
        {
            ImageObjectShapeModel imageObjectShape = new ImageObjectShapeModel()
            {
                LocationX = -195,
                LocationY = -70,
                Width = 995,
                Height = 340,
            };
            return imageObjectShape;
        }

        public ImageObjectShapeModel GetCanvasLocationAndDimensionsOuterLimits()
        {
            ImageObjectShapeModel imageObjectShape = new ImageObjectShapeModel()
            {
                LocationX = 30,
                LocationY = 20,
                Width = 1260,
                Height = 510,
            };
            return imageObjectShape;
        }
    }
}
