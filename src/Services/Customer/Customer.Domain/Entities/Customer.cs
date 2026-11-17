using Customer.Domain.Enums;
using Customer.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.Entities
{
    public sealed class Customer
    {
        private readonly List<Address> _address = [];

        public string Id { get; private set; }
        public string CustomerId { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public CustomerStatus Status { get; private set; }
        public IReadOnlyCollection<Address> Address => _address.AsReadOnly();
        public DateTime CreatedAt { get; private set; }

        private Customer() {
            Id = string.Empty;
            CustomerId = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        public Customer(string customerId, string firstName, string lastName, string email)
        {
            if(string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException("CustomerId cannot be null or empty.", nameof(customerId));
            }
            if(string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("FirstName cannot be null or empty.", nameof(firstName));
            }
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }
            Id = Guid.NewGuid().ToString();
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Status = CustomerStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddAddress(Address address)
        {
            ArgumentNullException.ThrowIfNull(address);
            _address.Add(address);
        }

        public void ChangeStatus(CustomerStatus customerStatus)
        {
            Status = customerStatus;
        }
    }
}
