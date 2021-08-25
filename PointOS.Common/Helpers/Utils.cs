using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Common.Helpers.IHelpers;
using PointOS.Common.Settings;
using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PointOS.Common.Helpers
{
    public class Utils : IUtils
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly StaticFilesPath _staticFilesPath;
        private readonly EmailSettings _emailSettings;
        private readonly UploadDocumentSettings _uploadDocumentSettings;
        public Utils(IOptions<ConnectionStrings> connectionStrings, IOptions<StaticFilesPath> staticFilesPath, IOptions<EmailSettings> emailSettings, IOptions<UploadDocumentSettings> uploadDocumentSettings)
        {
            _uploadDocumentSettings = uploadDocumentSettings.Value;
            _emailSettings = emailSettings.Value;
            _staticFilesPath = staticFilesPath.Value;
            _connectionStrings = connectionStrings.Value;
        }

        /// <summary>
        /// Get Connection String From appSettings
        /// </summary>
        /// <returns>Connection to Db as string</returns>
        public string GetConnectionString() => _connectionStrings.AppConnectionString;

        /// <summary>
        /// Gets the path of the physical static executable file of an anti-virus
        /// </summary>
        /// <returns></returns>
        public string GetAntiVirusPath() => _staticFilesPath.AntiVirus;

        public int GetUploadDocumentMaximumSize() => _uploadDocumentSettings.MaximumSize;

        /// <summary>
        /// Generate unique file name for uploaded documents
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Generated unique file name as a string</returns>
        public string GetUniqueFileName(IFormFile file) => $"{Guid.NewGuid()}_{file.FileName}";

        /// <summary>
        /// Upload documents/pictures and all kinds of files in a specify folder
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns>Nothing</returns>
        public async Task<string> UploadFile(IFormFile file, FileUploadFolder folder)
        {
            //var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            var uniqueFileName = GetUniqueFileName(file);

            var folderName = folder switch
            {
                FileUploadFolder.CompanyLogos => "Companies",
                FileUploadFolder.Documents => "Documents",
                FileUploadFolder.EmployeesPhoto => "Employees",
                FileUploadFolder.ProductsPhoto => "Products",
                _ => string.Empty
            };

            var logosFolder = Path.Combine(_staticFilesPath.UploadsPath, folderName);
            var filePath = Path.Combine(logosFolder, uniqueFileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return uniqueFileName;
        }

        /// <summary>
        /// Gets documents/pictures and all kinds of files uploaded specify path directory
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public string FilePath(FileUploadFolder folder)
        {
            var folderName = folder switch
            {
                FileUploadFolder.CompanyLogos => "Companies",
                FileUploadFolder.Documents => "Documents",
                FileUploadFolder.EmployeesPhoto => "Employees",
                FileUploadFolder.ProductsPhoto => "Products",
                _ => string.Empty
            };

            var path = Path.Combine(_staticFilesPath.UploadsPath, folderName);

            return path;
        }

        /// <summary>
        /// Generate unique Ticket numbers
        /// </summary>
        /// <returns></returns>
        public string GenerateTransactionTicket()
        {
            var rnd = new Random();
            var s1 = rnd.Next(000000, 999999);
            var s2 = Convert.ToInt64(DateTime.Now.ToString("HHmmss"));
            var s3 = s1 + "" + s2;
            var count = s3.Length;

            var result = s3;

            if (count == 12) return result;

            for (var i = 0; i < 12 - count; i++)
            {
                result = s3.Insert(count, i.ToString());
                s3 = result;
            }

            return result;
        }

        /// <summary>
        /// smtp email sender class
        /// </summary>
        /// <param name="request"></param>
        public void EmailSender(EmailRequest request)
        {
            //var host = _emailSettings.SmtpHostName;
            var client = new SmtpClient
            {
                //Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password),
                Host = _emailSettings.SmtpHostName,
                Port = _emailSettings.Port,
                //EnableSsl = true
            };

            var mailMessage = new MailMessage { From = new MailAddress(_emailSettings.SenderAddress) };

            mailMessage.To.Add(request.EmailAddress);
            mailMessage.Subject = $"{_emailSettings.SenderName} - {request.Subject}";
            //mailMessage.CC.Add("");
            //mailMessage.Bcc.Add("");
            mailMessage.IsBodyHtml = true;

            mailMessage.Body = request.Body;

            client.Send(mailMessage);
        }
    }
}
