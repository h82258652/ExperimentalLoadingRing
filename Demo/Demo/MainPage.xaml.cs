using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Demo.Extensions;

namespace Demo
{
    public sealed partial class MainPage
    {
        private readonly BindableEllipseStrokeDashArray _bindableEllipseStrokeDashArray;

        public MainPage()
        {
            InitializeComponent();
            _bindableEllipseStrokeDashArray = new BindableEllipseStrokeDashArray(Ellipse);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
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
                Value = Ellipse.GetCircumference()
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(1.5),
                Value = 0 - Ellipse.GetCircumference()
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(3),
                Value = 0
            });
            animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, _bindableEllipseStrokeDashArray);
            Storyboard.SetTargetProperty(animation, "Value");
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever
            };
            Storyboard.SetTarget(animation, Ellipse);
            Storyboard.SetTargetProperty(animation, "(Ellipse.RenderTransform).(RotateTransform.Angle)");
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}