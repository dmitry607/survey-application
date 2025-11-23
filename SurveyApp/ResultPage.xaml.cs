
using System.Windows;
using System.Windows.Controls;

namespace SurveyApp
{
    public partial class ResultPage : Page
    {
        private EmailService emailService;
        public ResultPage()
        {
            InitializeComponent();
            DataContext = this;
            InitializeEmailService();
        }
        private async void InitializeEmailService()
        {
            try
            {
                emailService = new EmailService();
                await emailService.LoadEmailSettingsAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"ошибка { ex.Message}", "лшибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void Button_SendEmail(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = EmailTextBox.Text.Trim();
                if (checkBox.IsChecked != true)
                {
                    if (string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Заполните поле email", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                if (!ValidEmail(email))
                {
                    MessageBox.Show("Введите корректный email", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }

                if (emailService == null)
                {
                    MessageBox.Show("Сервис отправки email не доступен",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    var viewModel = (ResultViewModel)DataContext;
                    await emailService.SendSurveyAsync(email, viewModel.Grade);

                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Ошибка при отправке email {ex.Message}");
                }

                if (Application.Current.MainWindow is MainWindow mainWindow)
                { mainWindow.NavigateToPage(new LastPage()); }

                else
                {
                    MessageBox.Show("Подтвердите выбор!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"произошла ошибка при отправке: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool ValidEmail(string email)
        {
            try
            {
                var emmail = new System.Net.Mail.MailAddress(email);
                return emmail.Address == email;
            }
            catch { return false; }
            /*
            try
            {
                string temp = "@mail.ru";
                var em = email.Substring(email.Length - 8);
                return em == temp;
            }
            catch { return false; }
            */
        }

        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            { mainWindow.NavigateToPage(new ThxPage()); }
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
        

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
