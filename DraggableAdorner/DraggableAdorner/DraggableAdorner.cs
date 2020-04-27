using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace ResizeAdorner
{
    public class SelectAdorner : Adorner
    {
    
        public SelectAdorner(UIElement adornedElement) : base(adornedElement)
        {
     
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);

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

    }

    public static class DraggableExtend
    {

        public static void Draggable(this UIElement uIElement)
        {
            var ador = new SelectAdorner(uIElement);
            var adLayer = AdornerLayer.GetAdornerLayer(uIElement);
            adLayer.Add(ador);

        }


        public static void SimpleDraggable(this FrameworkElement uIElement)
        {
            var ador = new SimpleDraggableAdorner(uIElement);
            var adLayer = AdornerLayer.GetAdornerLayer(uIElement);
            adLayer.Add(ador);

        }


    }
}
