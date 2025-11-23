using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace SurveyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyFrame.Navigate(new WelcomePage());
        }
        public void NavigateToPage(Page page)
        {
            MyFrame.Navigate(page);
        }
    }
}