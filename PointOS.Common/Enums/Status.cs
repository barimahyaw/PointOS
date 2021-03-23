using PointOS.Common.Attributes;

namespace PointOS.Common.Enums
{
    public enum Status
    {
        /// <summary>
        /// 
        /// </summary>
        [StringValue("")]
        Ok = 200,
        /// <summary>
        /// A successful record created status
        /// </summary>
        [StringValue("The {0} record(s) created successfully.")]
        Created = 201,
        /// <summary>
        /// Successful Signed in status
        /// </summary>
        [StringValue("Welcome {0}. Kindly adhere to all the <b>COVID 19 Protocols</b> as you work on premise. Thank you.")]
        SignedIn = 202,
        ///// <summary>
        ///// 
        ///// </summary>
        //[StringValue("")]
        //Accepted = 202,
        /// <summary>
        /// A resource not found status
        /// </summary>
        [StringValue("There is/are no record(s) found for {0}")]
        NotFound = 404,
        /// <summary>
        /// Clock In Not Allowed
        /// </summary>
        [StringValue("Sorry you can not Clock In from out side the office premises.")]
        SignedInError = 405,
        /// <summary>
        /// Clock Out Not Allowed
        /// </summary>
        [StringValue("Sorry you can not Clock Out from out side the office premises.")]
        SignedOutError = 406
    }
}
