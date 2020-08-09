namespace Messaging.Core
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using Messaging.Core.Interfaces;
    using Messaging_Client.Interfaces;

    internal class TCPClientSocket : ISocket
    {
        #region Private Fields

        private TcpClient client;

        #endregion Private Fields

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
                client = new TcpClient();
                client.Connect(serverEndPoint);
            }
            catch (Exception e)
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
                stream.Read(buffer, 0, buffer.Length);
            }
        }

        #endregion Private Methods
    }
}