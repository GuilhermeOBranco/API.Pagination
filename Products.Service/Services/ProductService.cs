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

    public async Task<ProductResponse> Get(FilteredParameters filterParams, List<OrderByParameter>? orderParams = null)
    {
        IQueryable<Product> dbQuery = _context.Product;

        if (!String.IsNullOrEmpty(filterParams.Filter))
            dbQuery = dbQuery.Where(x => x.Name == filterParams.Filter);

        if (orderParams != null)
            dbQuery = dbQuery.CustomOrderBy(orderParams);

        double pageCount = Math.Ceiling(dbQuery.Count() / filterParams.ItemsPerPage);
        
        dbQuery = dbQuery.Skip((filterParams.Page - 1) * (int)filterParams.ItemsPerPage)
            .Take((int)filterParams.ItemsPerPage);

        List<Product> products = dbQuery.ToList();
        
        
        return new ProductResponse { Products = products, CurrentPage = filterParams.Page, Pages = (int)pageCount };
    }

    public async Task<Product> Insert(Product product)
    {
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
}