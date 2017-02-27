using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DemoWpf.Extensions;

namespace DemoWpf
{
    public partial class BindableEllipseStrokeDashArray
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(BindableEllipseStrokeDashArray), new PropertyMetadata(default(double), OnValueChanged));

        private readonly Ellipse _ellipse;

        public BindableEllipseStrokeDashArray(Ellipse ellipse)
        {
            if (ellipse == null)
            {
                throw new ArgumentNullException(nameof(ellipse));
            }

            InitializeComponent();
            _ellipse = ellipse;
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

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (BindableEllipseStrokeDashArray)d;

            obj.UpdateStrokeDashArray();
        }

        private void UpdateStrokeDashArray()
        {
            var value = Value;
            if (value > 0)
            {
                _ellipse.StrokeDashArray = new DoubleCollection()
                {
                    value,
                    _ellipse.GetCircumference() - value
                };
            }
            else if (value < 0)
            {
                _ellipse.StrokeDashArray = new DoubleCollection()
                {
                    0,
                    _ellipse.GetCircumference() + value,
                    0 - value
                };
            }
            else
            {
                _ellipse.StrokeDashArray = new DoubleCollection();
            }
        }
    }
}