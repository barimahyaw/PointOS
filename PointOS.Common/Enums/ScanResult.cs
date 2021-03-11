using PointOS.Common.Attributes;

namespace eViSeM.Common.Enums
{
    /// <summary>  
    /// Result of the scan  
    /// </summary>  
    public enum ScanResult
    {
        /// <summary>  
        /// No threat was found  
        /// </summary>  
        [StringValue("No threat found")]
        NoThreatFound,

        /// <summary>  
        /// A threat was found  
        /// </summary>  
        [StringValue("Threat found. The file you uploaded contains threats and Operation cannot be proceeded.")]
        ThreatFound,

        /// <summary>  
        /// File not found  
        /// </summary>  
        [StringValue("The file could not be found")]
        FileNotFound,

        /// <summary>  
        /// The scan timed out  
        /// </summary>  
        [StringValue("Operation Timeout")]
        Timeout,

        /// <summary>  
        /// An error occured while scanning  
        /// </summary>  
        [StringValue("Operation Failed. Error trying to scan uploaded file. Please try again or contact system administrator.")]
        Error

    }
}
