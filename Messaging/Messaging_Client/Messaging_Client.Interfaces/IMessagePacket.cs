namespace Messaging_Client.Interfaces
{
    public interface IMessagePacket : IPacket
    {
        #region Public Properties

        IMessage Message { get; }

        #endregion Public Properties
    }
}