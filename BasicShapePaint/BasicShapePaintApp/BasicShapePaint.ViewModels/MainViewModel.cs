namespace BasicShapePaint.ViewModels
{
    using BasicShapePaint.Utilities.APIs;
    using System.ComponentModel;

    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        #region Public Constructors

        public MainViewModel()
        {
            MenuBarVM = new MenuBarViewModel();
            CanvasVM = new CanvasViewModel();
        }

        #endregion Public Constructors

        #region Public Properties

        public INotifyPropertyChanged MenuBarVM { get; }

        public INotifyPropertyChanged CanvasVM { get; }

        #endregion Public Properties
    }
}