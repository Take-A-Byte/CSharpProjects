namespace BasicShapePaint.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using BasicShapePaint.APIs;

    internal class CanvasViewModel : BaseViewModel, IMouseEventHandlerVM
    {
        #region Private Fields

        private BasicShapePaint.Models.Point startPoint;
        private BasicShapePaint.Models.Point secondPoint;
        private bool drawn;

        #endregion Private Fields

        #region Public Constructors

        public CanvasViewModel()
        {
            Shapes = new ObservableCollection<Shape>();
        }

        #endregion Public Constructors

        #region Public Properties

        public ObservableCollection<Shape> Shapes { get; }

        #endregion Public Properties

        #region Public Methods

        public void MouseDownEventHandler(object sender, MouseButtonEventArgs e)
        {
            var canvas = sender as ItemsControl;
            Rectangle rect;
            if (startPoint == null)
            {
                rect = new Rectangle();
                startPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
                rect.Width = 0;
                rect.Height = 0;
                rect.StrokeThickness = 2;
                rect.Stroke = new SolidColorBrush(Colors.Red);
                rect.RenderTransform = new TranslateTransform(startPoint.X, startPoint.Y);
                Shapes.Add(rect);
            }
            else if (secondPoint == null)
            {
                rect = (Shapes[0] as Rectangle);
                secondPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
                var angle = Math.Atan((secondPoint.Y - startPoint.Y) / (secondPoint.X - startPoint.X));
                var rotate = new RotateTransform(angle * 180 / 3.14);
                rotate.CenterX = (startPoint.X + secondPoint.X) / 2;
                rotate.CenterY = (startPoint.Y + secondPoint.Y) / 2;
                rect.RenderTransform = rotate;
            }
            else
            {
                drawn = true;
                rect = (Shapes[0] as Rectangle);
            }
        }

        public void MouseUpEventHandler(object sender, MouseButtonEventArgs e)
        {
            int a = 0;
        }

        public void MouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            var canvas = sender as ItemsControl;
            if (canvas.Items.Count != 0)
            {
                var rect = (canvas.Items[0] as Rectangle);

                if (secondPoint != null && !drawn)
                {
                    var xDiff = secondPoint.X - startPoint.X;
                    var yDiff = secondPoint.Y - startPoint.Y;
                    rect.Width = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

                    var m = yDiff / xDiff;
                    var c = startPoint.Y - m * startPoint.X;
                    var tempHeight = (m * e.GetPosition(canvas).X - e.GetPosition(canvas).Y + c)
                         / Math.Sqrt(Math.Pow(m, 2) + 1);
                    rect.Height = Math.Abs(tempHeight);

                    Transform rectifyTranslate = Transform.Identity;
                    Transform rectifyWidthTranslate = new TranslateTransform(-rect.Width / 2, 0);
                    Transform translate = Transform.Identity;
                    TransformGroup tansformGroup;
                    if (!(rect.RenderTransform is TransformGroup existingGroup))
                    {
                        translate = new TranslateTransform(
                              (startPoint.X + secondPoint.X) / 2, (startPoint.Y + secondPoint.Y) / 2);
                        tansformGroup = new TransformGroup();
                        tansformGroup.Children.Add(rectifyTranslate);
                        tansformGroup.Children.Add(rectifyWidthTranslate);
                        tansformGroup.Children.Add(translate);
                        tansformGroup.Children.Add(rect.RenderTransform);
                        rect.RenderTransform = tansformGroup;
                    }

                    tansformGroup = rect.RenderTransform as TransformGroup;
                    if (tempHeight > 0)
                    {
                        tansformGroup.Children[0] = new TranslateTransform(0, -tempHeight);
                    }
                }
            }
        }

        #endregion Public Methods
    }
}