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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Demo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private BindableStrokeDashArray _bindableStrokeDashArray;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var bindableStrokeDashArray = new BindableStrokeDashArray(Ellipse);
            _bindableStrokeDashArray = bindableStrokeDashArray;

            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.EnableDependentAnimation = true;
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = 0
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(1.5),
                Value = 2 * Math.PI * 50
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(1.5),
                Value = 0 - 2 * Math.PI * 50
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(3),
                Value = 0
            });
            animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, bindableStrokeDashArray);
            Storyboard.SetTargetProperty(animation, "Value");
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 360;
            animation.Duration = TimeSpan.FromSeconds(3);
            animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, Ellipse);
            Storyboard.SetTargetProperty(animation, "(Ellipse.RenderTransform).(RotateTransform.Angle)");
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}