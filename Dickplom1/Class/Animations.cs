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

        //Методы для разворачивания/сворачивания топиков в навигации
        public static void MaximazedNavTopics(StackPanel stackPanelItems, Image imgArrow)
        {
            Animations.MovingAnimation(stackPanelItems, new Thickness(0, -30, 0, 0), new Thickness(0, 0, 0, 0), 0.3);
            Animations.RotationAnimation(imgArrow, 0, 180, 0.2);
            stackPanelItems.Visibility = Visibility.Visible;
            Animations.OpacityAnimation(stackPanelItems, 0, 1, 0.2);
            Animations.HeightAnimation(stackPanelItems, 0, 138, 0.2);
        }

        public static void MinimazedNavTopics(StackPanel stackPanelItems, Image imgArrow)
        {
            Animations.MovingAnimation(stackPanelItems, stackPanelItems.Margin, new Thickness(0, -30, 0, 0), 0.3);
            Animations.RotationAnimation(imgArrow, 180, 0, 0.2);
            Animations.OpacityAnimation(stackPanelItems, 1, 0, 0.2);
            Animations.HeightAnimation(stackPanelItems, 138, 0, 0.3, stackPanelItems);
        }
        //-----------------------------------------------------------------------------------

        //Методы для разворачивания/сворачивания отчетов в header
        public static void MaximazedReports(Image imgArrow, Grid gridReportsToMake)
        {
            Animations.RotationAnimation(imgArrow, 0, 180, 0.2);
            Animations.OpacityAnimation(gridReportsToMake, 0, 1, 0.15);
            gridReportsToMake.Visibility = Visibility.Visible;
        }
        public static void MinimazedReports(Image imgArrow, Grid gridReportsToMake)
        {
            Animations.RotationAnimation(imgArrow, 180, 0, 0.2);
                Animations.OpacityAnimation(gridReportsToMake, 1, 0, 0.15, gridReportsToMake);
        }       
        //-----------------------------------------------------------------------------------

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

        public static void AnimateBackgroundBrush(UIElement element, Color fromColor, Color toColor, double time)
        {
            // Создаем новую кисть, чтобы избежать ошибки замороженного ресурса
            var brush = new SolidColorBrush(fromColor);

            if (element == null) return;

            if (element is TextBox textbox)
                textbox.Background = brush;

            if (element is Button button)
                button.Background = brush;

            if (element is ListBoxItem lIItem)
                lIItem.Background = brush;

            if (element is Border border)
                border.Background = brush;


            var animation = new ColorAnimation
            {
                From = fromColor,
                To = toColor,
                Duration = TimeSpan.FromSeconds(time),
                FillBehavior = FillBehavior.HoldEnd
            };

            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
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
        public static void OpacityAnimation(UIElement element, double from, double to, double time, UIElement completedAnim = null)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(time),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            if (completedAnim != null) // Очень классная структура, добавляет подписку на завершение анимации
            {
                animation.Completed += (s, e) =>
                {
                    completedAnim.Visibility = Visibility.Collapsed;
                };
            }

            element.BeginAnimation(FrameworkElement.OpacityProperty, animation);
        }
    }
}
