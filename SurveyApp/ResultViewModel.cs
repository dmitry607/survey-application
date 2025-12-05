using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace SurveyApp
{
    class ResultViewModel : INotifyPropertyChanged
    {
        private Question currentQuestion;
        private int totalScore;
        private string grade;
        private string recDescription;
        

        public ResultViewModel(Question question)
        {
            CurrentQuestion = question ?? new Question();
            Grade = GradeScore(CurrentQuestion.TotalScore);
            TotalScore = CurrentQuestion.TotalScore;
        }

        public Question CurrentQuestion
        {
            get => currentQuestion;
            set
            {
                currentQuestion = value;
                OnPropertyChanged();
                Grade = GradeScore(value?.TotalScore ?? 0);
                LoadRecText();
            }
        }
        public string Grade
        {
            get => grade;
            set
            {
                grade = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ResultText));
                LoadRecText();
            }
        }
        public string RecDescription
        {
            get => recDescription;
            set
            {
                recDescription = value;
                OnPropertyChanged();
            }
        }
        public string ResultText => $"Ваш результат {Grade} баллов";
        public int TotalScore
        {
            get => totalScore;
            set
            {
                totalScore = value;
                OnPropertyChanged();
                Grade = GradeScore(value);
            }
        }
        private async void LoadRecText()
        {
            try
            {
                var emailService = new EmailService();
                string recFile = RecFilePath(Grade);
                RecDescription = await emailService.ReadTextAsync(recFile);

            }
            catch (Exception ex) {
                MessageBox.Show($"ошибка загрузки текстов {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private string RecFilePath(string grade)
        {
            return grade switch
            {
                "0-2" => "C:\\dotnet\\SurveyApp\\SurveyApp\\texts\\rec0_2.txt",
                "3-6" => "C:\\dotnet\\SurveyApp\\SurveyApp\\texts\\rec3_6.txt",
                "7-10" => @"C:\dotnet\SurveyApp\SurveyApp\texts\rec7_10.txt"
            };
        }
        private string GradeScore(int score)
        {
            if (score <= 7) { return "0-2"; }
            if (score >= 12) { return "7-10"; }
            else return "3-6";
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}