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
            using (var smtpClient = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port))
            {
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Email, emailSettings.Password);

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(emailSettings.Email),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine($"email отправлен на {toEmail}");
            }
        }
        
        public async Task SendSurveyAsync(string toEmail)
        {
            string subject = "результаты опроса SberQ";
            string body = $"<h2>Вы прошли опрос</h2>";
            await SendEmailAsync(toEmail, subject, body);
        }

    }
}
