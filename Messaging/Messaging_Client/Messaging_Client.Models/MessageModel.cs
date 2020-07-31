namespace Messaging_Client.Models
{
    using Messaging_Client.Interfaces;

    public class MessageModel : IMessageModel
    {
        #region Public Constructors

        public MessageModel(string message)
        {
            Message = message;
            Time = System.DateTime.Now;
        }

        #endregion Public Constructors

        #region public Properties

        public string Message { get; }

        public System.DateTime Time { get; }

        #endregion public Properties
    }
}