﻿using EntityFramework.Data.Tables;
using EntityFramework.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly MyDbContext context;

        public SalesController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllGadtets()
        {
            return Ok(context.GetTableByType(new Sale()).ToList());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateSale(Sale model)
        {
            context.AddNSave(model);
            return Ok(model.Id);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateSale(Sale model)
        {
            context.UpdateNSave(model);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteSale(int id)
        {
            var model = context.Sales.Find(id);
            if (model == null)
                return NotFound();

            context.DeleteNSave(model);
            return NoContent();
        }
    }
}