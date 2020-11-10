using Messaging_Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Messaging_Client.PacketFactory
{
    internal class MessagingSocket : IMessagingSocket
    {
        #region Public Constructors

        public MessagingSocket(long ipAddress, int port)
        {
            IPAddress = ipAddress;
            PortNumber = port;
        }

        #endregion Public Constructors

        #region Public Properties

        public long IPAddress { get; }

        public int PortNumber { get; }

        #endregion Public Properties
    }
}