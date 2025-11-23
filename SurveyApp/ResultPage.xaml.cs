using System.Windows;
using System.Windows.Controls;


namespace SurveyApp
{
    /// <summary>
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public ResultPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        
        /*  --BUTTONS-- */
        private void Button_SendEmail(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            { mainWindow.NavigateToPage(new LastPage()); }
        }
        //public void Button_ToWelcomePage(object sender, RoutedEventArgs e)
        //{

        //}
        /* --HYPERLINKS--*/
        private void Open_Policy(object sender, RoutedEventArgs e)
        {
            var popupWindow1 = new PopupWindow1();
            popupWindow1.Owner = this;
            popupWindow1.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            popupWindow1.ShowDialog();
        }
        private void Open_Personal(object sender, RoutedEventArgs e)
        {

        }

        public static implicit operator Window(ResultPage v)
        {
            throw new NotImplementedException();
        }

        private void Button_Quit(object sender, RoutedEventArgs e)
        {

        }
        //private void checkBox_Checked(object sender, RoutedEventArgs e)
        //{

        //}


    }
}
