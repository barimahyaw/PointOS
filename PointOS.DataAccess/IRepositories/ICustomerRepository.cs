﻿using PointOS.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOS.DataAccess.IRepositories
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Gets customer's id and names by phone number to populate customer drop down
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<Customer> FindAsync(string phoneNumber);

        /// <summary>
        /// Gets customers by their PhoneNumber like phoneNumber to populate customer auto complete
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<List<Customer>> FindAllContainPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// Gets customer's id and names by firstName or lastName to populate customer drop down
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        Task<List<Customer>> FindAsync(string firstName, string lastName);

        /// <summary>
        /// Gets customer's details by customer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Customer> FindAsync(int id);

        /// <summary>
        /// Gets customers' details by company Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<Customer>> FindAllAsync(int companyId, int skip, int take);

        /// <summary>
        /// Attach changes made to a Customer's record into repository
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task Add(Customer customer);

        /// <summary>
        /// Attach changes made to a customer's record into repository
        /// </summary>
        /// <param name="customer"></param>
        Task UpdateAsync(Customer customer);
    }
}