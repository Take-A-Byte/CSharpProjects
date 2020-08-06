namespace Messaging_Client.Interfaces
{
    public interface IPacket
    {
        #region Public Properties

        PacketType Type { get; }

        #endregion Public Properties

        #region Public Methods

        byte[] ToByte();

        bool FromByte(byte[] buffer);

        #endregion Public Methods
    }
}