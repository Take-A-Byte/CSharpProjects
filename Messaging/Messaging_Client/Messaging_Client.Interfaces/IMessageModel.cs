namespace Messaging_Client.Interfaces
{
    public interface IMessageModel
    {
        #region Public Properties

        string Message { get; }
        System.DateTime Time { get; }

        #endregion Public Properties
    }
}