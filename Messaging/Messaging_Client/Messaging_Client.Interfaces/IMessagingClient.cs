namespace Messaging_Client.Interfaces
{
    public interface IMessagingClient
    {
        #region Public Methods

        bool ConnectToServer(string username);

        bool SendMessage(IMessage message);

        bool DisconnectFromServer();

        #endregion Public Methods
    }
}