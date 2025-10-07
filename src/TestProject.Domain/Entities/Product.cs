using System;
using Volo.Abp.Domain.Entities;

namespace TestProject.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    protected Product()
    {
    }

    public Product(Guid id, string name, decimal price) : base(id)
    {
        Name = name;
        Price = price;
        Stock = 0;
    }
}