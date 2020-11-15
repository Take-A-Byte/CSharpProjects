namespace BasicShapePaint.Utilities.APIs
{
    using System.ComponentModel;

    public interface IMainViewModel
    {
        #region Public Properties

        INotifyPropertyChanged CanvasVM { get; }

        INotifyPropertyChanged MenuBarVM { get; }

        #endregion Public Properties
    }
}