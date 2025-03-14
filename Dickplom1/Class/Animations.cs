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
        public static void WidthAnimation(UIElement element, double from, double to, double time)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            element.BeginAnimation(FrameworkElement.WidthProperty, animation);
        }

        public static void MovingAnimation(UIElement element, Thickness from, Thickness to, double time)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            element.BeginAnimation(FrameworkElement.MarginProperty, animation);
        }

        public static void HeightAnimation(UIElement element, double from, double to, double time, StackPanel stackPanel = null)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            if (stackPanel != null) // Очень классная структура
            {
                animation.Completed += (s, e) =>
                {
                    stackPanel.Visibility = Visibility.Collapsed;
                };
            }
            element.BeginAnimation(FrameworkElement.HeightProperty, animation);
        }

        public static void AnimateBorderBrush(UIElement element, Color fromColor, Color toColor, double time)
        {
            // Создаем новую кисть, чтобы избежать ошибки замороженного ресурса
            var brush = new SolidColorBrush(fromColor);

            if (element == null) return;

            if (element is TextBox textbox)
                textbox.BorderBrush = brush;

            if (element is Button button)
                button.BorderBrush = brush;

            if (element is ListBoxItem lIItem)
                lIItem.BorderBrush = brush;

            if (element is Border border)
                border.BorderBrush = brush;


            var animation = new ColorAnimation
            {
                From = fromColor,
                To = toColor,
                Duration = TimeSpan.FromSeconds(time),
                FillBehavior = FillBehavior.HoldEnd
            };

            // Запускаем анимацию
            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }
        public static void RotationAnimation(UIElement element, double from, double to, double time)
        {
            var rotateTransform = new RotateTransform(from);
            element.RenderTransform = rotateTransform;
            element.RenderTransformOrigin = new Point(0.5, 0.5);

            var animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
        }
        public static void OpacityAnimation(UIElement element, int from, int to, double time)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            element.BeginAnimation(FrameworkElement.OpacityProperty, animation);
        }
    }
}
