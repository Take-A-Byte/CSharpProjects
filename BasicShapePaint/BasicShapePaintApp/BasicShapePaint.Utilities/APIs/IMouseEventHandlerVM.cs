namespace BasicShapePaint.Utilities.APIs
{
    public interface IMouseEventHandlerVM
    {
        #region Public Methods

        void LeftMouseUpEventHandler(CanvasPoint mouseCoordinate);

        void LeftMouseDownEventHandler(CanvasPoint mouseCoordinate);

        void RightMouseUpEventHandler(CanvasPoint mouseCoordinate);

        void MouseMoveEventHandler(CanvasPoint mouseCoordinate);

        #endregion Public Methods
    }
}