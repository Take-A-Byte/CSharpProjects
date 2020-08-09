using Messaging.Models;
using Messaging_Client.Interfaces;
using Messaging_Client.PacketFactory.Packets;
using System.Net;

namespace Messaging_Client.PacketFactory
{
    public class PacketFactory : IPacketFactory
    {
        #region Public Methods

        public IPacket CreateMessagePacket(IMessage message)
        {
            return null;
        }

        public IPacket CreateUserPacket(string userName, IPEndPoint localEndPoint)
        {
            return new UsersPacket(new User(userName, localEndPoint));
        }

        public IPacket HandlePacket(byte[] buffer)
        {
            IPacket packet = null;
            if (buffer.Length > 0)
            {
                try
                {
                    if (buffer[0] == (byte)PacketType.User)
                    {
                        packet = UsersPacket.FromByte(buffer);
                    }
                    else if (buffer[0] == (byte)PacketType.Message)
                    {
                    }
                }
                catch
                {
                }
            }

            return packet;
        }

        #endregion Public Methods
    }
}