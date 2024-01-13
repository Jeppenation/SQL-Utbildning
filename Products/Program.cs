using Products.Models.Entities;
using Products.Service;

//Instantiate the ProductService with the connection string to the database
var productService = new ProductService(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hwila\source\repos\SQL\Products\Data\ProductDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

//Instantiate a ProductEntity with the values that will be added to the database
var productEntity = new ProductEntity()
{
    ArticleNumber = "A5",
    Title = "Test",
    Description = "asd",
    Price = 100
};

//Add the product to the database
var result = productService.AddProduct(productEntity);
//Prints the result
Console.WriteLine(result);

foreach (var product in productService.GetAllProducts())
{
    Console.WriteLine(product.ArticleNumber);
    Console.WriteLine(product.Title);
    Console.WriteLine(product.Description);
    Console.WriteLine(product.Price);
    Console.WriteLine();
}

Console.ReadKey();