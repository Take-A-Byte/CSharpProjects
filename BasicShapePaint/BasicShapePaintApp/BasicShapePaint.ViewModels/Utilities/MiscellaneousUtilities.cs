namespace BasicShapePaint.ViewModels.Utilities
{
    using System;

    public static class MiscellaneousUtilities
    {
        #region Public Delegates

        public delegate void EmptyEventHandler();

        public class DrawingEndedEventArgs : EventArgs
        {
            #region Public Constructors

            public DrawingEndedEventArgs(bool completed)
            {
                Completed = completed;
            }

            #endregion Public Constructors

            #region Public Properties

            public bool Completed { get; }

            #endregion Public Properties
        }

        #endregion Public Delegates
    }
}