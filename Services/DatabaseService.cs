using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace FilterStockTools.Services
{
    public class DatabaseService
    {
        private readonly string connectionString = "Data Source=Data/financial_data.db";

        public DatabaseService()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Organizations (Symbol TEXT PRIMARY KEY, ResponseJson TEXT, FetchedAt TEXT);
                CREATE TABLE IF NOT EXISTS BalanceSheets (Symbol TEXT PRIMARY KEY, ResponseJson TEXT, FetchedAt TEXT);
                CREATE TABLE IF NOT EXISTS IncomeStatements (Symbol TEXT PRIMARY KEY, ResponseJson TEXT, FetchedAt TEXT);
            ";
            cmd.ExecuteNonQuery();
        }

        public void SaveJson(string table, string symbol, string json)
        {
            var conn = new SqliteConnection(connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
                INSERT OR REPLACE INTO {table} (Symbol, ResponseJson, FetchedAt)
                VALUES (@symbol, @json, @time);";
            cmd.Parameters.AddWithValue("@symbol", symbol);
            cmd.Parameters.AddWithValue("@json", json);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("s"));
            cmd.ExecuteNonQuery();
        }

        public DataTable GetOrganizationTable()
        {
            var dt = new DataTable();
            var conn = new SqliteConnection(connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Symbol, FetchedAt FROM Organizations ORDER BY Symbol";
            var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }
    }
}
