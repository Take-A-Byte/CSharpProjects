namespace BasicShapePaint.Models
{
    public class Point
    {
        #region Public Constructors

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion Public Constructors

        #region Public Properties

        public float X { get; }

        public float Y { get; }

        #endregion Public Properties
    }
}