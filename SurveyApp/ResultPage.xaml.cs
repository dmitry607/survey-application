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

        private void Button_SendEmail(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkBox.IsChecked==true)
                {
                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    { mainWindow.NavigateToPage(new LastPage()); }
                }
                else
                {
                    MessageBox.Show("Подтвердите выбор!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"произошла ошибка {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Open_Policy(object sender, RoutedEventArgs e)
        {
            var popupWindow1 = new PopupWindow1();
            popupWindow1.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            popupWindow1.ShowDialog();
        }
        private void Open_Personal(object sender, RoutedEventArgs e)
        {
            var popupWindow2 = new PopupWindow2();
            popupWindow2.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            popupWindow2.ShowDialog();
        }
        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
                { mainWindow.NavigateToPage(new ThxPage()); }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
