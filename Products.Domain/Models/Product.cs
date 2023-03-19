namespace Products.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int StockAmount { get; set; }
}