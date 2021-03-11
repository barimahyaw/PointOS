//using System;
//using System.IO;
//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using eViSeM.Common.Enums;
//using eViSeM.Common.Helpers.IHelpers;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Options;

//namespace PointOS.Common.Helpers
//{
//    public class Utils : IUtils
//    {
//        private readonly ConnectionStrings _connectionStrings;
//        private readonly StaticFilesPath _staticFilesPath;
//        private readonly EmailSettings _emailSettings;
//        public Utils(IOptions<ConnectionStrings> connectionStrings, IOptions<StaticFilesPath> staticFilesPath, IOptions<EmailSettings> emailSettings)
//        {
//            _emailSettings = emailSettings.Value;
//            _staticFilesPath = staticFilesPath.Value;
//            _connectionStrings = connectionStrings.Value;
//        }
//        /// <summary>
//        /// Get Connection String From appSettings
//        /// </summary>
//        /// <returns>Connection to Db as string</returns>
//        public string GetConnectionString()
//        {
//            return _connectionStrings.AppConnectionString;
//        }

//        /// <summary>
//        /// Gets the path of the physical static executable file of an anti-virus
//        /// </summary>
//        /// <returns></returns>
//        public string GetAntiVirusPath()
//        {
//            return _staticFilesPath.AntiVirus;
//        }

//        /// <summary>
//        /// Upload documents/pictures and all kinds of files in a specify folder
//        /// </summary>
//        /// <param name="file"></param>
//        /// <param name="folder"></param>
//        /// <returns>Nothing</returns>
//        public async Task<string> UploadFile(IFormFile file, FileUploadFolder folder)
//        {
//            var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
//            var folderName = folder switch
//            {
//                FileUploadFolder.CompanyLogos => "CompanyLogos",
//                FileUploadFolder.Documents => "Documents",
//                FileUploadFolder.EmployeesPhoto => "EmployeesPhoto",
//                _ => string.Empty
//            };

//            var logosFolder = Path.Combine(_staticFilesPath.UploadsPath, folderName);
//            var filePath = Path.Combine(logosFolder, uniqueFileName);
//            await using var fileStream = new FileStream(filePath, FileMode.Create);
//            await file.CopyToAsync(fileStream);

//            return uniqueFileName;
//        }

//        /// <summary>
//        /// Generate unique Ticket numbers
//        /// </summary>
//        /// <returns></returns>
//        public string GenerateTicket()
//        {
//            var rnd = new Random();
//            var s1 = rnd.Next(000000, 999999);
//            var s2 = Convert.ToInt64(DateTime.Now.ToString("HHmmss"));
//            var s3 = s1 + "" + s2;
//            var count = s3.Length;

//            var result = s3;

//            if (count == 12) return result;

//            for (var i = 0; i < 12 - count; i++)
//            {
//                result = s3.Insert(count, i.ToString());
//                s3 = result;
//            }

//            return result;
//        }

//        /// <summary>
//        /// smtp email sender class
//        /// </summary>
//        /// <param name="emailAddress"></param>
//        /// <param name="subject"></param>
//        /// <param name="body"></param>
//        public void EmailSender(string emailAddress, string subject, string body)
//        {
//            var client = new SmtpClient
//            {
//                Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password),
//                Host = _emailSettings.SmtpHostName,
//                Port = _emailSettings.Port,
//                EnableSsl = true
//            };

//            var mailMessage = new MailMessage { From = new MailAddress(_emailSettings.SenderAddress) };

//            mailMessage.To.Add(emailAddress);
//            mailMessage.Subject = $"{_emailSettings.SenderName} - {subject}";
//            //mailMessage.CC.Add("");
//            //mailMessage.Bcc.Add("");
//            mailMessage.IsBodyHtml = true;

//            mailMessage.Body = body;

//            client.Send(mailMessage);
//        }
//    }
//}
