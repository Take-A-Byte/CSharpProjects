namespace Messaging_Client.Models
{
    using Messaging_Client.Interfaces;

    public class MessageModel : IMessage
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

        public IOwner Owner { get; }

        public byte[] ToByte()
        {
            throw new System.NotImplementedException();
        }

        #endregion public Properties
    }
}