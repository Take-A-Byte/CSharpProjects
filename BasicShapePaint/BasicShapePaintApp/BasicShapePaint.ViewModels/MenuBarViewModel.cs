using BasicShapePaint.ViewModels.Utilities;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace BasicShapePaint.ViewModels
{
    internal class MenuBarViewModel : BaseViewModel
    {
        #region Private Fields

        private ShapeType selectedShapeType;
        private ICommand chooseColorCommand;
        private Brush selectedColor;
        private bool drawing;
        private bool movingMode;

        #endregion Private Fields

        #region Public Constructors

        public MenuBarViewModel()
        {
            SelectedColor = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            MovingMode = false;
            ViewModelMediator.RegisterToViewModelEvent(ViewModelMediator.ViewModelEvent.DrawingStarted, DrawingStartedEventhandler);
            ViewModelMediator.RegisterToViewModelEvent(ViewModelMediator.ViewModelEvent.DrawingEnded, DrawingEndedEventhandler);
        }

        private void DrawingEndedEventhandler()
        {
            drawing = false;
        }

        private void DrawingStartedEventhandler()
        {
            drawing = true;
            MovingMode = false;
        }

        #endregion Public Constructors

        #region Public Properties

        public ShapeType SelectedShapeType
        {
            get => selectedShapeType;
            set
            {
                if (selectedShapeType != value)
                {
                    selectedShapeType = value;
                    ViewModelMediator.RaiseViewModelEvent(this, ViewModelMediator.ViewModelEvent.SelectedShapeChanged);
                    NotifyPropertyChanged(nameof(SelectedShapeType));
                }
            }
        }

        public Brush SelectedColor
        {
            get => selectedColor;
            set
            {
                selectedColor = value;
                NotifyPropertyChanged(nameof(SelectedColor));
            }
        }

        public ICommand ChooseColorCommand
        {
            get
            {
                if (chooseColorCommand == null)
                {
                    chooseColorCommand = new RelayCommand(ChooseColorCommandRequested, CanExecuteChooseColorCommand);
                }

                return chooseColorCommand;
            }
        }

        public bool MovingMode
        {
            get => movingMode;
            set
            {
                movingMode = value;
                NotifyPropertyChanged(nameof(MovingMode));
            }
        }

        #endregion Public Properties

        #region Private Methods

        private void ChooseColorCommandRequested()
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.FullOpen = true;
            colorDialog.SolidColorOnly = true;
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedColor = new SolidColorBrush(Color.FromRgb(
                    colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
            }
        }

        private bool CanExecuteChooseColorCommand()
        {
            return !drawing;
        }

        #endregion Private Methods
    }
}