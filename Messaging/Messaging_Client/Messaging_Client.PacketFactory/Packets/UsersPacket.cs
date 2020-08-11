namespace Messaging.Models
{
    using Messaging_Client.Interfaces;
    using Messaging_Client.PacketFactory.Packets;
    using Messaging_Client.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class UsersPacket : IUsersPacket
    {
        #region Public Constructors

        public UsersPacket(IServiceUser user)
        {
            Users = new List<IServiceUser>();
            Users.Add(user);
        }

        public UsersPacket(List<IServiceUser> users)
        {
            Users = new List<IServiceUser>();
            Users.AddRange(users);
        }

        #endregion Public Constructors

        #region Public Properties

        public PacketType Type => PacketType.User;

        public List<IServiceUser> Users { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates User packet object from bute array
        /// </summary>
        /// <param name="buffer">recieved byte array</param>
        /// <returns>UsersPacket</returns>
        public static IPacket FromByte(byte[] buffer)
        {
            IPacket packet = null;

            if (buffer[0] == (byte)PacketType.User)
            {
                buffer = buffer.SubArray(1);
                try
                {
                    int byteIndex = 0;
                    do
                    {
                        int lenthOfName = buffer[0];
                        string name = Encoding.ASCII.GetString(buffer, 1, lenthOfName);
                        long ipAddress = BitConverter.ToInt64(buffer, lenthOfName + 1);
                        int port = BitConverter.ToInt32(buffer, lenthOfName + 9);

                        IServiceUser user = new User(name, new System.Net.IPEndPoint(ipAddress, port));
                        if (packet == null)
                        {
                            packet = new UsersPacket(user);
                        }
                        else
                        {
                            ((UsersPacket)packet).Users.Add(user);
                        }
                        buffer = buffer.SubArray(lenthOfName + 13);
                    }
                    while (byteIndex < buffer.Length);
                }
                catch
                {
                }
            }

            return packet;
        }

        public byte[] ToByte()
        {
            // Since we know that while sending the packet there will be only one user

            List<byte> buffer = new List<byte>();
            buffer.Add((byte)Type);
            buffer.Add(Users[0].LengthOfName);
            buffer.AddRange(Encoding.ASCII.GetBytes(Users[0].Name));
            buffer.AddRange(BitConverter.GetBytes(Users[0].MessagingSocket.IPAddress));
            buffer.AddRange(BitConverter.GetBytes(Users[0].MessagingSocket.PortNumber));
            return buffer.ToArray();
        }

        #endregion Public Methods
    }
}