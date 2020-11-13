namespace BasicShapePaint.Utilities.APIs
{
    public interface IMouseEventHandlerVM
    {
        #region Public Methods

        void MouseDownEventHandler(Point mouseCoordinate);

        void MouseUpEventHandler(Point mouseCoordinate);

        void MouseMoveEventHandler(Point mouseCoordinate);

        #endregion Public Methods
    }
}