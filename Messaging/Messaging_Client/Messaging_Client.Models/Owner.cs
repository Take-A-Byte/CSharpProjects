namespace Messaging.Models
{
    using Messaging_Client.Interfaces;

    internal class Owner : IOwner
    {
        #region Public Constructors

        public Owner(string name, string userName)
        {
            Name = name;
            UserName = userName;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; }

        public string UserName { get; }

        #endregion Public Properties
    }
}