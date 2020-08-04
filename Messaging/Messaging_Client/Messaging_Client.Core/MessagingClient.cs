namespace Messaging_Client.Core
{
    using Messaging.Core;
    using Messaging.Core.Interfaces;
    using Messaging_Client.Interfaces;
    using System.Net;

    public class MessagingClient : IMessagingClient
    {
        #region Private Fields

        private ISocket clientSocket;

        #endregion Private Fields

        #region Public Constructors

        public MessagingClient()
        {
            clientSocket = new TCPClientSocket();
        }

        #endregion Public Constructors

        #region Public Methods

        public bool ConnectToServer()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            IPAddress myIP = IPAddress.Parse(Dns.GetHostByName(hostName).AddressList[0].ToString());

            int serverPort = 4000;
            IPEndPoint serverEndPt = new IPEndPoint(myIP, serverPort);

            return clientSocket.ConnectToServer(serverEndPt);
        }

        public bool DisconnectFromServer()
        {
            throw new System.NotImplementedException();
        }

        public bool SendMessage(IMessage message)
        {
            return clientSocket.SendData(message?.ToByte());
        }

        #endregion Public Methods
    }
}