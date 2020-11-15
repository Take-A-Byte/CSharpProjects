namespace Messaging_Client.Interfaces
{
    using System.Collections.Generic;

    public interface IUsersPacket : IPacket
    {
        #region Public Properties

        List<IServiceUser> Users { get; }

        #endregion Public Properties
    }
}