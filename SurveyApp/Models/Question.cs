using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp
{
    public class Question : INotifyPropertyChanged
    {
        private double question1;
        private double question2;
        private double question3;
        private double question4;
        private double question5;

        public double Question1
        {
            get { return question1; }
            set
            {
                question1 = value;
                OnPropertyChanged();
            }
        }
        public double Question2
        {
            get { return question2; }
            set
            {
                question2 = value; OnPropertyChanged();
            }
        }
        public double Question3
        {
            get { return question3; }
            set
            {
                question3 = value; OnPropertyChanged();
            }
        }
        public double Question4
        {
            get { return question4; }
            set
            {
                question4 = value; OnPropertyChanged();
            }
        }
        public double Question5
        {
            get { return question5; }
            set 
            { 
                question5 = value; OnPropertyChanged();
            }
        }

        public int TotalScore => (int)(Question1 + Question2 + Question3 + Question4 + Question5);

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}