namespace Day6_Sample.Models;

public class ListProduct
{
    public static List<Product> ListProducts;

    public Product SelectedProduct { get; set; }
    public ListProduct(Product product = null)
    {
        SelectedProduct = product;
    }

    public static List<Product> GetInstance()
    {
        if (ListProducts is null) return ListProducts = Data.GetData();
        return ListProducts;
    }
}