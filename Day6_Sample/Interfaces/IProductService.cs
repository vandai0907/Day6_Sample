using Day6_Sample.Models;

namespace Day6_Sample.Interfaces;

public interface IProductService
{
    List<Product> GetProducts();
    List<Product> GetProductLiked();
    Product GetProductById(int id);
    void UpdateQuantity(Product product);
    void UpdateProductLiked(Product product);
}