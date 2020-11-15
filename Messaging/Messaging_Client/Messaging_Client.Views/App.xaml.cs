namespace Messaging_Client.Views
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Protected Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModels.BaseViewModel mainVM = new ViewModels.MainViewModel();

            MainWindow mainWin = new MainWindow();
            mainWin.DataContext = mainVM;

            mainWin.Show();
        }

        #endregion Protected Methods
    }
}