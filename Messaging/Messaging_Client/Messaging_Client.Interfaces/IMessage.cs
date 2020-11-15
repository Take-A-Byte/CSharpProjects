namespace Messaging_Client.Interfaces
{
    public interface IMessage
    {
        #region Public Properties

        IServiceUser User { get; }

        string MessageText { get; }

        System.DateTime Time { get; }

        #endregion Public Properties
    }
}