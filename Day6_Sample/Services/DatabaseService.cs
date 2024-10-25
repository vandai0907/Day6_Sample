using Microsoft.Data.Sqlite;
using System.IO;

namespace Day6_Sample.Services;

public class DatabaseService
{
    public SqliteConnection Connection { get; }

    public DatabaseService()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbpath = Path.Combine(folderPath, "sqliteSample.db");
        Connection = new SqliteConnection($"Filename={dbpath}");
    }
}