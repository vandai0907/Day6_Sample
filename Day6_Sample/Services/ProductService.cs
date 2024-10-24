using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Microsoft.Data.Sqlite;
using System.Text;

namespace Day6_Sample.Services;

public class ProductService : IProductService
{
    private readonly SqliteConnection _connection;

    public ProductService()
    {
        _connection = DatabaseService.GetConnection();
    }

    public List<Product> GetProducts()
    {
        _connection.Open();
        var sql = new StringBuilder();
        sql.AppendLine("SELECT");
        sql.AppendLine("    Name,");
        sql.AppendLine("    Description,");
        sql.AppendLine("    Price,");
        sql.AppendLine("    Like,");
        sql.AppendLine("    Person,");
        sql.AppendLine("    Image,");
        sql.AppendLine("    Quantity,");
        sql.AppendLine("    Id");
        sql.AppendLine("FROM");
        sql.AppendLine("    Products");
        sql.AppendLine("WHERE");
        sql.AppendLine("    Quantity > 0");
        var command = new SqliteCommand(sql.ToString(), _connection);
        var result = command.ExecuteReader();
        var products = new List<Product>();
        while (result.Read())
        {
            var product = new Product()
            {
                Name = result.GetString(0),
                Description = result.GetString(1),
                Price = result.GetInt32(2),
                Like = result.GetDouble(3),
                Person = result.GetInt32(4),
                Image = result.GetString(5),
                Quantity = result.GetInt32(6),
                Id = result.GetString(7)
            };
            products.Add(product);
        }
        _connection.Close();
        return products;
    }

    public Product GetProductById(int id)
    {
        _connection.Open();
        var sql = new StringBuilder();
        sql.AppendLine("SELECT");
        sql.AppendLine("    Name,");
        sql.AppendLine("    Description,");
        sql.AppendLine("    Price,");
        sql.AppendLine("    Like,");
        sql.AppendLine("    Person,");
        sql.AppendLine("    Image,");
        sql.AppendLine("    Quantity,");
        sql.AppendLine("    Id");
        sql.AppendLine("FROM");
        sql.AppendLine("    Products");
        sql.AppendLine("WHERE");
        sql.AppendLine("    Id = @Id");
        var command = _connection.CreateCommand();
        command.CommandText = sql.ToString();
        command.Parameters.AddWithValue("@Id", id);
        var result = command.ExecuteReader();
        var product = new Product();
        while (result.Read())
        {
            product = new Product()
            {
                Name = result.GetString(0),
                Description = result.GetString(1),
                Price = result.GetInt32(2),
                Like = result.GetDouble(3),
                Person = result.GetInt32(4),
                Image = result.GetString(5),
                Quantity = result.GetInt32(6),
                Id = result.GetString(7)
            };
        }
        _connection.Close();
        return product;
    }

    public void UpdateQuantity(Product product)
    {
        _connection.Open();
        var sql = new StringBuilder();
        sql.AppendLine("UPDATE");
        sql.AppendLine("    Products");
        sql.AppendLine("SET");
        sql.AppendLine("    Quantity = @Quantity");
        sql.AppendLine("WHERE");
        sql.AppendLine("    Id = @Id");
        var command = _connection.CreateCommand();
        command.CommandText = sql.ToString();
        command.Parameters.AddWithValue("@Quantity", product.Quantity);
        command.Parameters.AddWithValue("@Id", product.Id);
        command.ExecuteNonQuery();
        _connection.Close();
    }
}