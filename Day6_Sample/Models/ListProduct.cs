namespace Day6_Sample.Models;

public class ListProduct
{
    public static List<Product> ListProducts;

    public static List<Product> GetInstance()
    {
        if (ListProducts is null) return ListProducts = Data.GetData();
        return ListProducts;
    }
}