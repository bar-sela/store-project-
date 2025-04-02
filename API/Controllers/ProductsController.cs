using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Needed for List<T>
using API.Data; // Ensure this namespace contains StoreContext


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // if the req is not legit -> returns 400 autoamtic 
    public class ProductsController : ControllerBase 
    {
        private readonly StoreContext _storeContext;

        public ProductsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
           if (_storeContext.Product == null)  // object of type DbSet<Product>
                return NotFound("Products not found.");

            var products = _storeContext.Product.ToList(); // returns an empty list of their are no rows in products table
            products.ForEach(x => Console.WriteLine(x.Name));
            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id ) //  ASP.NET Core automatically binds the id from the request to the method parameter


        {
            var product = _storeContext.Product.Find(id);
            if (product == null)
                return NotFound("");
            
            return product;
        } 
    }
}