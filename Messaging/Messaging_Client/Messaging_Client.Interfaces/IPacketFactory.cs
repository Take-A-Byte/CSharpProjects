namespace Messaging_Client.PacketFactory
{
    using Messaging_Client.Interfaces;
    using System.Collections.Generic;
    using System.Net;

    public interface IPacketFactory
    {
        #region Public Methods

        IPacket CreateMessagePacket(IMessage message);

        IPacket CreateUserPacket(string userName, IPEndPoint loaclEndPoint);

        IPacket CreateUserPacket(List<IServiceUser> user);

        IPacket HandlePacket(byte[] buffer);

        #endregion Public Methods
    }
}