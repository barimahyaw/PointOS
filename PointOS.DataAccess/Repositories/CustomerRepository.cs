using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;
using PointOS.DataAccess.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOS.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="dbContext"></param>
        public CustomerRepository(AppDbContext dbContext) => _dbContext = dbContext;

        /// <summary>
        ///  Gets Customers as queryable with no tracking of changes
        /// </summary>
        /// <returns></returns>
        private IQueryable<Customer> GetQueryable()
            => _dbContext.Customers.AsNoTrackingWithIdentityResolution();

        /// <summary>
        /// Gets customer's id and names by phone number to populate customer drop down
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<Customer> FindAsync(string phoneNumber)
            => await GetQueryable()
                .Select(c => new Customer
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    MiddleName = c.MiddleName,
                    LastName = c.LastName
                })
                .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);

        /// <summary>
        /// Gets customer's id and names by firstName or lastName to populate customer drop down
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public async Task<List<Customer>> FindAsync(string firstName, string lastName)
            => await GetQueryable()
                .Where(c => c.FirstName == firstName || c.LastName == lastName)
                .Select(c => new Customer
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    MiddleName = c.MiddleName,
                    LastName = c.LastName
                }).ToListAsync();


        /// <summary>
        /// Gets customer's details by customer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> FindAsync(int id)
            => await GetQueryable().FirstOrDefaultAsync(c => c.Id == id);

        /// <summary>
        /// Attach changes made to a Customer's record into repository
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task Add(Customer customer) => await _dbContext.Customers.AddAsync(customer);

        /// <summary>
        /// Attach changes made to a customer's record into repository
        /// </summary>
        /// <param name="customer"></param>
        public Task UpdateAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            return Task.FromResult(0);
        }
    }
}
