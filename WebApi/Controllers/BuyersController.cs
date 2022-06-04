using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using Microsoft.Extensions.Logging;
using EntityFramework.Data.Models;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyersController : Controller
    {
        private readonly CRUDHelper crud;

        public BuyersController(MyDbContext context, ILogger<BuyersController> logger)
        {
            crud = new(this, context, logger, "Buyers");
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllBuyers()
        {
            return crud.Read<Buyer>();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateBuyer(Buyer model)
        {
            return crud.Create(model);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateBuyer(Buyer model)
        {
            return crud.Update(model);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteBuyer(int id)
        {
            return crud.Delete<Buyer>(id);
        }
    }
}
