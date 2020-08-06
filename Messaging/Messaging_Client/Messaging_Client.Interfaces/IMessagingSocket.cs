namespace Messaging_Client.Interfaces
{
    public interface IMessagingSocket
    {
        #region Public Properties

        /// <summary>
        /// The long value of the IP address. For example, the value 0x2414188f in big-endian
        /// format would be the IP address "143.24.20.36".
        /// </summary>
        long IPAddress { get; }

        /// <summary>
        /// The port number of the messaging socket.
        /// </summary>
        uint PortNumber { get; }

        #endregion Public Properties
    }
}