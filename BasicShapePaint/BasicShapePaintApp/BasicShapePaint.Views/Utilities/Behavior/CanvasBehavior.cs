namespace BasicShapePaint.Views.Utilities.Behavior
{
    using BasicShapePaint.Utilities.APIs;
    using System.Windows;
    using System.Windows.Interactivity;

    internal class CanvasBehavior : Behavior<FrameworkElement>
    {
        #region Protected Methods

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
            SubscribeToEvents();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;
        }

        #endregion Protected Methods

        #region Private Methods

        private void AssociatedObject_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (AssociatedObject.DataContext is IMouseEventHandlerVM handlerVM)
            {
                AssociatedObject.PreviewMouseLeftButtonDown +=
                    (s, e) => handlerVM.MouseDownEventHandler(ConvertMouseCoordinate(e));
                AssociatedObject.PreviewMouseMove +=
                    (s, e) => handlerVM.MouseMoveEventHandler(ConvertMouseCoordinate(e));
                AssociatedObject.PreviewMouseLeftButtonUp +=
                    (s, e) => handlerVM.MouseUpEventHandler(ConvertMouseCoordinate(e));
            }
        }

        private BasicShapePaint.Utilities.Point ConvertMouseCoordinate(System.Windows.Input.MouseEventArgs e)
        {
            return new BasicShapePaint.Utilities.Point(
                e.GetPosition(AssociatedObject).X, e.GetPosition(AssociatedObject).Y);
        }

        #endregion Private Methods
    }
}