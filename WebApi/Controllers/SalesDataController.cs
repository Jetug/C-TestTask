
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
    public class SalesDataController : Controller
    {
        private readonly ILogger<SalesDataController> logger;
        private readonly CRUDHelper crud;

        public SalesDataController(MyDbContext context, ILogger<SalesDataController> logger)
        {
            this.logger = logger;
            crud = new(this, context, logger, "SalesData");
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllSaleData()
        {
            return crud.Read<SaleData>();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateSaleData(SaleData model)
        {
            return crud.Create(model);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateSaleData(SaleData model)
        {
            return crud.Update(model);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteSaleData(int id)
        {
            return crud.Delete<SaleData>(id);
        }
    }
}
