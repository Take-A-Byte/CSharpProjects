namespace BasicShapePaint.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using BasicShapePaint.Utilities.APIs;
    using BasicShapePaint.Utilities;
    using System.Linq;

    internal class CanvasViewModel : BaseViewModel, IMouseEventHandlerVM
    {
        #region Private Fields

        private Point firstPoint;
        private Point secondPoint;
        private bool drawn;

        #endregion Private Fields

        #region Public Constructors

        public CanvasViewModel()
        {
            Shapes = new ObservableCollection<Shape>();
            ViewModelMediator.SelectedShapeChanged += ViewModelMediator_SelectedShapeChanged;
        }

        private void ViewModelMediator_SelectedShapeChanged()
        {
            if (!drawn && firstPoint != null)
            {
                Shapes.Remove(Shapes.Last());
            }

            firstPoint = secondPoint = null;
            drawn = false;
        }

        #endregion Public Constructors

        #region Public Properties

        public ObservableCollection<Shape> Shapes { get; }

        #endregion Public Properties

        #region Public Methods

        public void MouseDownEventHandler(Point mouseCoordinate)
        {
            Shape shape;
            if (firstPoint == null)
            {
                shape = CreateShape();
                firstPoint = mouseCoordinate;
                shape.Width = shape.Height = 0;
                shape.StrokeThickness = 2;
                shape.Stroke = ViewModelMediator.SelectedColor;
                shape.RenderTransform = new TranslateTransform(firstPoint.X, firstPoint.Y);
                Shapes.Add(shape);
            }
            else if (secondPoint == null)
            {
                shape = Shapes[0];
                secondPoint = mouseCoordinate;
                var angle = Math.Atan((secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X));
                var rotate = new RotateTransform(angle * 180 / 3.14);
                rotate.CenterX = (firstPoint.X + secondPoint.X) / 2;
                rotate.CenterY = (firstPoint.Y + secondPoint.Y) / 2;
                shape.RenderTransform = rotate;
            }
            else
            {
                drawn = true;
            }
        }

        public void MouseUpEventHandler(Point mouseCoordinate)
        {
            int a = 0;
        }

        public void MouseMoveEventHandler(Point mouseCoordinate)
        {
            if (Shapes.Count != 0)
            {
                var shape = Shapes[0];

                if (secondPoint != null && !drawn)
                {
                    var xDiff = secondPoint.X - firstPoint.X;
                    var yDiff = secondPoint.Y - firstPoint.Y;
                    shape.Width = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

                    var m = yDiff / xDiff;
                    var c = firstPoint.Y - m * firstPoint.X;
                    var tempHeight = (m * mouseCoordinate.X - mouseCoordinate.Y + c)
                         / Math.Sqrt(Math.Pow(m, 2) + 1);

                    if (shape is Rectangle)
                    {
                        shape.Height = Math.Abs(tempHeight);
                    }
                    else
                    {
                        shape.Height = Math.Abs(tempHeight) * 2;
                    }

                    Transform rectifyTranslate = Transform.Identity;
                    Transform rectifyWidthTranslate = new TranslateTransform(-shape.Width / 2, 0);
                    Transform translate = Transform.Identity;
                    TransformGroup tansformGroup;
                    if (!(shape.RenderTransform is TransformGroup existingGroup))
                    {
                        translate = new TranslateTransform(
                              (firstPoint.X + secondPoint.X) / 2, (firstPoint.Y + secondPoint.Y) / 2);
                        tansformGroup = new TransformGroup();
                        tansformGroup.Children.Add(rectifyTranslate);
                        tansformGroup.Children.Add(rectifyWidthTranslate);
                        tansformGroup.Children.Add(translate);
                        tansformGroup.Children.Add(shape.RenderTransform);
                        shape.RenderTransform = tansformGroup;
                    }

                    tansformGroup = shape.RenderTransform as TransformGroup;
                    if (tempHeight > 0)
                    {
                        tansformGroup.Children[0] = new TranslateTransform(0, -tempHeight);
                    }
                    else if (shape is Ellipse)
                    {
                        tansformGroup.Children[0] = new TranslateTransform(0, tempHeight);
                    }
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private Shape CreateShape()
        {
            return ViewModelMediator.SelectedShapeType switch
            {
                ShapeType.Rectangle => new Rectangle(),
                ShapeType.Ellipse => new Ellipse(),
                ShapeType.Line => new Line()
            };
        }

        #endregion Private Methods
    }
}