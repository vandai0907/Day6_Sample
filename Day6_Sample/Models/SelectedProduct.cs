namespace Day6_Sample.Models;

public class SelectedProduct
{
    public Product Product { get; set; }

    public SelectedProduct(Product product)
    {
        Product = product;
    }
}