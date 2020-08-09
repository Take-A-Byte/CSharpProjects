namespace Messaging_Client.Core
{
    using Messaging.Core;
    using Messaging.Core.Interfaces;
    using Messaging_Client.Interfaces;
    using Messaging_Client.PacketFactory;
    using Messaging_Client.Utilities;
    using System.Collections.Generic;
    using System.Net;

    public class MessagingClient : IMessagingClient
    {
        #region Private Fields

        private IPacketFactory packetFactory;
        private ISocket clientSocket;
        private List<IServiceUser> serviceUsers;

        #endregion Private Fields

        #region Public Constructors

        public MessagingClient(IPacketFactory packetFactory)
        {
            this.packetFactory = packetFactory;
            clientSocket = new TCPClientSocket();
        }

        #endregion Public Constructors

        #region Public Methods

        public bool ConnectToServer(string userName)
        {
            bool successful = false;

            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            IPAddress myIP = IPAddress.Parse(Dns.GetHostByName(hostName).AddressList[0].ToString());

            int serverPort = 4000;
            IPEndPoint serverEndPt = new IPEndPoint(myIP, serverPort);

            successful = clientSocket.ConnectToServer(serverEndPt);
            clientSocket.ReceivedData += ClientSocket_ReceivedData;

            successful = successful && clientSocket.SendData(
                packetFactory.CreateUserPacket(userName, ((TCPClientSocket)clientSocket).IPEndPoint).ToByte());

            return successful;
        }

        public bool DisconnectFromServer()
        {
            bool successful = false;

            if (clientSocket != null)
            {
                successful = clientSocket.CloseSocket();
            }

            return successful;
        }

        public bool SendMessage(IMessage message)
        {
            return clientSocket.SendData(packetFactory.CreateMessagePacket(message).ToByte());
        }

        private void ClientSocket_ReceivedData(object sender, System.EventArgs e)
        {
            if (e is MessagingEventArgs.ReceivedDataEventArgs args)
            {
                IPacket packet = packetFactory.HandlePacket(args.Buffer);

                if (packet.Type == PacketType.User)
                {
                    serviceUsers.AddRange(((IUsersPacket)packet).Users);
                }
            }
        }

        #endregion Public Methods
    }
}