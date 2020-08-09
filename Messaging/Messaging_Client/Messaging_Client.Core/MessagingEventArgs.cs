namespace Messaging.Core
{
    using System;

    internal class MessagingEventArgs
    {
        #region Public Classes

        public class ReceivedDataEventArgs : EventArgs
        {
            #region Public Constructors

            public ReceivedDataEventArgs(byte[] buffer)
            {
                Buffer = buffer;
            }

            #endregion Public Constructors

            #region Public Properties

            public byte[] Buffer { get; }

            #endregion Public Properties
        }

        #endregion Public Classes
    }
}