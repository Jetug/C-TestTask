
using EntityFramework.Data;
using EntityFramework.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidedProductsController : Controller
    {
        private readonly ILogger<ProvidedProductsController> logger;
        private readonly CRUDHelper crud;

        public ProvidedProductsController(MyDbContext context, ILogger<ProvidedProductsController> logger)
        {
            this.logger = logger;
            crud = new(this, context, logger, "ProvidedProducts");
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllProvidedProducts()
        {
            return crud.Read<ProvidedProduct>();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateProvidedProduct(ProvidedProduct model)
        {
            return crud.Create(model);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateProvidedProduct(ProvidedProduct model)
        {
            return crud.Update(model);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteProvidedProduct(int id)
        {
            return crud.Delete<ProvidedProduct>(id);
        }
    }
}
