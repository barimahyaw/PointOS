﻿using eViSeM.Common.Enums;
using Microsoft.AspNetCore.Http;
using PointOS.Common.DTO.Request;
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

        /// <summary>
        /// Upload documents/pictures and all kinds of files in a specify folder
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folder"></param>
        /// <returns>Nothing</returns>
        Task<string> UploadFile(IFormFile file, FileUploadFolder folder);

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