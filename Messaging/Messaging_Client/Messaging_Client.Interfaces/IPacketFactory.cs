namespace Messaging_Client.PacketFactory
{
    using Messaging_Client.Interfaces;

    public interface IPacketFactory
    {
        #region Public Methods

        IPacket CreateMessagePacket(IMessage message);

        IPacket CreateUserPacket(string userName, IMessagingSocket socket);

        IPacket HandlePacket(byte[] buffer);

        #endregion Public Methods
    }
}