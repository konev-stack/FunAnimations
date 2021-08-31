using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FunProject
{
    /// <summary>
    /// Interaction logic for Executor.xaml
    /// </summary>
    public partial class Executor : Window
    {
        public Executor()
        {
            InitializeComponent();
        }
        Storyboard storyboard = new Storyboard();
        TimeSpan halfsecond = TimeSpan.FromMilliseconds(500);
        TimeSpan second = TimeSpan.FromSeconds(1);

        private IEasingFunction Smooth
        {
            get;
            set;
        }
        = new QuarticEase
        {
            EasingMode = EasingMode.EaseInOut
        };

        public void Fade(DependencyObject Object)
        {
            DoubleAnimation FadeIn = new DoubleAnimation()
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(halfsecond),
            };
            Storyboard.SetTarget(FadeIn, Object);
            Storyboard.SetTargetProperty(FadeIn, new PropertyPath("Opacity", 1));
            storyboard.Children.Add(FadeIn);
            storyboard.Begin();
        }

        public void FadeOut(DependencyObject Object)
        {
            DoubleAnimation FadeOut = new DoubleAnimation()
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(halfsecond),
            };
            Storyboard.SetTarget(FadeOut, Object);
            Storyboard.SetTargetProperty(FadeOut, new PropertyPath("Opacity", 1));
            storyboard.Children.Add(FadeOut);
            storyboard.Begin();
        }

        public void ObjectShiftPos(DependencyObject Object, Thickness Get, Thickness Set)
        {
            ThicknessAnimation ShiftAnimation = new ThicknessAnimation()
            {
                From = Get,
                To = Set,
                Duration = second,
                EasingFunction = Smooth,
            };
            Storyboard.SetTarget(ShiftAnimation, Object);
            Storyboard.SetTargetProperty(ShiftAnimation, new PropertyPath(MarginProperty));
            storyboard.Children.Add(ShiftAnimation);
            storyboard.Begin();
        }

        public void Resize()
        {
            DoubleAnimation danimationX = new DoubleAnimation();
            danimationX.From = MainBorder.Width;
            danimationX.To = 400;
            danimationX.Duration = second;
            danimationX.EasingFunction = Smooth;
            MainBorder.BeginAnimation(WidthProperty, danimationX);

            DoubleAnimation danimationY = new DoubleAnimation();
            danimationY.From = MainBorder.Height;
            danimationY.To = 300;
            danimationY.Duration = second;
            danimationY.EasingFunction = Smooth;
            MainBorder.BeginAnimation(HeightProperty, danimationY);
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.Fade(this.MainBorder);
            ObjectShiftPos(MainBorder, MainBorder.Margin, new Thickness(0));
            await Task.Delay(2000);
            Resize();
            await Task.Delay(1500);
            WorkingWindow workingWindow = new WorkingWindow();
            workingWindow.Show();
            this.Close();
        }
    }
}
