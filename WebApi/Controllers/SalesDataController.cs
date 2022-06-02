using EntityFramework.Data.Tables;
using EntityFramework.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDataController : ControllerBase
    {
        private readonly MyDbContext context;

        public SalesDataController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllGadtets()
        {
            return Ok(context.GetTableByType(new SaleData()).ToList());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateSaleData(SaleData model)
        {
            context.AddNSave(model);
            return Ok(model.Id);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateSaleData(SaleData model)
        {
            context.UpdateNSave(model);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteSaleData(int id)
        {
            var model = context.SalesData.Find(id);
            if (model == null)
                return NotFound();

            context.DeleteNSave(model);
            return NoContent();
        }
    }
}
