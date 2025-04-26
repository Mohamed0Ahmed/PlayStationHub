using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetOrCreateCustomerAsync(string phoneNumber, int branchId);
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Customer> GetCustomerByPhoneNumberAsync(string phoneNumber, int branchId);
    }
}