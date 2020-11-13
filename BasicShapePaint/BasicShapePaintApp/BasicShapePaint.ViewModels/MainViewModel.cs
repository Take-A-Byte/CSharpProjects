namespace BasicShapePaint.ViewModels
{
    using BasicShapePaint.APIs;
    using BasicShapePaint.Models;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public enum ShapeType
    {
        Line,
        Rectangle,
        Ellipse
    }

    public class MainViewModel : BaseViewModel, IMouseEventHandlerVM
    {
        #region Private Fields

        private ShapeType selectedShapeType;

        #endregion Private Fields

        #region Public Constructors

        public MainViewModel()
        {
            Shapes = new ObservableCollection<Shape>();
        }

        #endregion Public Constructors

        #region Public Properties

        public ObservableCollection<Shape> Shapes { get; }

        public ShapeType SelectedShapeType
        {
            get => selectedShapeType;
            set
            {
                selectedShapeType = value;
                NotifyPropertyChanged(nameof(SelectedShapeType));
            }
        }

        #endregion Public Properties

        #region Private Methods

        public void MouseDownEventHandler(object sender, MouseButtonEventArgs e)
        {
            int a = 0;
        }

        public void MouseUpEventHandler(object sender, MouseButtonEventArgs e)
        {
            int a = 0;
        }

        public void MouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            int a = 0;
        }

        #endregion Private Methods
    }
}