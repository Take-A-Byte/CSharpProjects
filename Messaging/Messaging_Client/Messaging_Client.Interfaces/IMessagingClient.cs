namespace Messaging_Client.Interfaces
{
    public interface IMessagingClient
    {
        #region Public Methods

        bool ConnectToServer();

        bool SendMessage(IMessage message);

        bool DisconnectFromServer();

        #endregion Public Methods
    }
}