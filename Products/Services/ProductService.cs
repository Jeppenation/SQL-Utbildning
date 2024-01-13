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
            

            // Returns the result, if the result is null it will return null
            return result!;
            
            

        }

        public IEnumerable<ProductEntity> GetAllProducts()
        {

            // Creates a list of products
            var products = new List<ProductEntity>();

            // Opens the connection to the database
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            // Gets all the products from the database
            using var cmd = new SqlCommand("SELECT * FROM Products", conn);

            // Reads the data from the database
            using var reader = cmd.ExecuteReader();

            //While there is data to read, this code will run
            while (reader.Read())
            {
                // Adds the data to the list of products
              products.Add(new ProductEntity()
              {
                  // Gets the data from the database and adds it to the list of products
                  ArticleNumber = reader.GetString(0),
                  Title = reader.GetString(1),
                  Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                  Price = reader.GetDecimal(3)
              });
            }

            return products;
        }

        public ProductEntity GetOneProduct(string articleNumber)
        {
            // Creates a list of products
            var productEntity = new ProductEntity();

            // Opens the connection to the database
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            // Gets all the products from the database
            using var cmd = new SqlCommand("IF EXISTS (SELECT 1 FROM Products WHERE ArticleNumber = @ArticleNumber) SELECT * FROM Products WHERE ArticleNumber = @ArticleNumber ELSE SELECT 'Product does not exist exists' AS Message \r\n", conn);

            // Adds the parameter to the SQL-code
            cmd.Parameters.AddWithValue("@ArticleNumber", articleNumber);

            // Reads the data from the database
            using var reader = cmd.ExecuteReader();

            //While there is data to read, this code will run
            while (reader.Read())
            {
                if (reader.GetString(0) == "Product does not exist exists")
                {
                    return null!;
                }
                else
                {
                    // Adds data to the variabel productEntity
                    productEntity = new ProductEntity()
                    {
                        // Gets the data from the database and adds it to the list of products
                        ArticleNumber = reader.GetString(0),
                        Title = reader.GetString(1),
                        Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Price = reader.GetDecimal(3)
                    };
                }

                
             
            }

            return productEntity;
        }
    }
}
