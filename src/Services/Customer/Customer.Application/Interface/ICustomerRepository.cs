using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.Interface
{
    public interface ICustomerRepository
    {
       Task AddAsync(Customer.Domain.Entities.Customer customer, CancellationToken cancellationToken = default);
       Task<Customer.Domain.Entities.Customer?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken = default);

       Task UpdateAsync(Customer.Domain.Entities.Customer customer, CancellationToken cancellationToken = default);
    }
}
