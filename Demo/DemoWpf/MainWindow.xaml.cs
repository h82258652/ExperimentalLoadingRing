﻿using System;
using System.Windows;
using System.Windows.Media.Animation;
using DemoWpf.Extensions;

namespace DemoWpf
{
    public partial class MainWindow
    {
        private readonly BindableEllipseStrokeDashArray _bindableEllipseStrokeDashArray;

        public MainWindow()
        {
            InitializeComponent();
            _bindableEllipseStrokeDashArray = new BindableEllipseStrokeDashArray(Ellipse);
        }

        private void Ellipse_Loaded(object sender, RoutedEventArgs e)
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = 0
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(1.5),
                Value = 240
            });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(3),
                Value = 720
            });
            animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, Ellipse);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Ellipse.RenderTransform).(RotateTransform.Angle)"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimationUsingKeyFrames();
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
            Storyboard.SetTargetProperty(animation, new PropertyPath("Value"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}