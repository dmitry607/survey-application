using SurveyApp.Views;
using SurveyApp.ViewModels;
using SurveyApp.Services;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
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
                MessageBox.Show($"ошибка { ex.Message}", "ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void Button_SendEmail(object sender, RoutedEventArgs e)
        {
            if(!await IsInternetAvaibleAsync()) { return; }

            try
            {
                string email = EmailTextBox.Text.Trim();
                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Заполните поле email", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (checkBox.IsChecked != true)
                {
                    MessageBox.Show("Необходимо согласиться с политикой конфиденциальности!","Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
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

                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    { mainWindow.NavigateToPage(new LastPage()); }

                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Ошибка при отправке email: {ex.Message}");
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

        public async Task<bool> IsInternetAvaibleAsync()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    var reply = await ping.SendPingAsync("8.8.8.8", 3000);
                    bool resume = reply.Status == IPStatus.Success;

                    if (!resume)
                    { MessageBox.Show("Сообщение не отправено. Проверьте подключение к интернету", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }

                    return resume;
                }
            }
            catch
            {
                MessageBox.Show("Сообщение не отправлено. Проверьте подключение к интернету", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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
