using Day6_Sample.Models;

namespace Day6_Sample.Interfaces;

public interface IProductService
{
    List<Product> GetProducts();
    Product GetProductById(int id);
    void UpdateQuantity(Product product);
}