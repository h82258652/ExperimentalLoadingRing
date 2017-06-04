using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Demo3
{
    public sealed partial class EllipseAssist
    {
        public static readonly DependencyProperty AttachedEllipseProperty = DependencyProperty.Register(nameof(AttachedEllipse), typeof(Ellipse), typeof(EllipseAssist), new PropertyMetadata(default(Ellipse), OnAttachedEllipseChanged));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(EllipseAssist), new PropertyMetadata(default(double), OnValueChanged));

        public EllipseAssist()
        {
            InitializeComponent();
        }

        public Ellipse AttachedEllipse
        {
            get
            {
                return (Ellipse)GetValue(AttachedEllipseProperty);
            }
            set
            {
                SetValue(AttachedEllipseProperty, value);
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

        private static void OnAttachedEllipseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (EllipseAssist)d;

            obj.UpdateAttachedEllipseStrokeDashArray();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (EllipseAssist)d;

            obj.UpdateAttachedEllipseStrokeDashArray();
        }

        private void UpdateAttachedEllipseStrokeDashArray()
        {
            var ellipse = AttachedEllipse;
            if (ellipse != null)
            {
                var value = Value;
                if (value > 0)
                {
                    var circumference = ellipse.GetCircumference();
                    ellipse.StrokeDashArray = new DoubleCollection()
                    {
                        circumference * value,
                        circumference * (1 - value)
                    };
                }
                else if (value < 0)
                {
                    var circumference = ellipse.GetCircumference();
                    ellipse.StrokeDashArray = new DoubleCollection()
                    {
                        0,
                        circumference * (1 + value),
                        0 - circumference * value
                    };
                }
                else
                {
                    ellipse.StrokeDashArray = new DoubleCollection()
                    {
                        0
                    };
                }
            }
        }
    }
}