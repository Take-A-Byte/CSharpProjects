namespace BasicShapePaint.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new BasicShapePaint.ViewModels.MainViewModel();
        }

        #endregion Public Constructors
    }
}