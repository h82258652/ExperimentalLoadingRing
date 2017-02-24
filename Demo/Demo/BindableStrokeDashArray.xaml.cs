using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Demo
{
    public sealed partial class BindableStrokeDashArray : UserControl
    {
        public BindableStrokeDashArray()
        {
            this.InitializeComponent();
        }

        private Ellipse _ellipse;

        public BindableStrokeDashArray(Ellipse ellipse) : this()
        {
            _ellipse = ellipse;
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(BindableStrokeDashArray), new PropertyMetadata(default(double), OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindableStrokeDashArray obj = (BindableStrokeDashArray)d;
            var value = (double)e.NewValue;

            if (value >= 0)
            {
                obj._ellipse.StrokeDashArray = new DoubleCollection() { (value) / obj._ellipse.StrokeThickness, (2 * Math.PI * (50 - obj._ellipse.StrokeThickness / 2) - value) / obj._ellipse.StrokeThickness };
            }
            else
            {
                obj._ellipse.StrokeDashArray = new DoubleCollection()
                {
                    0,
                    (2 *Math.PI *(50 - obj._ellipse.StrokeThickness / 2) + value)/ obj._ellipse.StrokeThickness,
                    (0 - value)/obj._ellipse.StrokeThickness
                };
            }
        }

        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
    }
}