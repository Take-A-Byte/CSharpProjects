namespace Messaging_Client.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Messaging.ViewModels.Utilities;
    using Messaging_Client.Interfaces;
    using Messaging_Client.Models;

    public class MainViewModel : BaseViewModel
    {
        #region Private Fields

        private string newMessageText;

        #endregion Private Fields

        #region Public Constructors

        public MainViewModel()
        {
            newMessageText = String.Empty;
            Messages = new ObservableCollection<IMessage>();
            InitializeCommands();
        }

        #endregion Public Constructors

        #region Public Properties

        public ObservableCollection<IMessage> Messages { get; set; }

        public string NewMessageText
        {
            get => newMessageText;
            set
            {
                newMessageText = value;
                NotifyPropertyChanged(nameof(NewMessageText));
            }
        }

        public ICommand SendMessageCommand { get; private set; }

        #endregion Public Properties

        #region Private Methods

        private void InitializeCommands()
        {
            SendMessageCommand = new RelayCommand(OnSendMessageCommand);
        }

        #region Command Handlers

        private void OnSendMessageCommand(object obj)
        {
            if (!String.IsNullOrWhiteSpace(NewMessageText))
            {
                IMessage newMessage = new MessageModel(NewMessageText);
                Messages.Add(newMessage);
            }
        }

        #endregion Command Handlers

        #endregion Private Methods
    }
}