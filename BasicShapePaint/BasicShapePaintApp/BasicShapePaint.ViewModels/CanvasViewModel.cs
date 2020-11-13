namespace BasicShapePaint.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using BasicShapePaint.Utilities.APIs;
    using BasicShapePaint.Utilities;
    using System.Linq;
    using static BasicShapePaint.ViewModels.Utilities.MiscellaneousUtilities;

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
            ViewModelMediator.RegisterToViewModelEvent(
                ViewModelMediator.ViewModelEvent.SelectedShapeChanged, Reset);
        }

        #endregion Public Constructors

        #region Public Properties

        public ObservableCollection<Shape> Shapes { get; }

        #endregion Public Properties

        #region Public Methods

        public void LeftMouseUpEventHandler(Point mouseCoordinate)
        {
            if (firstPoint == null)
            {
                ViewModelMediator.RaiseViewModelEvent(this, ViewModelMediator.ViewModelEvent.DrawingStarted);
                drawn = false;  // new drawing started
                Shape shape = CreateShape();
                firstPoint = mouseCoordinate;

                if (shape is Line line)
                {
                    line.X1 = firstPoint.X;
                    line.Y1 = firstPoint.Y;
                    line.X2 = firstPoint.X;
                    line.Y2 = firstPoint.Y;
                    Shapes.Add(shape);
                }
                else
                {
                    shape.Width = 0;
                    shape.Height = 2;
                    shape.RenderTransform = new TranslateTransform(firstPoint.X, firstPoint.Y);
                    Shapes.Add(shape);

                    Line tempShape = CreateShape(ShapeType.Line) as Line;
                    tempShape.X1 = firstPoint.X;
                    tempShape.Y1 = firstPoint.Y;
                    tempShape.X2 = firstPoint.X;
                    tempShape.Y2 = firstPoint.Y;
                    Shapes.Add(tempShape);
                }
            }
            else if (secondPoint == null)
            {
                secondPoint = mouseCoordinate;
                if (ViewModelMediator.SelectedShapeType == ShapeType.Line)
                {
                    Shape shape = Shapes.Last();
                    (Shapes.Last() as Line).X2 = secondPoint.X;
                    (Shapes.Last() as Line).Y2 = secondPoint.Y;
                    drawn = true;
                    firstPoint = secondPoint = null;
                    ViewModelMediator.RaiseViewModelEvent(this, ViewModelMediator.ViewModelEvent.DrawingEnded);
                }
                else
                {
                    Shapes.Remove(Shapes.Last());
                    Shape shape = Shapes.Last();
                    var angle = Math.Atan((secondPoint.Y - firstPoint.Y) / (secondPoint.X - firstPoint.X));
                    var rotate = new RotateTransform(angle * 180 / 3.14);
                    rotate.CenterX = (firstPoint.X + secondPoint.X) / 2;
                    rotate.CenterY = (firstPoint.Y + secondPoint.Y) / 2;
                    shape.RenderTransform = rotate;
                }
            }
            else
            {
                drawn = true;
                firstPoint = secondPoint = null;
                Shape shape = Shapes.Last();
                ViewModelMediator.RaiseViewModelEvent(this, ViewModelMediator.ViewModelEvent.DrawingEnded);
                shape.Fill = ViewModelMediator.SelectedColor;
                shape.MouseDown += Shape_MouseDown;
            }
        }

        public void RightMouseUpEventHandler(Point mouseCoordinate)
        {
            Reset();
        }

        public void MouseMoveEventHandler(Point mouseCoordinate)
        {
            if (Shapes.Count != 0 && !drawn)
            {
                var shape = Shapes.Last();

                if (shape is Line line && firstPoint != null)
                {
                    line.X2 = mouseCoordinate.X;
                    line.Y2 = mouseCoordinate.Y;
                }
                else if (secondPoint != null)
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

        private void Shape_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var shape = sender as Shape;
            shape.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        #endregion Public Methods

        #region Private Methods

        private Shape CreateShape(ShapeType? type = null)
        {
            ShapeType shapeType = type ?? ViewModelMediator.SelectedShapeType;
            Shape shape = shapeType switch
            {
                ShapeType.Rectangle => new Rectangle(),
                ShapeType.Ellipse => new Ellipse(),
                ShapeType.Line => new Line()
            };

            shape.StrokeThickness = 2;
            shape.Stroke = ViewModelMediator.SelectedColor;
            return shape;
        }

        private void Reset()
        {
            if (!drawn && firstPoint != null)
            {
                Shapes.Remove(Shapes.Last());
            }

            firstPoint = secondPoint = null;
            drawn = false;
            ViewModelMediator.RaiseViewModelEvent(this, ViewModelMediator.ViewModelEvent.DrawingEnded);
        }

        #endregion Private Methods
    }
}