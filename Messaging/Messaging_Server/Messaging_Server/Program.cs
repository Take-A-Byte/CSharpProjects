﻿namespace Messaging_Server
{
    using Messaging_Client.Interfaces;
    using Messaging_Client.PacketFactory;
    using Messaging_Client.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using System.Linq;

    internal class Program
    {
        #region Private Fields

        private static TcpListener server;
        private static IPacketFactory packetFactory;
        private static Dictionary<IServiceUser, TcpClient> users;

        #endregion Private Fields

        #region Private Methods

        private static void Main(string[] args)
        {
            packetFactory = new PacketFactory();
            users = new Dictionary<IServiceUser, TcpClient>();
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            IPAddress myIP = IPAddress.Parse(Dns.GetHostByName(hostName).AddressList[0].ToString());

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
            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                IPacket packet = packetFactory.HandlePacket(bytes.SubArray(0, i));

                if (packet.Type == PacketType.User)
                {
                    new Task(() => NotifyUserAdded(packet)).Start();

                    // we know that only one user will be registered per client
                    users.Add(((IUsersPacket)packet).Users[0], client);
                    Console.WriteLine("\nReceived: Registration for {0}", users.Last().Key.Name);
                    Console.Write("Waiting for a connection... ");
                    var allRegisteredUsers = packetFactory.CreateUserPacket(users.Keys.ToList());
                    byte[] msg = allRegisteredUsers.ToByte();

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: All registered user list.");
                }
            }

            // Shutdown and end connection
            client.Close();
        }

        private static void NotifyUserAdded(IPacket packet)
        {
            if (packet is IUsersPacket usersPacket)
            {
                foreach (var user in users)
                {
                    if (user.Key.Name != usersPacket.Users[0].Name)
                    {
                        NetworkStream stream = user.Value.GetStream();
                        byte[] buffer = packet.ToByte();
                        stream.Write(buffer, 0, buffer.Length);

                        Console.WriteLine("Sent: " + usersPacket.Users[0].Name +
                            " sent to " + user.Key.Name + ".");
                    }
                }
            }
        }

        #endregion Private Methods
    }
}