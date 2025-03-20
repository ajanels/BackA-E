using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BackendAE.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("EmailSettings");
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Obtener configuración
            string smtpServer = _configuration["SmtpServer"];
            string portStr = _configuration["Port"];
            string senderEmail = _configuration["SenderEmail"];
            string senderPassword = _configuration["Password"];

            // Validar que los valores no sean nulos o vacíos
            if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(portStr) ||
                string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword))
            {
                throw new ArgumentException("Los parámetros del servidor de correo no están configurados correctamente.");
            }

            // Convertir el puerto a entero
            if (!int.TryParse(portStr, out int port))
            {
                throw new ArgumentException("El puerto SMTP no es válido.");
            }

            try
            {
                var smtpClient = new SmtpClient(smtpServer)
                {
                    Port = port,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, _configuration["SenderName"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                throw;
            }
        }
    }
}
