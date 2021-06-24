using PointOS.Common.DTO.Sessions;

namespace PointOS.Common.DTO.Response
{
    public class ResponseHeader
    {
        /// <summary>
        /// Reference returned from operation
        /// </summary>
        public string ReferenceNumber { get; set; }
        /// <summary>
        /// True or False whether operation is successful or not
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// Message from operation response
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Operation response code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Any Simple/Complex data
        /// </summary>
        public UserSession Data { get; set; }
    }
}
