using Microsoft.Data.Sqlite;
using System.IO;
using System.Text;

namespace Day6_Sample.Models;

public static class Data
{


    public static void InitializeDatabase()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbpath = Path.Combine(folderPath, "sqliteSample.db");
        using var db = new SqliteConnection($"Filename={dbpath}");
        db.Open();
        var sqlQuery = new StringBuilder();
        sqlQuery.AppendLine("CREATE TABLE IF NOT ");
        sqlQuery.AppendLine("EXISTS Products (Id INTEGER, ");
        sqlQuery.AppendLine("Name NVARCHAR(2048) NULL,");
        sqlQuery.AppendLine("Description NVARCHAR(2048) NULL,");
        sqlQuery.AppendLine("Price INTEGER, ");
        sqlQuery.AppendLine("Like DECIMAL,");
        sqlQuery.AppendLine("Person INTEGER,");
        sqlQuery.AppendLine("Image NVARCHAR(2048) NULL,");
        sqlQuery.AppendLine("IsLiked INTEGER,");
        sqlQuery.AppendLine("Quantity INTEGER");
        sqlQuery.AppendLine(")");
        var createTable = new SqliteCommand(sqlQuery.ToString(), db);

        createTable.ExecuteReader();
    }

    public static void Init()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbpath = Path.Combine(folderPath, "sqliteSample.db");
        using var db = new SqliteConnection($"Filename={dbpath}");
        db.Open();
        var sqlQuery = new StringBuilder();
        sqlQuery.AppendLine("CREATE TABLE IF NOT ");
        sqlQuery.AppendLine("EXISTS Carts (");
        sqlQuery.AppendLine("ProductId INTEGER,");
        sqlQuery.AppendLine("ProductName NVARCHAR(2048) NULL,");
        sqlQuery.AppendLine("Quantity INTEGER, ");
        sqlQuery.AppendLine("Price DECIMAL,");
        sqlQuery.AppendLine("Image NVARCHAR(2048) NULL");
        sqlQuery.AppendLine(")");
        var createTable = new SqliteCommand(sqlQuery.ToString(), db);
        createTable.ExecuteReader();
    }

    public static void InitImage()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbpath = Path.Combine(folderPath, "sqliteSample.db");
        using var db = new SqliteConnection($"Filename={dbpath}");
        db.Open();
        var sqlQuery = new StringBuilder();
        sqlQuery.AppendLine("CREATE TABLE IF NOT ");
        sqlQuery.AppendLine("EXISTS Images (");
        sqlQuery.AppendLine("ProductId INTEGER,");
        sqlQuery.AppendLine("Image NVARCHAR(2048) NULL");
        sqlQuery.AppendLine(")");
        var createTable = new SqliteCommand(sqlQuery.ToString(), db);
        createTable.ExecuteReader();
    }

    public static void AddData(Product product)
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var dbpath = Path.Combine(folderPath, "sqliteSample.db");
        using var db = new SqliteConnection($"Filename={dbpath}");
        db.Open();

        var insertCommand = new SqliteCommand();
        insertCommand.Connection = db;

        // Use parameterized query to prevent SQL injection attacks
        insertCommand.CommandText = "INSERT INTO Products VALUES (@Id, @Name, @Description, @Price, @Like, @Person, @Image, @IsLiked, @Quantity);";
        insertCommand.Parameters.AddWithValue("@Id", int.Parse(product.Id));
        insertCommand.Parameters.AddWithValue("@Name", product.Name);
        insertCommand.Parameters.AddWithValue("@Description", product.Description);
        insertCommand.Parameters.AddWithValue("@Price", product.Price);
        insertCommand.Parameters.AddWithValue("@Like", product.Like);
        insertCommand.Parameters.AddWithValue("@Person", product.Person);
        insertCommand.Parameters.AddWithValue("@Image", product.Image);
        insertCommand.Parameters.AddWithValue("@IsLiked", product.IsLiked);
        insertCommand.Parameters.AddWithValue("@Quantity", product.Quantity);

        insertCommand.ExecuteReader();
    }

    public static List<Product> GetData()
    {
        var entries = new List<Product>();
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbpath = Path.Combine(folderPath, "sqliteSample.db");
        using var db = new SqliteConnection($"Filename={dbpath}");
        db.Open();
        var selectCommand = new SqliteCommand
            ("SELECT Name, Description, Price, Like, Person, Image, Quantity, Id, IsLiked from Products", db);

        SqliteDataReader query = selectCommand.ExecuteReader();

        while (query.Read())
        {
            var product = new Product()
            {
                Name = query.GetString(0),
                Description = query.GetString(1),
                Price = query.GetInt32(2),
                Like = query.GetDouble(3),
                Person = query.GetInt32(4),
                Image = query.GetString(5),
                Quantity = query.GetInt32(6),
                Id = query.GetString(7),
                IsLiked = query.GetInt32(8) == 1,
            };
            entries.Add(product);
        }
        return entries;
    }
}