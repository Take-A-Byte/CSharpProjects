namespace BasicShapePaint.Views.Utilities.Behavior
{
    using BasicShapePaint.APIs;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    internal class CanvasBehavior : Behavior<Canvas>
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
                AssociatedObject.PreviewMouseLeftButtonDown += handlerVM.MouseDownEventHandler;
                AssociatedObject.PreviewMouseMove += handlerVM.MouseMoveEventHandler;
                AssociatedObject.PreviewMouseLeftButtonUp += handlerVM.MouseUpEventHandler;
            }
        }

        #endregion Private Methods
    }
}