using Microsoft.Data.SqlClient;
using Products.Models.Entities;

namespace Products.Service
{
    public class ProductService(string connectionString)
    {

        private readonly string _connectionString = connectionString;

        ///<summary
        /// This method adds a product to the database if no product with the same name exists
        /// </Summary>
        /// <param name="entity"> The Product entity that will be added to the database </param>
        /// <returns> Returns true if the product was added successfully, false otherwise </returns>
        public bool AddProduct(ProductEntity Entity)
        {

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("INSERT INTO Products VALUES (@ArticleNumber, @Title, @Description, @Price)", conn);

            //1:02:30 i senaste videon

        }
    }
}
