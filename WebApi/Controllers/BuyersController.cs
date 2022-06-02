using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using EntityFramework.Data.Tables;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyersController : Controller
    {
        private readonly MyDbContext context;
        private readonly ILogger logger;

        public BuyersController(MyDbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllBuyers()
        {
            return Ok(context.GetTableByType(new Buyer()).ToList());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateBuyer(Buyer model)
        {
            context.AddNSave(model);
            return Ok(model.Id);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateBuyer(Buyer model)
        {
            context.UpdateNSave(model);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteBuyer(int id)
        {
            var model = context.Buyers.Find(id);
            if (model == null)
                return NotFound();

            context.DeleteNSave(model);
            return NoContent();
        }
    }
}
