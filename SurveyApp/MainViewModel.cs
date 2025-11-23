using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace SurveyApp
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Question currentQuestion;

        public MainViewModel()
        {
            CurrentQuestion = new Question();
        }

        public Question CurrentQuestion
        {
            get => currentQuestion;
            set
            {
                currentQuestion = value;
                OnPropertyChanged();
            }
        }
        public double Slider_1
        {
            get => CurrentQuestion.Question1;
            set
            {
                CurrentQuestion.Question1 = (int)value;
                OnPropertyChanged();
            }
        }
        public double Slider_2
        {
            get => CurrentQuestion.Question2;
            set
            {
                CurrentQuestion.Question2 = (int)value;
                OnPropertyChanged();
            }
        }
        public double Slider_3
        {
            get => CurrentQuestion.Question3;
            set
            {
                CurrentQuestion.Question3 = (int)value;
                OnPropertyChanged();
            }
        }
        public double Slider_4
        {
            get => CurrentQuestion.Question4;
            set
            {
                CurrentQuestion.Question4 = (int)value;
                OnPropertyChanged();
            }
        }
        public double Slider_5
        {
            get => CurrentQuestion.Question5;
            set
            {
                CurrentQuestion.Question5 = (int)value;
                OnPropertyChanged();
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