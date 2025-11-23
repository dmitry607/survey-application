using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SurveyApp
{
    class ResultViewModel : INotifyPropertyChanged
    {
        private Question currentQuestion;
        private int totalScore;
        private string grade;

        public ResultViewModel(Question question)
        {
            CurrentQuestion = question ?? new Question();
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

        private string GradeScore(int score)
        {
            if (score <= 7) { return "0-2"; }
            if (score >= 12) { return "7-10"; }
            else return "3-6";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}