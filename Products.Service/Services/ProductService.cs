using Microsoft.EntityFrameworkCore;
using Products.Domain.Dtos;
using Products.Domain.Filters;
using Products.Domain.Models;
using Products.Service.Data;
using Products.Service.Extensions;

namespace Products.Service.Services;

public class ProductService : IProductService
{
    private readonly ProductContext _context;

    public ProductService(ProductContext context)
    {
        _context = context;
    }

    public Task<List<Product>> Get(ProductFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductResponse> Get(FilteredParameters filterParams, List<OrderByParameter> orderParams)
    {
        double pageCount = Math.Ceiling(await _context.Product.CountAsync() / filterParams.ItemsPerPage);
        Console.WriteLine(_context.Product
            .Skip((filterParams.Page - 1) * (int)filterParams.ItemsPerPage)
            .Take((int)filterParams.ItemsPerPage)
            .OrderBy(x => x.Id)
            .CustomOrderBy(orderParams).ToQueryString());
        var products = _context.Product
            .Skip((filterParams.Page - 1) * (int)filterParams.ItemsPerPage)
            .Take((int)filterParams.ItemsPerPage)
            .OrderBy(x => x.Id)
            .CustomOrderBy(orderParams)
            .ToList();

        return new ProductResponse { Products = products, CurrentPage = filterParams.Page, Pages = (int)pageCount };
    }

    public async Task<Product> Insert(Product product)
    {
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
}