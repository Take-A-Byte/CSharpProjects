namespace BasicShapePaint.Utilities
{
    public class Point
    {
        #region Public Constructors

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion Public Constructors

        #region Public Properties

        public double X { get; }

        public double Y { get; }

        #endregion Public Properties
    }
}