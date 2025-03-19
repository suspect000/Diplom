using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dickplom1.Class
{
    public class Musor
    {
        public static void HideElement(UIElement element)
        {
            element.Visibility = Visibility.Collapsed;
        }
        public static void ShowElement(UIElement element)
        {
            element.Visibility = Visibility.Visible;
        }
    }
}
