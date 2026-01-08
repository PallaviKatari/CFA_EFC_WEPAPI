using CFA_EFC_WEPAPI.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

public class ProductRepository
{
    // ADO.NET approach
    // ADO.NET is a set of classes that expose data access services for .NET Framework programmers
    // ADO.NET stands for ActiveX Data Objects .NET
    // Classes like SqlConnection, SqlCommand, SqlDataReader are part of ADO.NET
    // SqlConnection: Represents a connection to a SQL Server database
    private readonly string _connectionString;

    //Accessing the connection string from appsettings.json
    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // 🔹 Get All Products
    public List<Product> GetAllProducts()
    {
        List<Product> products = new();

        //SQL Connection - accessing the database - DefaultConnection
        SqlConnection con = new(_connectionString);
        //Sql Command - SQL Query to be executed
        //2 parameters: SQL Query and SQL Connection
        using SqlCommand cmd = new("SELECT * FROM Product", con);

        //Internally it will open SqlConnection, execute the command and close the connection
        con.Open();
        //Click of Execute in Sql Server Management Studio will be performed by ExecuteReader method
        using SqlDataReader reader = cmd.ExecuteReader(); // The records that are returned from the database from Product Table
        //Read the records one by one from reader
        //Add the records to products list using Add method
        while (reader.Read())
        {
            products.Add(new Product
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()!,
                Price = Convert.ToDecimal(reader["Price"]),
            });
        }

        return products;
    }

    // 🔹 Get Product By Id
    public Product? GetProductById(int id) //1
    {
        Product? product = null;

        using SqlConnection con = new(_connectionString);
        //Retrive the product based on the id
        //table variable @id=1
        //Select * from Product where Id=1
        using SqlCommand cmd = new("SELECT * FROM Product WHERE Id=@id", con);
        //Assign the value to the parameter
        cmd.Parameters.AddWithValue("@id", id);
        con.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            product = new Product
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()!,
                Price = Convert.ToDecimal(reader["Price"]),
            };
        }

        return product;
    }
}
