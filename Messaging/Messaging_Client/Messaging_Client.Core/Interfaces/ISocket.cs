namespace Messaging.Core.Interfaces
{
    using System.Net;

    internal interface ISocket
    {
        #region Public Methods

        bool ConnectToServer(IPEndPoint serverEndPoint);

        bool SendData(byte[] buffer);

        byte[] ReceiveData();

        bool CloseSocket();

        #endregion Public Methods
    }
}