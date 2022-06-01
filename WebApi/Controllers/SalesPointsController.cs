using EntityFramework.Data.Tables;
using EntityFramework.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPointsController : ControllerBase
    {
        private readonly MyDbContext context;

        public SalesPointsController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllGadtets()
        {
            return Ok(context.GetTableByType(new SalesPoint()).ToList());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateSalesPoint(SalesPoint model)
        {
            context.AddNSave(model);
            return Ok(model.Id);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateSalesPoint(SalesPoint model)
        {
            context.UpdateNSave(model);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteSalesPoint(int id)
        {
            var model = context.SalesPoints.Find(id);
            if (model == null)
                return NotFound();

            context.DeleteNSave(model);
            return NoContent();
        }
    }
}