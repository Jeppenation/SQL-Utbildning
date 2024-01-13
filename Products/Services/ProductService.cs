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
        /// <returns> Returns a string result value </returns>
        public string AddProduct(ProductEntity Entity)
        {

            using var conn = new SqlConnection(_connectionString);
            // Open the connection to the database
            conn.Open();

            // Adds the product to the database with SQL-code
            using var cmd = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM Products WHERE ArticleNumber = @ArticleNumber) BEGIN INSERT INTO Products VALUES(@ArticleNumber, @Title, @Description, @Price) SELECT 'Product inserted' AS Message END ELSE BEGIN SELECT 'Product already exists' AS MESSAGE END", conn);

            // Adds the parameters to the SQL-code
            cmd.Parameters.AddWithValue("@ArticleNumber", Entity.ArticleNumber);
            cmd.Parameters.AddWithValue("@Title", Entity.Title);
            cmd.Parameters.AddWithValue("@Description", Entity.Description ?? null);
            cmd.Parameters.AddWithValue("@Price", Entity.Price);

            // Executes the SQL-code and returns the number of rows affected. It just perform the action.
            //cmd.ExecuteNonQuery();

            //Execture the SQL-code and returns the first column of the first row in the result set returned by the query. In this case it returns the ID of the product that was added.
            //The same as doing 'SELECT SCOPE_IDENTITY()'
            //cmd.ExecuteScalar();

            
            //var result = cmd.ExecuteNonQuery();

            //Exectue the SQL-code and returns a value in the form of a object 
            var result = cmd.ExecuteScalar().ToString();
            


            return result!;
            
            

        }

        public IEnumerable<ProductEntity> GetAllProducts()
        {
            var products = new List<ProductEntity>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var cmd = new SqlCommand("SELECT * FROM Products", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
              products.Add(new ProductEntity()
              {
                  ArticleNumber = reader.GetString(0),
                  Title = reader.GetString(1),
                  Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                  Price = reader.GetDecimal(3)
              });
            }

            return products;
        }
    }
}
