namespace Messaging_Client.Interfaces
{
    using UInt8 = System.Byte;

    public interface IServiceUser
    {
        #region Public Properties

        string Name { get; }

        UInt8 LengthOfName { get; }

        IMessagingSocket MessagingSocket { get; }

        #endregion Public Properties
    }
}