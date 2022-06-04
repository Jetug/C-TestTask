
using EntityFramework.Data;
using EntityFramework.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> logger;
        private readonly CRUDHelper crud;

        public ProductsController(MyDbContext context, ILogger<ProductsController> logger)
        {
            this.logger = logger;
            crud = new(this, context, logger, "Products");
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllProducts()
        {
            return crud.Read<Product>();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateProduct(Product model)
        {
            return crud.Create(model);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateProduct(Product model)
        {
            return crud.Update(model);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            return crud.Delete<Product>(id);
        }
    }
}