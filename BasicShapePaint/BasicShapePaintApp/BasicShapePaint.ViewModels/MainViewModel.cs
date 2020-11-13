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

    public class MainViewModel : BaseViewModel
    {
        #region Public Constructors

        public MainViewModel()
        {
            MenuBarVM = new MenuBarViewModel();
            CanvasVM = new CanvasViewModel();
        }

        #endregion Public Constructors

        #region Public Properties

        public BaseViewModel MenuBarVM { get; }

        public BaseViewModel CanvasVM { get; }

        #endregion Public Properties
    }
}