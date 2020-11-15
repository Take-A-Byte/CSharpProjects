namespace Messaging.Core.Interfaces
{
    using System;
    using System.Net;
    using Messaging_Client.Interfaces;

    internal interface ISocket
    {
        #region Public Events

        event EventHandler ReceivedData;

        #endregion Public Events

        #region Public Methods

        bool ConnectToServer(IPEndPoint serverEndPoint);

        bool SendData(byte[] buffer);

        bool CloseSocket();

        #endregion Public Methods
    }
}