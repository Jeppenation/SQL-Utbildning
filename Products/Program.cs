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
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("--- SHOWS RESULT OF ADD ---");
Console.ResetColor();
Console.WriteLine(result);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n--- SHOWS ALL PRODUCTS ---");
Console.ResetColor();
foreach (var product in productService.GetAllProducts())
{
    Console.WriteLine($"{product.ArticleNumber}, {product.Title}, {product.Description}, {product.Price}\n ");

    
}

//Prints the result of getting one product
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n--- SHOWS ONE PRODUCT ---");
Console.ResetColor();
var oneProduct = productService.GetOneProduct("A1");

//If the product does not exist, this code will run
if (oneProduct == null)
{
    Console.WriteLine("Product does not exist");
}
//If the product does exist, this code will run
else
    Console.WriteLine($"{oneProduct.ArticleNumber}, {oneProduct.Title}, {oneProduct.Description}, {oneProduct.Price}");


Console.ReadKey();