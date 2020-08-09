namespace Messaging_Client.PacketFactory.Packets
{
    using System;
    using System.Net;
    using Messaging_Client.Interfaces;

    internal class User : IServiceUser
    {
        #region Public Constructors

        public User(string name, IPEndPoint localEndPoint)
        {
            if (name.Length < 256)
            {
                LengthOfName = (byte)name.Length;
                Name = name;
                MessagingSocket = new MessagingSocket(localEndPoint.Address.Address, (uint)localEndPoint.Port);
            }
            else
            {
                throw new InsufficientMemoryException("Name of user should be less than 255 characters!");
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; }

        public IMessagingSocket MessagingSocket { get; }

        public byte LengthOfName { get; }

        #endregion Public Properties
    }
}