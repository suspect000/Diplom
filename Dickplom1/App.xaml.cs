using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Dickplom1.Properties;

namespace Dickplom1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Загружаем сохранённую тему при запуске
            ApplyTheme(Settings.Default.AppTheme);
        }

        public static void ApplyTheme(string theme)
        {
            ResourceDictionary themeDict = new ResourceDictionary();

            if (theme == "Dark")
                themeDict.Source = new Uri("Themes/Dark.xaml", UriKind.Relative);
            else
                themeDict.Source = new Uri("Themes/Light.xaml", UriKind.Relative);

            // Очищаем старую тему и добавляем новую
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(themeDict);
        }

        public static void SaveTheme(string theme)
        {
            // Сохраняем выбор пользователя
            Settings.Default.AppTheme = theme;
            Settings.Default.Save();
        }
    }
}
