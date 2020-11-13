namespace BasicShapePaint.ViewModels
{
    using System;
    using System.Windows.Media;

    public partial class BaseViewModel
    {
        #region Protected Classes

        protected static class ViewModelMediator
        {
            #region Private Fields

            private static CanvasViewModel canvasVM;
            private static MenuBarViewModel menubarVM;

            #endregion Private Fields

            #region Public Events

            public delegate void SelectedShapeChanedEventHandler();

            public static event SelectedShapeChanedEventHandler SelectedShapeChanged;

            #endregion Public Events

            #region Public Properties

            public static ShapeType SelectedShapeType { get => menubarVM.SelectedShapeType; }
            public static Brush SelectedColor { get => menubarVM.SelectedColor; }

            #endregion Public Properties

            #region Internal Properties

            internal static CanvasViewModel CanvasVM
            {
                get => canvasVM;
                set
                {
                    canvasVM = value;
                }
            }

            internal static MenuBarViewModel MenubarVM
            {
                get => menubarVM;
                set
                {
                    if (menubarVM != null)
                    {
                        menubarVM.PropertyChanged -= MenubarVM_PropertyChanged;
                    }

                    menubarVM = value;
                    menubarVM.PropertyChanged += MenubarVM_PropertyChanged;
                }
            }

            #endregion Internal Properties

            #region Private Methods

            private static void MenubarVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(menubarVM.SelectedShapeType))
                {
                    SelectedShapeChanged?.Invoke();
                }
            }

            #endregion Private Methods
        }

        #endregion Protected Classes
    }
}