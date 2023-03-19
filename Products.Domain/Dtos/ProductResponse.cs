using Products.Domain.Models;

namespace Products.Domain.Dtos;

public class ProductResponse
{
    public List<Product> Products { get; set; } = new List<Product>();
    public int Pages { get; set; }
    public int CurrentPage { get; set; }

}