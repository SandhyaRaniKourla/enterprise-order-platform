using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Domain.ValueObjects
{
    public sealed record Address(string Type, string street, string City, string State, string PostalCode, string Country);
}
