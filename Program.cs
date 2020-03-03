using System;
using System.Data.SqlClient;
using Dapper;

namespace Lab2
{
    //https://dapper-tutorial.net/dapper
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using var connection = new SqlConnection(connectionString); //dodanie dappera i MySQclient ten drugi do połączenia z bazą
           
            var region = new Region() { RegionDescription = "dapper obiekt", RegionId=101 };

            var db = new DB();
            db.Select(connection);
            db.Insert(connection, region);
            db.Delete(connection, 101);
            db.SelectOrder(connection, 10290);
        }
    }
}
