using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ResizeAdorner
{

    public class SimpleDraggableAdorner : Adorner
    {
        private Thumb _thumb;
        private FrameworkElement _adornedElement;
        public SimpleDraggableAdorner(FrameworkElement adornedElement) : base(adornedElement)
        {
            this._adornedElement = adornedElement;
            this._thumb = new Thumb
            {
                Width = adornedElement.Width,
                Height = adornedElement.Height,
                BorderBrush = new SolidColorBrush(Colors.Yellow),
                Opacity = 0.3,
                Cursor = Cursors.SizeAll
            };
            this._thumb.DragDelta += _thumb_DragDelta;

            var visualChildren = new VisualCollection(this);
            visualChildren.Add(_thumb);
        }

        private void _thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double dX = e.HorizontalChange;
            double dY = e.VerticalChange;

            Point p = GetLocation();
            p.X += dX;
            p.Y += dY;

            SetLocation(p);
        }

        protected override Visual GetVisualChild(int index) { return _thumb; }
        protected override int VisualChildrenCount { get { return 1; } }
        protected override Size ArrangeOverride(Size finalSize)
        {
            _thumb.Arrange(new Rect(new Point(0, 0), new Size(_adornedElement.Width, _adornedElement.Height)));
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(new Size(_adornedElement.Width, _adornedElement.Height));

            // Some arbitrary drawing implements.
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Yellow);
            renderBrush.Opacity = 0.2;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.YellowGreen), 1.5);
            double renderRadius = 5.0;

            // Draw a circle at each corner.
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
        }

        private Point GetLocation()
        {
            Point p = new Point(Canvas.GetLeft(_adornedElement), Canvas.GetTop(_adornedElement));

            if (p.X.Equals(Double.NaN))
            {
                p.X = 11.11;
            }
            if (p.Y.Equals(Double.NaN))
            {
                p.Y = 11.11;
            }
            return p;
        }

        private void SetLocation(Point point)
        {
            if (!(_adornedElement.Parent is Canvas))
                return;
            Canvas.SetLeft(_adornedElement, point.X);
            Canvas.SetTop(_adornedElement, point.Y);
            this.ArrangeOverride(new Size(_adornedElement.Width, _adornedElement.Height));
        }

    }
}

