namespace BasicShapePaint.APIs
{
    public interface IMouseEventHandlerVM
    {
        #region Public Methods

        void MouseDownEventHandler(object sender, System.Windows.Input.MouseButtonEventArgs e);

        void MouseUpEventHandler(object sender, System.Windows.Input.MouseButtonEventArgs e);

        void MouseMoveEventHandler(object sender, System.Windows.Input.MouseEventArgs e);

        #endregion Public Methods
    }
}