namespace BasicShapePaint.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    public class BaseViewModel : INotifyPropertyChanged
    {
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