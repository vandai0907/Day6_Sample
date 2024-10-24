using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.Text;

namespace Day6_Sample.Services
{
    class CartService : ICartService
    {
        private readonly SqliteConnection _connection;

        public CartService()
        {
            _connection = DatabaseService.GetConnection();
        }

        public void AddToCart(Product product)
        {
            var cartExist = GetCartById(int.Parse(product.Id));
            _connection.Open();
            var command = _connection.CreateCommand();
            var sql = new StringBuilder();
            if (cartExist.ProductId is null)
            {
                sql.AppendLine("INSERT INTO Carts (");
                sql.AppendLine("    ProductId,");
                sql.AppendLine("    ProductName,");
                sql.AppendLine("    Quantity,");
                sql.AppendLine("    Price,");
                sql.AppendLine("    Image)");
                sql.AppendLine("VALUES(");
                sql.AppendLine("    @ProductId,");
                sql.AppendLine("    @ProductName,");
                sql.AppendLine("    @Quantity,");
                sql.AppendLine("    @Price,");
                sql.AppendLine("    @Image)");

                command.CommandText = sql.ToString();
                command.Parameters.AddWithValue("@ProductId", product.Id);
                command.Parameters.AddWithValue("@ProductName", product.Name);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Image", product.Image);
            }
            else
            {
                UpdateCart(cartExist, product.Quantity + cartExist.Quantity);
                return;
            }
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void UpdateCart(Cart cart, int quantity)
        {
            _connection.Open();
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE");
            sql.AppendLine("    Carts");
            sql.AppendLine("SET");
            sql.AppendLine("    Quantity = @Quantity");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ProductId = @ProductId");
            var command = _connection.CreateCommand();
            command.CommandText = sql.ToString();
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@ProductId", cart.ProductId);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteCart(Cart cart)
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    Carts");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ProductId = @ProductId");
            command.CommandText = sql.ToString();
            command.Parameters.AddWithValue("@ProductId", cart.ProductId);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public ObservableCollection<Cart> GetCart()
        {
            _connection.Open();
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    ProductId,");
            sql.AppendLine("    ProductName,");
            sql.AppendLine("    Quantity,");
            sql.AppendLine("    Price,");
            sql.AppendLine("    Image");
            sql.AppendLine("FROM");
            sql.AppendLine("    Carts");
            var command = new SqliteCommand(sql.ToString(), _connection);
            var result = command.ExecuteReader();
            var carts = new ObservableCollection<Cart>();
            while (result.Read())
            {
                var cart = new Cart()
                {
                    ProductId = result.GetString(0),
                    ProductName = result.GetString(1),
                    Quantity = result.GetInt32(2),
                    Price = result.GetInt32(3),
                    Image = result.GetString(4),
                    TotalPrice = 1
                };
                carts.Add(cart);
            }
            _connection.Close();
            return carts;
        }

        public Cart GetCartById(int id)
        {
            _connection.Open();
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    ProductId,");
            sql.AppendLine("    ProductName,");
            sql.AppendLine("    Quantity,");
            sql.AppendLine("    Price,");
            sql.AppendLine("    Image");
            sql.AppendLine("FROM");
            sql.AppendLine("    Carts");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ProductId = @Id");

            var command = _connection.CreateCommand();
            command.CommandText = sql.ToString();
            command.Parameters.AddWithValue("@Id", id);
            var result = command.ExecuteReader();
            var cart = new Cart();
            while (result.Read())
            {
                cart = new Cart()
                {
                    ProductId = result.GetString(0),
                    ProductName = result.GetString(1),
                    Quantity = result.GetInt32(2),
                    Price = result.GetInt32(3),
                    Image = result.GetString(4),
                    TotalPrice = 1
                };
            }
            _connection.Close();
            return cart;
        }

        public void Order()
        {
            _connection.Open();
            var command = _connection.CreateCommand();
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    Carts");
            command.CommandText = sql.ToString();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public int GetTotalCart()
        {
            _connection.Open();
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("    COUNT(*)");
            sql.AppendLine("FROM");
            sql.AppendLine("    Carts");
            var command = new SqliteCommand(sql.ToString(), _connection);
            var result = command.ExecuteReader();
            var total = 0;
            while (result.Read())
            {
                total = result.GetInt32(0);
            }
            _connection.Close();
            return total;
        }
    }
}
