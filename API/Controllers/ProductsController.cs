using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; // Needed for List<T>
using API.Data;
using Microsoft.EntityFrameworkCore; // Ensure this namespace contains StoreContext


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
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            if (_storeContext.Product == null) // בדיקה האם ה-DbSet<Product> קיים
                return NotFound("Products not found.");

            var products = await _storeContext.Product.ToListAsync(); // קריאה אסינכרונית למסד הנתונים

            products.ForEach(x => Console.WriteLine(x.Name)); // הדפסת שמות המוצרים לקונסול
            
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