using Microsoft.Data.Sqlite;
using System.IO;

namespace Day6_Sample.Services;

public class DatabaseService
{
    public static SqliteConnection GetConnection()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbpath = Path.Combine(folderPath, "sqliteSample.db");
        return new SqliteConnection($"Filename={dbpath}");
    }
}