using Products.Domain.Dtos;
using Products.Domain.Filters;
using Products.Domain.Models;

namespace Products.Service;

public interface IProductService
{
    Task<List<Product>> Get(ProductFilter filter);
    Task<ProductResponse> Get(FilteredParameters filterParams, List<OrderByParameter> orderParams);
    Task<Product> Insert(Product product);
}