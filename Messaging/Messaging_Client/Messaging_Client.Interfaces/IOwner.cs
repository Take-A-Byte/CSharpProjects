namespace Messaging_Client.Interfaces
{
    using System;

    public interface IOwner
    {
        #region Public Properties

        string Name { get; }

        string UserName { get; }

        #endregion Public Properties
    }
}