namespace Messaging_Server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    internal class Program
    {
        #region Private Fields

        private static TcpListener server;
        private static List<TcpClient> clients;

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            IPAddress myIP = IPAddress.Parse(Dns.GetHostEntry(hostName).AddressList[0].ToString());

            Int32 port = 4000;
            server = new TcpListener(myIP, port);

            server.Start();

            BackgroundWorker acceptClientWorker = new BackgroundWorker();
            acceptClientWorker.DoWork += AcceptClientWorker_DoWork;
            acceptClientWorker.RunWorkerAsync();

            while (true)
            {
            }
        }

        private static void AcceptClientWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected to " +
                    IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()) +
                    " on port number " + ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString());

                new Task(() => { AcceptMessageWorker_DoWork(client); }).Start();
            }
        }

        private static void AcceptMessageWorker_DoWork(TcpClient client)
        {
            clients.Add(client);

            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }

            // Shutdown and end connection
            client.Close();
        }

        #endregion Private Methods
    }
}