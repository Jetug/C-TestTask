
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
    public class SalesPointsController : Controller
    {
        private readonly ILogger<SalesPointsController> logger;
        private readonly CRUDHelper crud;

        public SalesPointsController(MyDbContext context, ILogger<SalesPointsController> logger)
        {
            this.logger = logger;
            crud = new(this, context, logger, "SalesPoints");
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllSalesPoints()
        {
            return crud.Read<SalesPoint>();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateSalesPoint(SalesPoint model)
        {
            return crud.Create(model);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateSalesPoint(SalesPoint model)
        {
            return crud.Update(model);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteSalesPoint(int id)
        {
            return crud.Delete<SalesPoint>(id);
        }
    }
}