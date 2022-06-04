
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
    public class SalesController : Controller
    {
        private readonly ILogger<SalesController> logger;
        private readonly CRUDHelper crud;

        public SalesController(MyDbContext context, ILogger<SalesController> logger)
        {
            this.logger = logger;
            crud = new(this, context, logger, "Sales");
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllSales()
        {
            return crud.Read<Sale>();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateSale(Sale model)
        {
            return crud.Create(model);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateSale(Sale model)
        {
            return crud.Update(model);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteSale(int id)
        {
            return crud.Delete<Sale>(id);
        }
    }
}