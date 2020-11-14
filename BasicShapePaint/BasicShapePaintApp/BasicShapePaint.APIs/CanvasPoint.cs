namespace BasicShapePaint.Utilities
{
    using System.Windows;
    using System.Windows.Input;

    public class CanvasPoint
    {
        #region Private Fields

        private MouseEventArgs args;

        #endregion Private Fields

        #region Public Constructors

        public CanvasPoint(MouseEventArgs args, FrameworkElement canvas)
        {
            this.args = args;
            AbsolutePoint = new Point(args.GetPosition(canvas).X, args.GetPosition(canvas).Y);
        }

        #endregion Public Constructors

        #region Public Properties

        public Point AbsolutePoint { get; }

        #endregion Public Properties

        #region Public Methods

        public Point GetRelativePosition(FrameworkElement element)
        {
            return new Point(args.GetPosition(element).X, args.GetPosition(element).Y);
        }

        #endregion Public Methods
    }
}