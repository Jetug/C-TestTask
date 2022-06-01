using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using EntityFramework.Data.Tables;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyersController : Controller
    {
        private readonly MyDbContext context;

        public BuyersController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllGadtets()
        {
            return Ok(context.GetByType(new Buyer()).ToList());
            //return Ok(context.Buyers.ToList());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateBuyer(Buyer buyer)
        {
            context.Buyers.Add(buyer);
            context.SaveChanges();
            return Ok(buyer.Id);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateBuyer(Buyer buyer)
        {
            context.Buyers.Update(buyer);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteBuyer(int id)
        {
            var buyer = context.Buyers.Find(id);
            if (buyer == null)
                return NotFound();

            context.Buyers.Remove(buyer);
            context.SaveChanges();
            return NoContent();
        }
    }
}
