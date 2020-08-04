namespace Messaging.Core
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using Messaging.Core.Interfaces;

    internal class TCPClientSocket : ISocket
    {
        #region Private Fields

        private TcpClient client;

        #endregion Private Fields

        #region Public Methods

        public bool ConnectToServer(IPEndPoint serverEndPoint)
        {
            try
            {
                client = new TcpClient();
                client.Connect(serverEndPoint);
            }
            catch (System.Exception e)
            {
                return false;
            }

            return true;
        }

        public byte[] ReceiveData()
        {
            throw new System.NotImplementedException();
        }

        public bool SendData(byte[] buffer)
        {
            if (client.Connected)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    string message = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
                    message += '.' + ((IPEndPoint)client.Client.LocalEndPoint).Port.ToString();
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    return true;
                }
                catch (Exception e)
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

        private static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        #endregion Public Methods
    }
}