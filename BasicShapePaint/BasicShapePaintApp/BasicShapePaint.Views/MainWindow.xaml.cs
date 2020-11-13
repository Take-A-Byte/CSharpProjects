namespace BasicShapePaint.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new BasicShapePaint.ViewModels.MainViewModel();
        }

        #endregion Public Constructors

        // Rotated rectangle
        //private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var canvas = sender as Canvas;
        //    Rectangle rect;
        //    if (startPoint == null)
        //    {
        //        rect = new Rectangle();
        //        startPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //        rect.Width = 0;
        //        rect.Height = 0;
        //        rect.StrokeThickness = 2;
        //        rect.Stroke = new SolidColorBrush(Colors.Red);
        //        rect.RenderTransform = new TranslateTransform(startPoint.X, startPoint.Y);
        //        canvas.Children.Add(rect);
        //    }
        //    else if (secondPoint == null)
        //    {
        //        rect = (canvas.Children[0] as Rectangle);
        //        secondPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //        var angle = Math.Atan((secondPoint.Y - startPoint.Y) / (secondPoint.X - startPoint.X));
        //        var rotate = new RotateTransform(angle * 180 / 3.14);
        //        rotate.CenterX = (startPoint.X + secondPoint.X) / 2;
        //        rotate.CenterY = (startPoint.Y + secondPoint.Y) / 2;
        //        rect.RenderTransform = rotate;
        //    }
        //    else
        //    {
        //        drawn = true;
        //        rect = (canvas.Children[0] as Rectangle);
        //    }
        //}

        //private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    var canvas = sender as Canvas;
        //    if (canvas.Children.Count != 0)
        //    {
        //        var rect = (canvas.Children[0] as Rectangle);

        //        if (secondPoint != null && !drawn)
        //        {
        //            var xDiff = secondPoint.X - startPoint.X;
        //            var yDiff = secondPoint.Y - startPoint.Y;
        //            rect.Width = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

        //            var m = yDiff / xDiff;
        //            var c = startPoint.Y - m * startPoint.X;
        //            var tempHeight = (m * e.GetPosition(canvas).X - e.GetPosition(canvas).Y + c)
        //                 / Math.Sqrt(Math.Pow(m, 2) + 1);
        //            rect.Height = Math.Abs(tempHeight);

        //            Transform rectifyTranslate = Transform.Identity;
        //            Transform rectifyWidthTranslate = new TranslateTransform(-rect.Width / 2, 0);
        //            Transform translate = Transform.Identity;
        //            TransformGroup tansformGroup;
        //            if (!(rect.RenderTransform is TransformGroup existingGroup))
        //            {
        //                translate = new TranslateTransform(
        //                      (startPoint.X + secondPoint.X) / 2, (startPoint.Y + secondPoint.Y) / 2);
        //                tansformGroup = new TransformGroup();
        //                tansformGroup.Children.Add(rectifyTranslate);
        //                tansformGroup.Children.Add(rectifyWidthTranslate);
        //                tansformGroup.Children.Add(translate);
        //                tansformGroup.Children.Add(rect.RenderTransform);
        //                rect.RenderTransform = tansformGroup;
        //            }

        //            tansformGroup = rect.RenderTransform as TransformGroup;
        //            if (tempHeight > 0)
        //            {
        //                tansformGroup.Children[0] = new TranslateTransform(0, -tempHeight);
        //            }
        //        }
        //    }
        //}

        // Rotated ellipse
        //private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var canvas = sender as Canvas;
        //    Ellipse ellipse;
        //    if (startPoint == null)
        //    {
        //        ellipse = new Ellipse();
        //        startPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //        ellipse.Width = 0;
        //        ellipse.Height = 0;
        //        ellipse.StrokeThickness = 2;
        //        ellipse.Stroke = new SolidColorBrush(Colors.Red);
        //        ellipse.RenderTransform = new TranslateTransform(startPoint.X, startPoint.Y);
        //        canvas.Children.Add(ellipse);
        //    }
        //    else if (secondPoint == null)
        //    {
        //        ellipse = (canvas.Children[0] as Ellipse);
        //        secondPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //        var angle = Math.Atan((secondPoint.Y - startPoint.Y) / (secondPoint.X - startPoint.X));
        //        var rotate = new RotateTransform(angle * 180 / 3.14);
        //        rotate.CenterX = (startPoint.X + secondPoint.X) / 2;
        //        rotate.CenterY = (startPoint.Y + secondPoint.Y) / 2;
        //        ellipse.RenderTransform = rotate;
        //    }
        //    else
        //    {
        //        drawn = true;
        //        ellipse = (canvas.Children[0] as Ellipse);
        //    }
        //}

        //private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    var canvas = sender as Canvas;
        //    if (canvas.Children.Count != 0)
        //    {
        //        var ellipse = (canvas.Children[0] as Ellipse);

        //        if (secondPoint != null && !drawn)
        //        {
        //            var xDiff = secondPoint.X - startPoint.X;
        //            var yDiff = secondPoint.Y - startPoint.Y;
        //            ellipse.Width = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

        //            var m = yDiff / xDiff;
        //            var c = startPoint.Y - m * startPoint.X;
        //            var tempHalfHeight = (m * e.GetPosition(canvas).X - e.GetPosition(canvas).Y + c)
        //                 / Math.Sqrt(Math.Pow(m, 2) + 1);
        //            ellipse.Height = Math.Abs(tempHalfHeight) * 2;

        //            Transform rectifyTranslate = Transform.Identity;
        //            Transform rectifyWidthTranslate = new TranslateTransform(-ellipse.Width / 2, 0);
        //            Transform translate = Transform.Identity;
        //            TransformGroup tansformGroup;
        //            if (!(ellipse.RenderTransform is TransformGroup existingGroup))
        //            {
        //                translate = new TranslateTransform(
        //                      (startPoint.X + secondPoint.X) / 2, (startPoint.Y + secondPoint.Y) / 2);
        //                tansformGroup = new TransformGroup();
        //                tansformGroup.Children.Add(rectifyTranslate);
        //                tansformGroup.Children.Add(rectifyWidthTranslate);
        //                tansformGroup.Children.Add(translate);
        //                tansformGroup.Children.Add(ellipse.RenderTransform);
        //                ellipse.RenderTransform = tansformGroup;
        //            }

        //            tansformGroup = ellipse.RenderTransform as TransformGroup;
        //            if (tempHalfHeight > 0)
        //            {
        //                tansformGroup.Children[0] = new TranslateTransform(0, -tempHalfHeight);
        //            }
        //            else
        //            {
        //                tansformGroup.Children[0] = new TranslateTransform(0, tempHalfHeight);
        //            }
        //        }
        //    }
        //}

        // Ellipse
        //private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var canvas = sender as Canvas;
        //    Ellipse ellipse;
        //    if (startPoint == null)
        //    {
        //        ellipse = new Ellipse();
        //        startPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //        ellipse.Width = 0;
        //        ellipse.Height = 0;
        //        ellipse.StrokeThickness = 2;
        //        ellipse.Stroke = new SolidColorBrush(Colors.Red);
        //        ellipse.RenderTransform = new TranslateTransform(startPoint.X, startPoint.Y);
        //        canvas.Children.Add(ellipse);
        //    }
        //    else
        //    {
        //        ellipse = (canvas.Children[0] as Ellipse);
        //    }
        //}

        //private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    var canvas = sender as Canvas;
        //    if (canvas.Children.Count != 0)
        //    {
        //        var ellipse = (canvas.Children[0] as Ellipse);

        //        if (e.GetPosition(canvas).X - startPoint.X < 0
        //            && e.GetPosition(canvas).Y - startPoint.Y < 0)
        //        {
        //            ellipse.RenderTransform = new TranslateTransform(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //            ellipse.Height = startPoint.Y - e.GetPosition(canvas).Y;
        //            ellipse.Width = startPoint.X - e.GetPosition(canvas).X;
        //        }
        //        else
        //        {
        //            if (e.GetPosition(canvas).X - startPoint.X > 0)
        //                ellipse.Width = e.GetPosition(canvas).X - startPoint.X;
        //            else
        //            {
        //                ellipse.RenderTransform = new TranslateTransform(e.GetPosition(canvas).X, startPoint.Y);
        //                ellipse.Width = startPoint.X - e.GetPosition(canvas).X;
        //            }

        //            if (e.GetPosition(canvas).Y - startPoint.Y > 0)
        //                ellipse.Height = e.GetPosition(canvas).Y - startPoint.Y;
        //            else
        //            {
        //                ellipse.RenderTransform = new TranslateTransform(startPoint.X, e.GetPosition(canvas).Y);
        //                ellipse.Height = startPoint.Y - e.GetPosition(canvas).Y;
        //            }
        //        }
        //    }
        //}

        // rectangle
        //private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var canvas = sender as Canvas;
        //    Rectangle rect;
        //    if (startPoint == null)
        //    {
        //        rect = new Rectangle();
        //        startPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //        rect.Width = 0;
        //        rect.Height = 0;
        //        rect.StrokeThickness = 2;
        //        rect.Stroke = new SolidColorBrush(Colors.Red);
        //        rect.RenderTransform = new TranslateTransform(startPoint.X, startPoint.Y);
        //        canvas.Children.Add(rect);
        //    }
        //    else
        //    {
        //        rect = (canvas.Children[0] as Rectangle);
        //    }
        //}

        //private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    var canvas = sender as Canvas;
        //    if (canvas.Children.Count != 0)
        //    {
        //        var rect = (canvas.Children[0] as Rectangle);

        //        if (e.GetPosition(canvas).X - startPoint.X < 0
        //            && e.GetPosition(canvas).Y - startPoint.Y < 0)
        //        {
        //            rect.RenderTransform = new TranslateTransform(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //            rect.Height = startPoint.Y - e.GetPosition(canvas).Y;
        //            rect.Width = startPoint.X - e.GetPosition(canvas).X;
        //        }
        //        else
        //        {
        //            if (e.GetPosition(canvas).X - startPoint.X > 0)
        //                rect.Width = e.GetPosition(canvas).X - startPoint.X;
        //            else
        //            {
        //                rect.RenderTransform = new TranslateTransform(e.GetPosition(canvas).X, startPoint.Y);
        //                rect.Width = startPoint.X - e.GetPosition(canvas).X;
        //            }

        //            if (e.GetPosition(canvas).Y - startPoint.Y > 0)
        //                rect.Height = e.GetPosition(canvas).Y - startPoint.Y;
        //            else
        //            {
        //                rect.RenderTransform = new TranslateTransform(startPoint.X, e.GetPosition(canvas).Y);
        //                rect.Height = startPoint.Y - e.GetPosition(canvas).Y;
        //            }
        //        }
        //    }
        //}

        // Line
        //private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var canvas = sender as Canvas;

        //    System.Windows.Shapes.Line line;
        //    if (canvas.Children.Count == 0)
        //    {
        //        line = new System.Windows.Shapes.Line();
        //        line.StrokeThickness = 2;
        //        line.Stroke = new SolidColorBrush(Colors.Red);
        //        canvas.Children.Add(line);
        //    }
        //    else
        //    {
        //        line = canvas.Children[0] as System.Windows.Shapes.Line;
        //    }

        //    if (startPoint == null)
        //    {
        //        startPoint = new Models.Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
        //        line.X1 = e.GetPosition(canvas).X;
        //        line.Y1 = e.GetPosition(canvas).Y;
        //    }
        //    else
        //    {
        //        startPoint = null;
        //    }

        //    line.X2 = e.GetPosition(canvas).X;
        //    line.Y2 = e.GetPosition(canvas).Y;
        //}

        //private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    if (startPoint != null)
        //    {
        //        var canvas = sender as Canvas;
        //        (canvas.Children[0] as System.Windows.Shapes.Line).X2 = e.GetPosition(canvas).X;
        //        (canvas.Children[0] as System.Windows.Shapes.Line).Y2 = e.GetPosition(canvas).Y;
        //    }
        //}
    }
}