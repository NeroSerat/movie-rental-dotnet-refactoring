using System;

namespace MovieRental.Entities;

public class Customer
{
    public string Name { get; set; }

    public Customer(string name)
    {
        if (name == null)
            throw new NullReferenceException("Customer name cannot be null");

        Name = name.Trim();
    }
}