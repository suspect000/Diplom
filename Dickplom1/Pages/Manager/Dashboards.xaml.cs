using Dickplom1.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dickplom1.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для Dashboards.xaml
    /// </summary>
    public partial class Dashboards : Page
    {
        public Dashboards()
        {
            InitializeComponent();
            List< Person> people = new List<Person>
            {
                new Person {Number = "№ 21321321", Name = "Сапожников В. И.", Status = "Выполняется"},
                new Person {Number = "№ 11321321", Name = "Аапожников В. И.", Status = "В процессе"},
                new Person {Number = "№ 61321321", Name = "Нажопников В. И.", Status = "Новое процессе"},
                new Person {Number = "№ 61321321", Name = "Нажопников В. И.", Status = "Новое процессе"},
                new Person {Number = "№ 61321321", Name = "Нажопников В. И.", Status = "Новое процессе"},
                new Person {Number = "№ 61321321", Name = "Нажопников В. И.", Status = "Новое процессе"},
                new Person {Number = "№ 61321321", Name = "Нажопников В. И.", Status = "Новое процессе"},

            };
            DataGridCustom.dg.ItemsSource = people;

        }
        public class Person
        {
            public string Number { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gridSalesAllChoseDate.Visibility == Visibility.Collapsed)
                Animations.MaximazedReports(ImgReportsArrowDown, gridSalesAllChoseDate);
            else
                Animations.MinimazedReports(ImgReportsArrowDown, gridSalesAllChoseDate);
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rbtnChoseDateSalesAllMounth.IsChecked = true;
            tbSalesAllChosenDate.Text = "за месяц";
        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            rbtnChoseDateSalesAllWeek.IsChecked = true;
            tbSalesAllChosenDate.Text = "за неделю";
        }

        private void TextBlock_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            rbtnChoseDateSalesAllYear.IsChecked = true;
            tbSalesAllChosenDate.Text = "за год";

        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            tbYear.Opacity = 0.8;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            tbYear.Opacity = 1;
        }

        private void TextBlock_MouseEnter_1(object sender, MouseEventArgs e)
        {
            tbMounth.Opacity = 0.8;
        }

        private void TextBlock_MouseLeave_1(object sender, MouseEventArgs e)
        {
            tbMounth.Opacity = 1;
        }

        private void tbWeek_MouseEnter(object sender, MouseEventArgs e)
        {
            tbWeek.Opacity = 0.8;
        }

        private void tbWeek_MouseLeave(object sender, MouseEventArgs e)
        {
            tbWeek.Opacity = 1;
        }

        private void gridStatusSalesSorting_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /*if (gridst.Visibility == Visibility.Collapsed)
                Animations.MaximazedReports(ImgReportsArrowDown, gridSalesAllChoseDate);
            else
                Animations.MinimazedReports(ImgReportsArrowDown, gridSalesAllChoseDate);*/
        }

        private void DataGridCustom_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Проверяем, что событие еще не обработано
            if (!e.Handled)
            {
                // Передаем событие прокрутки родительскому ScrollViewer
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };
                MainContentScroll.RaiseEvent(eventArg);
            }
        }
    }
}
