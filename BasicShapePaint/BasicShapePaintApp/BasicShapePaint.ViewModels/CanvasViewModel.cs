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
    using System.Collections.Specialized;
    using System.Collections.Generic;

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
            Shapes = new ObservableCollection<ShapeViewModel>();
            Shapes.CollectionChanged += Shapes_CollectionChanged;
            ViewModelMediator.RegisterToViewModelEvent(
                ViewModelMediator.ViewModelEvent.SelectedShapeChanged, () => Reset());
            ViewModelMediator.RegisterToViewModelEvent(
                ViewModelMediator.ViewModelEvent.MovingModeChanged, MovingModeChanged);
        }

        private void Shapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                (e.OldItems[e.OldItems.Count - 1] as ShapeViewModel).CleanUp();
            }
        }

        private void MovingModeChanged()
        {
            if (ViewModelMediator.MovingMode)
            {
                Reset();
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public ObservableCollection<ShapeViewModel> Shapes { get; }

        #endregion Public Properties

        #region Public Methods

        public void LeftMouseUpEventHandler(Point mouseCoordinate)
        {
            if (!ViewModelMediator.MovingMode)
            {
                if (firstPoint == null)
                {
                    ViewModelMediator.RaiseViewModelEvent(this, ViewModelMediator.ViewModelEvent.DrawingStarted);
                    drawn = false;  // new drawing started
                    ShapeViewModel shapeVM = CreateShapeVM();
                    firstPoint = mouseCoordinate;

                    if (shapeVM.Shape is Line line)
                    {
                        line.X1 = firstPoint.X;
                        line.Y1 = firstPoint.Y;
                        line.X2 = firstPoint.X;
                        line.Y2 = firstPoint.Y;
                        Shapes.Add(shapeVM);
                    }
                    else
                    {
                        shapeVM.Shape.Width = 0;
                        shapeVM.Shape.Height = 2;
                        shapeVM.Shape.RenderTransform = new TranslateTransform(firstPoint.X, firstPoint.Y);
                        Shapes.Add(shapeVM);

                        ShapeViewModel tempLineVM = CreateShapeVM(ShapeType.Line);
                        Line tempLine = tempLineVM.Shape as Line;
                        tempLine.X1 = firstPoint.X;
                        tempLine.Y1 = firstPoint.Y;
                        tempLine.X2 = firstPoint.X;
                        tempLine.Y2 = firstPoint.Y;
                        Shapes.Add(tempLineVM);
                    }
                }
                else if (secondPoint == null)
                {
                    secondPoint = mouseCoordinate;
                    if (ViewModelMediator.SelectedShapeType == ShapeType.Line)
                    {
                        Shape shape = Shapes.Last().Shape;
                        (shape as Line).X2 = secondPoint.X;
                        (shape as Line).Y2 = secondPoint.Y;
                        drawn = true;
                        firstPoint = secondPoint = null;
                        ViewModelMediator.RaiseViewModelEvent(this,
                            ViewModelMediator.ViewModelEvent.DrawingEnded);
                    }
                    else
                    {
                        Shapes.Remove(Shapes.Last());
                        Shape shape = Shapes.Last().Shape;
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
                    ViewModelMediator.RaiseViewModelEvent(this,
                        ViewModelMediator.ViewModelEvent.DrawingEnded);
                }
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
                var shape = Shapes.Last().Shape;

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

        #endregion Public Methods

        #region Private Methods

        private ShapeViewModel CreateShapeVM(ShapeType? type = null)
        {
            ShapeType shapeType = type ?? ViewModelMediator.SelectedShapeType;
            ShapeViewModel shapeVM = shapeType switch
            {
                ShapeType.Rectangle => new ShapeViewModel(new Rectangle()),
                ShapeType.Ellipse => new ShapeViewModel(new Ellipse()),
                ShapeType.Line => new ShapeViewModel(new Line())
            };

            return shapeVM;
        }

        private void Reset()
        {
            GC.Collect();
            if (!drawn && firstPoint != null)
            {
                Shapes.Remove(Shapes.Last());
            }

            firstPoint = secondPoint = null;
            drawn = false;
            ViewModelMediator.RaiseViewModelEvent(this,
                ViewModelMediator.ViewModelEvent.DrawingEnded);
        }

        #endregion Private Methods
    }
}