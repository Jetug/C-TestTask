using EntityFramework.Data.Tables;
using EntityFramework.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidedProductsController : ControllerBase
    {
        private readonly MyDbContext context;

        public ProvidedProductsController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllGadtets()
        {
            return Ok(context.GetTableByType(new ProvidedProduct()).ToList());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateProvidedProduct(ProvidedProduct model)
        {
            context.AddNSave(model);
            return Ok(model.Id);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateProvidedProduct(ProvidedProduct model)
        {
            context.UpdateNSave(model);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteProvidedProduct(int id)
        {
            var model = context.ProvidedProducts.Find(id);
            if (model == null)
                return NotFound();

            context.DeleteNSave(model);
            return NoContent();
        }
    }
}
