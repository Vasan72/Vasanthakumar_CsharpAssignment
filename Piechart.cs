
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using SkiaSharp;

public class PieChart
{
    public static Task GenerateAsync(List<Employee> employees)
    {
        if (employees == null || employees.Count == 0)
        {
            throw new Exception("No employee data available for the pie chart.");
        }

        const int width = 1400;
        const int height = 1000;

        using SKBitmap bitmap = new SKBitmap(width, height);
        using SKCanvas canvas = new SKCanvas(bitmap);

        canvas.Clear(SKColors.White);

        using SKPaint titlePaint = new SKPaint
        {
            Color = new SKColor(30, 45, 70),
            IsAntialias = true
        };

        using SKPaint subtitlePaint = new SKPaint
        {
            Color = SKColors.Gray,
            IsAntialias = true
        };

        using SKPaint slicePaint = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill
        };

        using SKPaint legendPaint = new SKPaint
        {
            Color = new SKColor(45, 55, 70),
            IsAntialias = true
        };

        using SKFont titleFont = new SKFont(SKTypeface.Default, 42);
        using SKFont subtitleFont = new SKFont(SKTypeface.Default, 24);
        using SKFont legendFont = new SKFont(SKTypeface.Default, 23);

        canvas.DrawText(
            "Employee Working Hours",
            70,
            75,
            SKTextAlign.Left,
            titleFont,
            titlePaint
        );

        canvas.DrawText(
            "Percentage of total time worked by each employee",
            70,
            120,
            SKTextAlign.Left,
            subtitleFont,
            subtitlePaint
        );

        double totalHours = employees.Sum(e => e.TotalHours);

        if (totalHours <= 0)
        {
            throw new Exception("Total working hours must be greater than zero.");
        }

        SKColor[] colors =
        {
            new SKColor(0, 92, 200),
            new SKColor(255, 122, 0),
            new SKColor(35, 160, 120),
            new SKColor(140, 90, 200),
            new SKColor(230, 75, 75),
            new SKColor(40, 160, 190),
            new SKColor(190, 150, 30),
            new SKColor(100, 120, 150),
            new SKColor(220, 100, 160),
            new SKColor(90, 160, 80)
        };

        SKRect chartRect = new SKRect(70, 190, 790, 910);

        float startAngle = -90;

        for (int i = 0; i < employees.Count; i++)
        {
            Employee employee = employees[i];

            float sweepAngle = (float)(
                employee.TotalHours / totalHours * 360
            );

            slicePaint.Color = colors[i % colors.Length];

            canvas.DrawArc(
                chartRect,
                startAngle,
                sweepAngle,
                true,
                slicePaint
            );

            startAngle += sweepAngle;
        }

        // Draw employee legend.
        float legendX = 850;
        float legendY = 220;
        float lineHeight = 48;

        for (int i = 0; i < employees.Count; i++)
        {
            Employee employee = employees[i];

            float percentage = (float)(
                employee.TotalHours / totalHours * 100
            );

            using SKPaint colorPaint = new SKPaint
            {
                Color = colors[i % colors.Length],
                IsAntialias = true
            };

            canvas.DrawCircle(
                legendX + 12,
                legendY - 8,
                11,
                colorPaint
            );

            string label =
                $"{employee.Name}: {percentage:F2}% ({employee.TotalHours:F2} hrs)";

            canvas.DrawText(
                label,
                legendX + 38,
                legendY,
                SKTextAlign.Left,
                legendFont,
                legendPaint
            );

            legendY += lineHeight;

            // Avoid drawing legend entries outside the image.
            if (legendY > height - 30)
            {
                break;
            }
        }

        using SKImage image = SKImage.FromBitmap(bitmap);
        using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);

        string outputPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "EmployeeWorkingHoursPieChart.png"
        );

        using FileStream stream = File.OpenWrite(outputPath);
        data.SaveTo(stream);

        return Task.CompletedTask;
    }
}
