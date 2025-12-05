using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SurveyApp
{
    internal class EmailService
    {
        private EmailSettings emailSettings;
        private bool isInitialized = false;
        public async Task LoadEmailSettingsAsync()
        {
            
            try {
                string filePath = @"C:\dotnet\SurveyApp\settings.json";
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("файл настроек не найден", filePath);
                }
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    emailSettings = await JsonSerializer.DeserializeAsync<EmailSettings>(fs);
                }
                if (emailSettings == null)
                {
                    throw new InvalidOperationException("не удалось загрузить настройки email");
                }
                isInitialized = true;
            }
            catch (Exception ex) { 
                Console.WriteLine($"Ошибка загрузки настроек: {ex.Message}");
                throw;
            }
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            if (!isInitialized)
            {
                throw new InvalidOperationException("сервис email не инициализирован");
            }
            using (var Client = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port))
            {
                Client.EnableSsl = true;
                Client.Credentials = new NetworkCredential(emailSettings.Email, emailSettings.Password);

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(emailSettings.Email),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);
                await Client.SendMailAsync(mailMessage);
            }
        }
        public async Task<string> ReadTextAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"{filePath} Not Found:");
                }
                return await File.ReadAllTextAsync(filePath, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка чтения файла {filePath}, {ex.Message}");
                throw;
            }
        }
        public async Task SendSurveyAsync(string toEmail, string grade)
        {
            string rec = RecFilePath(grade);
            string recText = await ReadTextAsync(rec);

            string subject = "результаты опроса SberQ";
            string body = @$"
            <h2>результаты опроса</h2>
            <p> ваш результат {grade} баллов</p>
            <p>{recText}</p>";

            await SendEmailAsync(toEmail, subject, body);
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
    }
}
