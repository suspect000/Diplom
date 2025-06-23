using Dickplom1.DataFolder;
using Dickplom1.Pages.Manager;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Dickplom1.Class
{
    public class Charts
    {
        public PieSeries<double> CompletedOrdersSeries { get; set; }

        public IEnumerable<ISeries> Series { get; set; }

        public Charts()
        {
            CompletedOrdersSeries = new PieSeries<double>
            {
                Values = new double[] { 20 },
                MaxRadialColumnWidth = 10,
                Fill = new SolidColorPaint(SKColor.Parse("687183")),
                CornerRadius = 30,
                IsHoverable = false,
                DataLabelsPaint = new SolidColorPaint(SKColor.Parse("687183")),
                DataLabelsSize = 25,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.ChartCenter
            };

            Series = new List<ISeries>
        {
            new PieSeries<double>
            {
                Values = new double[] { 100 },
                MaxRadialColumnWidth = 10,
                Fill = new SolidColorPaint(SKColors.White),
                IsHoverable = false,
                Stroke = null,
                Pushout = 0,
                DataLabelsPaint = null
            },
            CompletedOrdersSeries
        };

        }
        public void SetCompletedOrdersPercent(double percent)
        {
            // Устанавливаем новое значение
            CompletedOrdersSeries.Values = new double[] { percent };
        }
        public ISeries[] SeriesDynamicSales { get; set; } =
        {
            new LineSeries<double>
            {
                //Значения для графика динамики продаж присваиваются здесь
                Values = new double[] { 2, 1, 3, 5, 3, 4, 3 },
                Fill = new SolidColorPaint(SKColor.Empty),
                Stroke = new SolidColorPaint(SKColor.Parse("687183"))
                {
                    StrokeThickness = 5,
                },
                GeometryStroke = new SolidColorPaint(SKColor.Parse("687183")),
                GeometryFill = new SolidColorPaint(SKColor.Parse("687183")),
                GeometrySize = 10, // Размер кругов со значениями на графике
                MiniatureShapeSize = 5, // Размер кругов со значениями на тултипе
                DataLabelsMaxWidth = 40
            }
        };








        //Второй график (Динамика продаж)


        // Метод для подготовки данных о заказах для графика
        public Dictionary<DateTime, int> PrepareOrdersData(List<OrdersViewModel> orders, string period)
        {
            var ordersData = new Dictionary<DateTime, int>();
            var today = DateTime.Today;

            // Преобразуем строковые даты в DateTime и группируем по дням
            var dailyOrders = orders
                .Where(o => DateTime.TryParse(o.StartDate, out _))
                .GroupBy(o => DateTime.Parse(o.StartDate).Date)
                .ToDictionary(g => g.Key, g => g.Count());

            switch (period)
            {
                case "Неделя":
                    var monday = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                    for (int i = 0; i < 7; i++)
                    {
                        var date = monday.AddDays(i);
                        ordersData[date] = dailyOrders.ContainsKey(date) ? dailyOrders[date] : 0;
                    }
                    break;

                case "Месяц":
                    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        var date = new DateTime(today.Year, today.Month, day);
                        ordersData[date] = dailyOrders.ContainsKey(date) ? dailyOrders[date] : 0;
                    }
                    break;

                case "Год":
                    for (int month = 1; month <= 12; month++)
                    {
                        var firstDayOfMonth = new DateTime(today.Year, month, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                        int monthlyCount = dailyOrders
                            .Where(kv => kv.Key >= firstDayOfMonth && kv.Key <= lastDayOfMonth)
                            .Sum(kv => kv.Value);

                        ordersData[firstDayOfMonth] = monthlyCount;
                    }
                    break;
            }

            return ordersData;
        }



        public Axis[] XAxesDynamicSales { get; set; }
        public void UpdateDynamicSalesData(Dictionary<DateTime, int> ordersData, string selectedPeriod)
        {
            var today = DateTime.Today;
            var values = new List<double>();
            var labels = new List<string>();

            switch (selectedPeriod)
            {
                case "Неделя":
                    // Получаем понедельник текущей недели
                    var monday = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

                    // Для каждого дня недели проверяем, есть ли данные
                    for (int i = 0; i < 7; i++)
                    {
                        var currentDate = monday.AddDays(i);
                        var dateKey = ordersData.Keys.FirstOrDefault(d => d.Date == currentDate.Date);

                        if (dateKey != default(DateTime))
                        {
                            values.Add(ordersData[dateKey]);
                        }
                        else
                        {
                            values.Add(0);
                        }

                        labels.Add(currentDate.ToString("dd MMM"));
                    }
                    break;

                case "Месяц":
                    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

                    for (int day = 1; day <= daysInMonth; day++)
                    {
                        var currentDate = new DateTime(today.Year, today.Month, day);
                        var dateKey = ordersData.Keys.FirstOrDefault(d => d.Date == currentDate.Date);

                        if (dateKey != default(DateTime))
                        {
                            values.Add(ordersData[dateKey]);
                        }
                        else
                        {
                            values.Add(0);
                        }

                        labels.Add(day.ToString());
                    }
                    break;

                case "Год":
                    for (int month = 1; month <= 12; month++)
                    {
                        var monthStart = new DateTime(today.Year, month, 1);
                        var monthEnd = monthStart.AddMonths(1).AddDays(-1);

                        // Суммируем заказы за весь месяц
                        int monthOrders = ordersData
                            .Where(kv => kv.Key.Date >= monthStart.Date && kv.Key.Date <= monthEnd.Date)
                            .Sum(kv => kv.Value);

                        values.Add(monthOrders);
                        labels.Add(monthStart.ToString("MMM"));
                    }
                    break;
            }

            // Обновляем серии графика
            SeriesDynamicSales = new ISeries[]
            {
        new LineSeries<double>
        {
            Values = values.ToArray(),
            Fill = new SolidColorPaint(SKColor.Empty),
            Stroke = new SolidColorPaint(SKColor.Parse("687183"))
            {
                StrokeThickness = 5,
            },
            GeometryStroke = new SolidColorPaint(SKColor.Parse("687183")),
            GeometryFill = new SolidColorPaint(SKColor.Parse("687183")),
            GeometrySize = 10,
            MiniatureShapeSize = 5,
            DataLabelsMaxWidth = 40
        }
            };

            // Обновляем оси X
            XAxesDynamicSales = new[]
            {
        new Axis
        {
            Labels = labels.ToArray(),
            LabelsRotation = 0
        }
    };
        }

























        /*//Настройки графика для динамики продаж
        public ISeries[] SeriesDynamicSales { get; set; } =
        {
            new LineSeries<double>
            {
                //Значения для графика динамики продаж присваиваются здесь
                Values = new double[] { 2, 1, 3, 5, 3, 4, 3 },
                Fill = new SolidColorPaint(SKColor.Empty),
                Stroke = new SolidColorPaint(SKColor.Parse("687183"))
                {
                    StrokeThickness = 5,
                },
                GeometryStroke = new SolidColorPaint(SKColor.Parse("687183")),
                GeometryFill = new SolidColorPaint(SKColor.Parse("687183")),
                GeometrySize = 10, // Размер кругов со значениями на графике
                MiniatureShapeSize = 5, // Размер кругов со значениями на тултипе
                DataLabelsMaxWidth = 40
            }
        };
        public Axis[] XAxesDynamicSales { get; set; }

        public void UpdateXAxis(string selectedPeriod) // Метод который обновляет значения AxisX в графике
        {
            XAxesDynamicSales = new[]
            {
                new Axis
                {
                    Labels = GenerateXAxisLabels(selectedPeriod),
                    LabelsRotation = 0
                }
            };
        }

        //Метод для генерации генерации чисел и передачи их в AxisX исходя из того какой радиобатон выбра (неделя, месяц, год)
        public string[] GenerateXAxisLabels(string period)
        {
            var today = DateTime.Today;

            switch (period)
            {
                case "Неделя":
                    // понедельник текущей недели
                    var monday = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                    return Enumerable.Range(0, 7)
                        .Select(offset => monday.AddDays(offset).ToString("dd MMM"))
                        .ToArray();

                case "Месяц":
                    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
                    return Enumerable.Range(1, daysInMonth)
                        .Select(day => day.ToString())
                        .ToArray();

                case "Год":
                    return Enumerable.Range(1, 12)
                        .Select(month => new DateTime(today.Year, month, 1).ToString("MMM"))
                        .ToArray();

                default:
                    return Array.Empty<string>();
            }
        }*/
    }
}
