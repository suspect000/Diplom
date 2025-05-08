using LiveChartsCore;
using LiveChartsCore.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Windows.Media;

namespace Dickplom1.Class
{
    public class ViewModel
    {
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

    }
}
