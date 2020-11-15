namespace BasicShapePaint.ViewModels
{
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using BasicShapePaint.Utilities;
    using static BasicShapePaint.ViewModels.Utilities.MiscellaneousUtilities;

    public class ShapeViewModel : BaseViewModel
    {
        #region Private Fields

        private Shape shape;
        private Brush shapeBrush;
        private BasicShapePaint.Utilities.Point translateFrom;
        private bool isSelected;

        #endregion Private Fields

        #region Public Constructors

        public ShapeViewModel(Shape shape)
        {
            this.shape = shape;
            shape.StrokeThickness = 2;
            shape.Stroke = ViewModelMediator.SelectedColor;
            shapeBrush = shape.Stroke;
            ViewModelMediator.RegisterToViewModelEvent(
                ViewModelMediator.ViewModelEvent.DrawingEnded, DrawingEndedEventHandler);
            ViewModelMediator.RegisterToViewModelEvent(
                ViewModelMediator.ViewModelEvent.MovingModeChanged, MovingModeChanged);
        }

        #endregion Public Constructors

        #region Private Events

        private static event EmptyEventHandler OtherShapeSelected;

        #endregion Private Events

        #region Public Properties

        public Shape Shape { get => shape; }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (value != isSelected)
                {
                    if (value)
                    {
                        OtherShapeSelected?.Invoke();
                        shape.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    }
                    else
                    {
                        if (Shape is Line || Shape is Polyline)
                        {
                            shape.Stroke = shapeBrush;
                        }
                        else
                        {
                            shape.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        }
                    }

                    isSelected = value;
                }
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void CleanUp()
        {
            try
            {
                OtherShapeSelected -= ShapeViewModel_OtherShapeSelected;
                shape.MouseDown -= Shape_LeftMouseDown;
            }
            finally
            {
                ViewModelMediator.UnregisterToViewModelEvent(
                    ViewModelMediator.ViewModelEvent.DrawingEnded, DrawingEndedEventHandler);
                ViewModelMediator.UnregisterToViewModelEvent(
                    ViewModelMediator.ViewModelEvent.MovingModeChanged, MovingModeChanged);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void DrawingEndedEventHandler()
        {
            ViewModelMediator.UnregisterToViewModelEvent(
                ViewModelMediator.ViewModelEvent.DrawingEnded, DrawingEndedEventHandler);
            if (!(Shape is Line || Shape is Polyline))
            {
                shape.Fill = ViewModelMediator.SelectedColor;
                shape.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }

            OtherShapeSelected += ShapeViewModel_OtherShapeSelected;
            shape.MouseLeftButtonDown += Shape_LeftMouseDown;
        }

        private void MovingModeChanged()
        {
            if (!ViewModelMediator.MovingMode)
            {
                IsSelected = false;
            }
        }

        private void Shape_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ViewModelMediator.MovingMode)
            {
                IsSelected = true;
                translateFrom = new BasicShapePaint.Utilities.Point(
                    e.GetPosition(shape).X, e.GetPosition(shape).Y);
                shape.PreviewMouseMove += Shape_PreviewMouseMove;
                CanvasViewModel.MouseMovedOnCanvas += MouseMovedOnCanvas; ;
                shape.MouseLeftButtonUp += Shape_MouseLeftButtonUp;
            }
        }

        private void MouseMovedOnCanvas(CanvasPoint mouseCoordinate)
        {
            Point relativePoint = mouseCoordinate.GetRelativePosition(shape);
            if (Shape is Line || Shape is Polyline || !(relativePoint.X > 0 && relativePoint.Y > 0))
            {
                PreviewMouseMove(relativePoint);
            }
        }

        private void Shape_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            PreviewMouseMove(new Point(e.GetPosition(Shape).X, e.GetPosition(Shape).Y));
        }

        private void Shape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            translateFrom = null;
            shape.PreviewMouseMove -= Shape_PreviewMouseMove;
            CanvasViewModel.MouseMovedOnCanvas -= MouseMovedOnCanvas;
            IsSelected = false;
        }

        private void PreviewMouseMove(Point mouseCoordinate)
        {
            Transform translate = new TranslateTransform(
                mouseCoordinate.X - translateFrom.X, mouseCoordinate.Y - translateFrom.Y);
            TransformGroup group = new TransformGroup();
            group.Children.Add(translate);
            group.Children.Add(Shape.RenderTransform);
            Shape.RenderTransform = group;
        }

        private void ShapeViewModel_OtherShapeSelected()
        {
            if (IsSelected)
            {
                IsSelected = false;
            }
        }

        #endregion Private Methods
    }
}