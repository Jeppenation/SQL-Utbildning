using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Models.Entities
{
    public class ProductEntity
    {

        //Exactly the same as the tables in the database
        public string ArticleNumber { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }


    }   
}
