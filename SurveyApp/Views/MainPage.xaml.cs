using SurveyApp.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace SurveyApp
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
        public void Button_ToResultPage(object sender, RoutedEventArgs e)
        {
            var mainViewModel = (MainViewModel)DataContext;
            var resultViewModel = new ResultViewModel(mainViewModel.CurrentQuestion);
            NavigationService.Navigate(new ResultPage()
            {
                DataContext = resultViewModel
            });
        }
    }
}
