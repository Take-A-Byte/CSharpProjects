using System.Drawing;

namespace BasicShapePaint.Models
{
    public abstract class Shape
    {
        #region Public Properties

        public Point Position { get; protected set; }

        public Color Color { get; protected set; }

        #endregion Public Properties
    }
}