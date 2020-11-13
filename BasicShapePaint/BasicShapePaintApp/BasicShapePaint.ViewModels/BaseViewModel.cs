namespace BasicShapePaint.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    public partial class BaseViewModel : INotifyPropertyChanged
    {
        #region Public Constructors

        public BaseViewModel()
        {
            if (this is MenuBarViewModel menubarVM)
            {
                ViewModelMediator.menubarVM = menubarVM;
            }
            else if (this is CanvasViewModel canvasVM)
            {
                ViewModelMediator.canvasVM = canvasVM;
            }
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Protected Methods

        protected void NotifyPropertyChanged(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion Protected Methods
    }
}