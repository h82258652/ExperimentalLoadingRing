﻿using System;
using Windows.UI.Xaml.Shapes;

namespace Demo3
{
    public static class EllipseExtensions
    {
        public static double GetCircumference(this Ellipse ellipse)
        {
            if (ellipse == null)
            {
                throw new ArgumentNullException(nameof(ellipse));
            }

            var strokeThickness = ellipse.StrokeThickness;
            var width = ellipse.ActualWidth - strokeThickness;
            var height = ellipse.ActualHeight - strokeThickness;

            var a = Math.Max(width / 2, height / 2);
            var b = Math.Min(width / 2, height / 2);
            var circumference = 2 * Math.PI * b + 4 * (a - b);
            return circumference / strokeThickness;
        }
    }
}