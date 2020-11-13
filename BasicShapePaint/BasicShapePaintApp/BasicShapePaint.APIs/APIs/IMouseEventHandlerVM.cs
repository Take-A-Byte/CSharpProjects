namespace BasicShapePaint.Utilities.APIs
{
    public interface IMouseEventHandlerVM
    {
        #region Public Methods

        void LeftMouseUpEventHandler(Point mouseCoordinate);

        void RightMouseUpEventHandler(Point mouseCoordinate);

        void MouseMoveEventHandler(Point mouseCoordinate);

        #endregion Public Methods
    }
}