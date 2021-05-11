using eViSeM.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace PointOS.Common.Helpers.IHelpers
{
    public interface IScanner
    {
        /// <summary>  
        /// Scan a single file  
        /// </summary>  
        /// <param name="file">The file to scan</param>  
        /// <param name="timeoutInMs">The maximum time in milliseconds to take for this scan</param>  
        /// <returns>The scan result</returns>  
        ScanResult Scan(IFormFile file, int timeoutInMs = 30000);
    }
}