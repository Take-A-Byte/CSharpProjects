namespace BasicShapePaint.Views.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows.Data;
    using BasicShapePaint.Utilities;

    internal class ShapeTypeToIconConverter : IValueConverter
    {
        #region Private Fields

        private IReadOnlyDictionary<ShapeType, string> iconSources = new ReadOnlyDictionary<ShapeType, string>(
            new Dictionary<ShapeType, string>()
            {
                {ShapeType.Line, "Icons/line.png" },
                {ShapeType.Rectangle, "Icons/rectangle.png" },
                {ShapeType.Ellipse, "Icons/ellipse.png" },
                {ShapeType.FreeHand, "Icons/pen.png" },
            });

        #endregion Private Fields

        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ShapeType shapeType)
            {
                return iconSources[shapeType];
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion Public Methods
    }
}