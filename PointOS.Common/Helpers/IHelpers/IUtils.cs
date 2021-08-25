using Microsoft.AspNetCore.Http;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using System.Threading.Tasks;

namespace PointOS.Common.Helpers.IHelpers
{
    public interface IUtils
    {
        /// <summary>
        /// Get Connection String From appSettings
        /// </summary>
        /// <returns>Connection to Db as string</returns>
        string GetConnectionString();

        /// <summary>
        /// Gets the path of the physical static executable file of an anti-virus
        /// </summary>
        /// <returns></returns>
        string GetAntiVirusPath();

        int GetUploadDocumentMaximumSize();

        /// <summary>
        /// Generate unique file name for uploaded documents
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Generated unique file name as a string</returns>
        string GetUniqueFileName(IFormFile file);

        /// <summary>
        /// Upload documents/pictures and all kinds of files in a specify folder
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns>Nothing</returns>
        Task<string> UploadFile(IFormFile file, FileUploadFolder folder);

        /// <summary>
        /// Gets documents/pictures and all kinds of files uploaded specify path directory
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        string FilePath(FileUploadFolder folder);

        /// <summary>
        /// Generate unique Ticket numbers
        /// </summary>
        /// <returns></returns>
        public string GenerateTransactionTicket();

        /// <summary>
        /// smtp email sender class
        /// </summary>
        /// <param name="request"></param>
        void EmailSender(EmailRequest request);
    }
}