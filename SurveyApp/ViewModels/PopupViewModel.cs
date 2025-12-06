using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

namespace SurveyApp.ViewModels
{
    public class PopupViewModel : INotifyPropertyChanged
    {
        public PopupViewModel()
        {
            LoadPolicyText();
            LoadPersonalText();
        }

        private string _policyText;
        public string PolicyText
        {
            get => _policyText;
            private set
            {
                _policyText = value;
                OnPropertyChanged();
            }
        }

        private string _personalText;
        public string PersonalText
        {
            get => _personalText;
            private set
            {
                _personalText = value;
                OnPropertyChanged();
            }
        }

        private void LoadPolicyText()
        {
            try
            {
                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(exeDir, "texts", "Policy.txt");

                if (File.Exists(filePath))
                {
                    PolicyText = File.ReadAllText(filePath);
                }
                else
                {
                    PolicyText = "файл Policy.txt не найден по пути:\n" + filePath;
                }
            }
            catch (Exception ex)
            {
                PolicyText = $"ошибка при загрузке файла:\n{ex.Message}";
            }
        }
        private void LoadPersonalText()
        {
            try
            {
                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                string filePathPerson = Path.Combine(exeDir, "texts", "Personal.txt");
                if (File.Exists(filePathPerson))
                {
                    PersonalText = File.ReadAllText(filePathPerson);
                }
                else
                {
                    PersonalText = "файл Personal.txt не найден по пути\n" + filePathPerson;
                }
            }
            
            catch (Exception ex)
                {
                    PersonalText = $"ошибка при загрузке файла: \n{ex.Message}";
                }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
    