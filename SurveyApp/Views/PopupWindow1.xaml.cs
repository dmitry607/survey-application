using System.Windows;
using System.Windows.Input;


namespace SurveyApp.Views
{
    /// <summary>
    /// Логика взаимодействия для PopupWindow1.xaml
    /// </summary>
    public partial class PopupWindow1 : Window
    {
        public PopupWindow1()
        {
            InitializeComponent();
        }
        public void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
