using BasicShapePaint.Models;
using BasicShapePaint.ViewModels.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BasicShapePaint.ViewModels
{
    public enum ShapeType
    {
        Line,
        Rectangle,
        Ellipse
    }

    public class MainViewModel : BaseViewModel
    {
        #region Private Fields

        private ICommand mouseDownCommand;

        #endregion Private Fields

        #region Private Constructors

        private MainViewModel()
        {
            Shapes = new ObservableCollection<Shape>();
        }

        #endregion Private Constructors

        #region Public Properties

        public ICommand MouseDownCommand
        {
            get
            {
                if (mouseDownCommand == null)
                {
                    mouseDownCommand = new RelayCommand(OnMouseDownCommandInvoked);
                }

                return mouseDownCommand;
            }
        }

        public ObservableCollection<Shape> Shapes { get; }

        public ShapeType SelectedShapeType { get; }

        #endregion Public Properties

        #region Private Methods

        private void OnMouseDownCommandInvoked()
        {
        }

        #endregion Private Methods
    }
}