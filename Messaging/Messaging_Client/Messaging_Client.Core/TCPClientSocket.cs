namespace Messaging.Core
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using Messaging.Core.Interfaces;
    using Messaging_Client.Interfaces;
    using Messaging_Client.Utilities;
    using static Messaging.Core.MessagingEventArgs;

    internal class TCPClientSocket : ISocket
    {
        #region Private Fields

        private TcpClient client;

        #endregion Private Fields

        #region Public Constructors

        public TCPClientSocket()
        {
            client = new TcpClient();
            new Task(() => ListenToAllUsers());
        }

        #endregion Public Constructors

        #region Public Properties

        public IPEndPoint IPEndPoint => (IPEndPoint)client.Client.LocalEndPoint;

        #endregion Public Properties

        #region Public Events

        public event EventHandler ReceivedData;

        #endregion Public Events

        #region Public Methods

        public bool ConnectToServer(IPEndPoint serverEndPoint)
        {
            try
            {
                client.Connect(serverEndPoint);
                new Thread(() => { StartListening(); }).Start();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool SendData(byte[] buffer)
        {
            if (client.Connected)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    //string message = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
                    //message += '.' + ((IPEndPoint)client.Client.LocalEndPoint).Port.ToString();
                    //Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    stream.Write(buffer, 0, buffer.Length);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CloseSocket()
        {
            try
            {
                client.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion Public Methods

        #region Private Methods

        private void StartListening()
        {
            NetworkStream stream = client.GetStream();

            while (client.Connected)
            {
                byte[] buffer = new byte[1024];
                int length = stream.Read(buffer, 0, buffer.Length);

                ReceivedDataEventArgs args = new ReceivedDataEventArgs(buffer.SubArray(0, length));
                ReceivedData?.Invoke(this, args);
            }
        }

        private void ListenToAllUsers()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            IPAddress myIP = IPAddress.Parse(Dns.GetHostByName(hostName).AddressList[0].ToString());

            client.Connect(new IPEndPoint(myIP, 0));
        }

        #endregion Private Methods
    }
}