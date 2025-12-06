using System.Windows;
using System.Windows.Controls;
using SurveyApp.Views;

namespace SurveyApp
{
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
            DataContext = this;
        }
        public void Button_ToMainPage(object sender, RoutedEventArgs e)
        {
            if(Application.Current.MainWindow is MainWindow mainWindow)
            { mainWindow.NavigateToPage(new MainPage()); }
        }
    }
}
