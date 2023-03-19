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

    public async Task<ProductResponse> Get(int page,float itemsPerPage, string orderBy, bool descending)
    {
        double pageCount = Math.Ceiling(await _context.Product.CountAsync() / itemsPerPage);

        var products = await _context.Product
            .CustomOrderBy(orderBy, descending)
            .Skip((page - 1) * (int)itemsPerPage)
            .Take((int)itemsPerPage)
            .ToListAsync();

        return new ProductResponse { Products = products, CurrentPage = page, Pages = (int)pageCount };
    }

    public async Task<Product> Insert(Product product)
    {
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
}