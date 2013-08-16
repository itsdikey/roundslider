using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Slider
{
    /// <summary>
    ///     Interaction logic for RoundSlider.xaml
    /// </summary>
    public partial class RoundSlider : UserControl
    {
        public RoundSlider()
        {
            InitializeComponent();
            Maximum = 255;
            Minimum = 0;
            GridContent.MouseMove += GridContent_MouseMove;
        }

        public int Maximum { get; set; }

        public int Minimum { get; set; }

        public new Brush Background
        {
            get
            {
                return (GridContent.Children[0] as Rectangle).Fill;
            }
            set
            {
                (GridContent.Children[0] as Rectangle).Fill = value;
            }
        }

        public Double Value
        {
            get { return GetCurrentValue(); }
            set { throw new UnauthorizedAccessException(); }
        }

        private double GetCurrentValue()
        {
            var transform = RoundWithoutCenterRoundPath.RenderTransform as TranslateTransform;
            if (transform == null) return Minimum;
            double x = (transform).X;
            x = Minimum + ((x/255)*(Maximum - Minimum));
            return x;
        }

        private void GridContent_MouseMove(object sender, MouseEventArgs e)
        {
            var args = new ValueChangedEventArgs(){ OldValue =  GetCurrentValue()};
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var x = QcelBrnel(e.GetPosition(GridContent).X - 20);
            var path = GridContent.Children[1] as Path;
            if (path != null)
                path.RenderTransform = new TranslateTransform(x, 0);
            args.NewValue = GetCurrentValue();
            OnValueChanged(args);
        }

        private double QcelBrnel(double o)
        {
            var rectangle = GridContent.Children[0] as Rectangle;
            if (rectangle == null) return 0;
            var width = rectangle.Width;

            return o < 0 ? 0 : o > width ? width : o;
        }

        public delegate void ValueChangedHandler(object sender, ValueChangedEventArgs e);

        public event ValueChangedHandler ValueChanged;

        protected virtual void OnValueChanged(ValueChangedEventArgs e)
        {
            ValueChangedHandler handler = ValueChanged;
            if (handler != null) handler(this, e);
        }
    }
}