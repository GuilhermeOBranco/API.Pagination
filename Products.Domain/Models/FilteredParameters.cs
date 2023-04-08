namespace Products.Domain.Models;

public class FilteredParameters
{
    public int Page { get; set; }
    public float ItemsPerPage { get; set; }
    public string? Filter { get; set; }
}