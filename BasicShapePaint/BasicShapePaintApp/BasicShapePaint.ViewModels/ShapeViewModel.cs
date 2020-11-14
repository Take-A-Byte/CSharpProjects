namespace BasicShapePaint.ViewModels
{
    using System;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using static BasicShapePaint.ViewModels.Utilities.MiscellaneousUtilities;

    internal class ShapeViewModel : BaseViewModel
    {
        #region Private Fields

        private Shape shape;
        private bool isSelected;

        #endregion Private Fields

        #region Public Constructors

        public ShapeViewModel(Shape shape)
        {
            this.shape = shape;
            shape.StrokeThickness = 2;
            shape.Stroke = ViewModelMediator.SelectedColor;
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
                        shape.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
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
                shape.MouseDown -= Shape_MouseDown;
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
            if (!(Shape is Line))
            {
                shape.Fill = ViewModelMediator.SelectedColor;
                shape.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }

            OtherShapeSelected += ShapeViewModel_OtherShapeSelected;
            shape.MouseDown += Shape_MouseDown;
        }

        private void MovingModeChanged()
        {
            if (!ViewModelMediator.MovingMode)
            {
                shape.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ViewModelMediator.MovingMode)
            {
                IsSelected = true;
            }
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