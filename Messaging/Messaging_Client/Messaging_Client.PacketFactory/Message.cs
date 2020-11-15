namespace Messaging_Client.Utilities
{
    using Messaging_Client.Interfaces;

    public class Message : IMessage
    {
        #region Public Constructors

        public Message(string message)
        {
            MessageText = message;
            Time = System.DateTime.Now;
        }

        #endregion Public Constructors

        #region public Properties

        public string MessageText { get; }

        public System.DateTime Time { get; }

        public IServiceUser User { get; }

        #endregion public Properties
    }
}