namespace Messaging.ViewModels.Utilities
{
    using System;
    using System.Windows.Input;

    internal class RelayCommand : ICommand
    {
        #region Private Fields

        private Action<object> execute;
        private Func<object, bool> canExecute;

        #endregion Private Fields

        #region Public Constructors

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion Public Events

        #region Public Methods

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        #endregion Public Methods
    }
}