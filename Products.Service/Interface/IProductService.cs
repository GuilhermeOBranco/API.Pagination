using Products.Domain.Dtos;
using Products.Domain.Filters;
using Products.Domain.Models;

namespace Products.Service;

public interface IProductService
{
    Task<List<Product>> Get(ProductFilter filter);
    Task<ProductResponse> Get(int page, float itemsPerPage, string orderBy, bool descending = true);
    Task<Product> Insert(Product product);
}