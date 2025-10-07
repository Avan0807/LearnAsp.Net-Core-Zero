using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages;

public class ProductsModel : TestProjectPageModel
{
    private readonly IRepository<Product, Guid> _productRepository;

    public ProductsModel(IRepository<Product, Guid> productRepository)
    {
        _productRepository = productRepository;
    }

    public List<ProductDto> Products { get; set; }

    public async Task OnGetAsync()
    {
        var products = await _productRepository.GetListAsync();

        Products = products.Select(p => new ProductDto
        {
            Name = p.Name,
            Price = p.Price,
            Description = p.Description
        }).ToList();
    }
}

public class ProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}