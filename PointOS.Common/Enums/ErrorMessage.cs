using PointOS.Common.Attributes;

namespace PointOS.Common.Enums
{
    public enum ErrorMessage
    {
        /// <summary>
        /// Expired login session message
        /// </summary>
        [StringValue("Sorry, your login session has expired. Please try to login again and repeat the action.")]
        LoginSessionExpired,
        /// <summary>
        /// Unauthorized action message
        /// </summary>
        [StringValue("Sorry, you are not authorized to perform {0} action. Contact system administrator.")]
        UnauthorizedAction,
        /// <summary>
        /// File Size Exceeds limit
        /// </summary>
        [StringValue("The size of the file exceeds limit. Uploaded documents should not be more than 300KB.")]
        FileSizeExceeded
    }
}
