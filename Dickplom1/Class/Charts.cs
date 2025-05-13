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
        public Charts() 
        {
            UpdateXAxis("Месяц");
        }
        //Настройки графика пирога для соотношения выполненных заказов
        public IEnumerable<ISeries> Series { get; set; } =
        new List<ISeries>
        {
            // Фоновая белая дуга
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

            // Значение (Value)
            new PieSeries<double>
            {
                Values = new double[] { 20 },
                MaxRadialColumnWidth = 10,
                Fill = new SolidColorPaint(SKColor.Parse("687183")),
                CornerRadius = 30,
                IsHoverable = false,
                DataLabelsPaint = new SolidColorPaint(SKColor.Parse("687183")),
                DataLabelsSize = 25,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.ChartCenter
            }
        }; 

        //Настройки графика для динамики продаж
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
        }
    }
}
