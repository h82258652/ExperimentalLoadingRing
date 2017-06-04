using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace Demo2
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

    public sealed partial class EllipseAssist
    {
        public static readonly DependencyProperty AttachedEllipseProperty = DependencyProperty.Register(nameof(AttachedEllipse), typeof(Ellipse), typeof(EllipseAssist), new PropertyMetadata(default(Ellipse), OnAttachedEllipseChanged));

        private static void OnAttachedEllipseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(EllipseAssist), new PropertyMetadata(false, OnIsActiveChanged));

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

        public bool IsActive
        {
            get
            {
                return (bool)GetValue(IsActiveProperty);
            }
            set
            {
                SetValue(IsActiveProperty, value);
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

        private Storyboard _storyboard;

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (EllipseAssist)d;
            var value = (bool)e.NewValue;

            if (value && obj.AttachedEllipse != null)
            {
                obj._storyboard?.Stop();
                var storyboard = new Storyboard();
                var animation = new DoubleAnimationUsingKeyFrames()
                {
                    EnableDependentAnimation = true
                };
                animation.KeyFrames.Add(new LinearDoubleKeyFrame()
                {
                    KeyTime = TimeSpan.FromSeconds(0),
                    Value = 0
                });
                animation.KeyFrames.Add(new LinearDoubleKeyFrame()
                {
                    KeyTime = TimeSpan.FromSeconds(1.5),
                    Value = obj.AttachedEllipse.GetCircumference()
                });
                animation.KeyFrames.Add(new LinearDoubleKeyFrame()
                {
                    KeyTime = TimeSpan.FromSeconds(1.5),
                    Value = 0 - obj.AttachedEllipse.GetCircumference()
                });
                animation.KeyFrames.Add(new LinearDoubleKeyFrame()
                {
                    KeyTime = TimeSpan.FromSeconds(3),
                    Value = 0
                });
                animation.RepeatBehavior = RepeatBehavior.Forever;
                Storyboard.SetTarget(animation, obj);
                Storyboard.SetTargetProperty(animation, "Value");
                storyboard.Children.Add(animation);
                obj._storyboard = storyboard;
                storyboard.Begin();
            }
            else
            {
                obj._storyboard?.Stop();
            }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (EllipseAssist)d;

            var ellipse = obj.AttachedEllipse;
            if (ellipse != null)
            {
                var value = (double)e.NewValue;
                if (value > 0)
                {
                    ellipse.StrokeDashArray = new DoubleCollection()
                    {
                        value,
                        ellipse.GetCircumference() - value
                    };
                }
                else if (value < 0)
                {
                    ellipse.StrokeDashArray = new DoubleCollection()
                    {
                        0,
                        ellipse.GetCircumference() + value,
                        0 - value
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