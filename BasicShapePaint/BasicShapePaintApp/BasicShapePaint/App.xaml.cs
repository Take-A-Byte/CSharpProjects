namespace BasicShapePaint
{
    using System.Windows;
    using BasicShapePaint.ViewModels;
    using BasicShapePaint.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Protected Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainViewModel mainVM = new MainViewModel();
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = mainVM;
            mainWindow.Show();
        }

        #endregion Protected Methods
    }
}