namespace Messaging_Client.Interfaces
{
    public interface IMessage
    {
        #region Public Properties

        IOwner Owner { get; }

        string Message { get; }

        System.DateTime Time { get; }

        #endregion Public Properties

        #region Public Methods

        byte[] ToByte();

        #endregion Public Methods
    }
}