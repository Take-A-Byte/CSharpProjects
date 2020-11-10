namespace BasicShapePaint.Models
{
    public class Line : Shape
    {
        #region Public Constructors

        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            this.Position = new Point((endPoint.X - startPoint.X) / 2, (endPoint.Y - startPoint.Y) / 2);
        }

        #endregion Public Constructors

        #region Public Properties

        public Point StartPoint { get; }

        public Point EndPoint { get; }

        #endregion Public Properties
    }
}