using PointOS.DataAccess;

namespace PointOS.BusinessLogic
{
    public class CustomerBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CustomerBusiness(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        ///// <summary>
        ///// Gets customer's id and names by phone number to populate customer drop down
        ///// </summary>
        ///// <param name="phoneNumber"></param>
        ///// <returns></returns>
        //public async Task<Customer> FindAsync(string phoneNumber)
        //{

        //}
    }
}
