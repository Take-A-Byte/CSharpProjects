namespace BasicShapePaint.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using BasicShapePaint.APIs;
    using BasicShapePaint.Models;

    internal class CanvasViewModel : BaseViewModel, IMouseEventHandlerVM
    {
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

        #endregion Public Methods
    }
}