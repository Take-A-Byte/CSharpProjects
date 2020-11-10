namespace BasicShapePaint.ViewModels.Utilities
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        #region Private Fields

        private readonly Action execute;

        private readonly Func<bool> canExecute;

        #endregion Private Fields

        #region Public Constructors

        public RelayCommand(Action execute) : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler CanExecuteChanged;

        #endregion Public Events

        #region Public Methods

        public bool CanExecute(object parameter) => canExecute == null || canExecute();

        public void Execute(object parameter) => execute();

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        #endregion Public Methods
    }
}