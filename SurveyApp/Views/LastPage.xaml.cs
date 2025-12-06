using SurveyApp.Views;
using System.Windows;
using System.Windows.Controls;


namespace SurveyApp
{
    public partial class LastPage : Page
    {
        public LastPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_ReturnToWelcomePage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            { mainWindow.NavigateToPage(new WelcomePage()); }
        }
    }
}
