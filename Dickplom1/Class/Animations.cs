using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dickplom1.Class
{
    public class Animations
    {
        public static void WidthAnimation(UIElement element, double from, double to)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            element.BeginAnimation(FrameworkElement.WidthProperty, animation);
        }
    }
}
