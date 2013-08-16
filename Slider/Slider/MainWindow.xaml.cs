using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Slider
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RoundSlider[] _rects;

        public MainWindow()
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromArgb(255, 83, 83, 83));
            Loaded += MainWindow_Loaded;
            foreach (RoundSlider result1 in (Content as Grid).Children.OfType<RoundSlider>())
            {
                result1.Maximum = 255;
                result1.Minimum = 0;
                result1.ValueChanged += result1_ValueChanged;
            }
        }

        private void result1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            for (int i = 0; i < _rects.Length; i++)
            {
                Color colorStartOld = (_rects[i].Background as LinearGradientBrush).GradientStops[0].Color;

                Color colorEndOld = (_rects[i].Background as LinearGradientBrush).GradientStops[1].Color;
                if (_rects[i] != (sender as RoundSlider))
                {
                    if ((sender as RoundSlider).Name.StartsWith("B"))
                    {
                        colorStartOld.B = (byte) e.NewValue;
                        colorEndOld.B = (byte) e.NewValue;
                    }
                    if ((sender as RoundSlider).Name.StartsWith("R"))
                    {
                        colorStartOld.R = (byte) e.NewValue;
                        colorEndOld.R = (byte) e.NewValue;
                    }
                    if ((sender as RoundSlider).Name.StartsWith("G"))
                    {
                        colorStartOld.G = (byte) e.NewValue;
                        colorEndOld.G = (byte) e.NewValue;
                    }
                }

                _rects[i].Background =
                    new LinearGradientBrush(
                        new GradientStopCollection(new[]
                        {new GradientStop(colorStartOld, 0f), new GradientStop(colorEndOld, 1f)}));
            }
            UpdateResult();
        }


        private void UpdateResult()
        {
            result.Fill =
                new SolidColorBrush(Color.FromArgb(255, (byte) RedSlider.Value, (byte) GreenSlider.Value,
                    (byte) BlueSlider.Value));
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _rects = (Content as Grid).Children.OfType<RoundSlider>().ToArray();
            RedSlider.Background =
                new LinearGradientBrush(
                    new GradientStopCollection(new[]
                    {new GradientStop(Colors.Black, 0f), new GradientStop(Colors.Red, 1f)}));
            GreenSlider.Background =
                new LinearGradientBrush(
                    new GradientStopCollection(new[]
                    {new GradientStop(Colors.Black, 0f), new GradientStop(Colors.Green, 1f)}));
            BlueSlider.Background =
                new LinearGradientBrush(
                    new GradientStopCollection(new[]
                    {new GradientStop(Colors.Black, 0f), new GradientStop(Colors.Blue, 1f)}));
        }
    }
}