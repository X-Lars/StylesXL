using System;
using System.Windows;
using System.Windows.Media;

namespace StylesXL.Extensions
{
    public enum FadeDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public static class BrushExtensions
    {
        public static Brush Shade(this Brush instance, double factor = 0.5)
        {
            SolidColorBrush brush = (SolidColorBrush)(instance);

            factor = Math.Min(factor, 1);
            factor = Math.Max(factor, 0);

            Color color = Color.FromArgb(brush.Color.A,
                                  (byte)(brush.Color.R * (1 - factor)),
                                  (byte)(brush.Color.G * (1 - factor)),
                                  (byte)(brush.Color.B * (1 - factor)));

            return new SolidColorBrush(color);
        }

        public static Brush Tint(this Brush instance, double factor = 0.5)
        {
            SolidColorBrush brush = (SolidColorBrush)instance;

            factor = Math.Min(factor, 1);
            factor = Math.Max(factor, 0);

            Color color = Color.FromArgb(brush.Color.A,
                                  (byte)(brush.Color.R + (255 - brush.Color.R) * factor),
                                  (byte)(brush.Color.G + (255 - brush.Color.G) * factor),
                                  (byte)(brush.Color.B + (255 - brush.Color.B) * factor));

            return new SolidColorBrush(color);
        }

        public static Brush Alpha(this Brush instance, double factor = 0.5)
        {
            SolidColorBrush brush = (SolidColorBrush)instance;

            factor = Math.Min(factor, 1);
            factor = Math.Max(factor, 0);

            Color color = Color.FromArgb((byte)(factor * 255), brush.Color.R, brush.Color.G, brush.Color.B);
            
            return new SolidColorBrush(color);
        }

        public static double GetBrightness(this Brush instance)
        {
            SolidColorBrush brush = (SolidColorBrush)instance;

            return brush.Color.Brightness();
        }

        public static Brush Fade(this Brush instance, FadeDirection fade)
        {

            SolidColorBrush inputBrush = instance as SolidColorBrush;

            if (inputBrush == null)
                return instance;

            LinearGradientBrush brush = new LinearGradientBrush();

            GradientStop transparent  = new GradientStop() { Color = Colors.Transparent, Offset = 0 };
            GradientStop opaque       = new GradientStop() { Color = inputBrush.Color, Offset = 1 };

            brush.GradientStops.Add(transparent);
            brush.GradientStops.Add(opaque);

            switch (fade)
            {
                case FadeDirection.Left:
                    brush.StartPoint = new Point(0, 0.5);
                    brush.EndPoint   = new Point(1, 0.5);
                    break;
                case FadeDirection.Right:
                    brush.StartPoint = new Point(1, 0.5);
                    brush.EndPoint   = new Point(0, 0.5);
                    break;
                case FadeDirection.Up:
                    brush.StartPoint = new Point(0.5, 0);
                    brush.EndPoint   = new Point(0.5, 1);
                    break;
                case FadeDirection.Down:
                    brush.StartPoint = new Point(0.5, 1);
                    brush.EndPoint   = new Point(0.5, 0);
                    break;
            }

            brush.Freeze();

            return brush;
        }
    }
}
