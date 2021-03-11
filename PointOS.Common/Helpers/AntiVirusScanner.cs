using System;
using System.Diagnostics;
using System.IO;
using eViSeM.Common.Enums;
using eViSeM.Common.Helpers.IHelpers;
using Microsoft.AspNetCore.Http;

namespace PointOS.Common.Helpers
{
    public class AntiVirusScanner : IScanner
    {
        private readonly string _mpcmdrunLocation;

        public AntiVirusScanner(string mpcmdrunLocation)
        {
            if (!File.Exists(mpcmdrunLocation))
            {
                throw new FileNotFoundException();
            }

            _mpcmdrunLocation = new FileInfo(mpcmdrunLocation).FullName;
        }
        /// <summary>  
        /// Scan a single file  
        /// </summary>  
        /// <param name="file">The file to scan</param>  
        /// <param name="timeoutInMs">The maximum time in milliseconds to take for this scan</param>  
        /// <returns>The scan result</returns>  
        public ScanResult Scan(IFormFile file, int timeoutInMs = 30000)
        {
            if (!(file.Length > 0)) return ScanResult.FileNotFound;
            var documentPathRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //var rootOfCurrentPath = Path.GetPathRoot(Environment.CurrentDirectory);
            //var driveWhereWindowsIsInstalled = Path.GetPathRoot(Environment.SystemDirectory);
            var uploadsFolder = Path.Combine(documentPathRoot, "FilesToScan");
            var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            //var fileInfo = file.Name/*new FileInfo(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"'))*/;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            //using var document = new Document(filePath);

            //var ms = new MemoryStream();

            //document.SaveToStream(ms, FileFormat.PDF);
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fs);
            }

            //file.CopyTo(new FileStream(filePath, FileMode.Create));

            ScanResult result;
            using (var process = new Process())
            {
                var startInfo = new ProcessStartInfo(_mpcmdrunLocation)
                {
                    Arguments = $"-Scan -ScanType 3 -File \"{filePath}\" -DisableRemediation",
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false
                };

                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit(timeoutInMs);

                result = ScanResult.Timeout;

                if (process.HasExited)
                    result = process.ExitCode switch
                    {
                        0 => ScanResult.NoThreatFound,
                        2 => ScanResult.ThreatFound,
                        _ => ScanResult.Error
                    };

                process.Kill();
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return result;
        }
    }
}
