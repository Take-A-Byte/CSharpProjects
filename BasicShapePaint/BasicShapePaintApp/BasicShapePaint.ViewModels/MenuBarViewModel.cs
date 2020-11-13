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

        #endregion Private Fields

        #region Public Constructors

        public MenuBarViewModel()
        {
            SelectedColor = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        }

        #endregion Public Constructors

        #region Public Properties

        public ShapeType SelectedShapeType
        {
            get => selectedShapeType;
            set
            {
                selectedShapeType = value;
                NotifyPropertyChanged(nameof(SelectedShapeType));
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
            return true;
        }

        #endregion Private Methods
    }
}