using System.Windows;
using System.Windows.Controls;
using SurveyApp.Views;

namespace SurveyApp
{
    public partial class ThxPage : Page
    {
        public ThxPage()
        {
            InitializeComponent();
        }

        private void Button_ReturnToWelcomePage(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow) 
            { mainWindow.NavigateToPage(new WelcomePage()); }
        }
    }
}
