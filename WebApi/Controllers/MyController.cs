﻿using EntityFramework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        //private readonly ILogger<MyController> _logger;
        private readonly MyDbContext myDbContext;
        private readonly Operations operations;

        public MyController(MyDbContext dbContext)
        {
            myDbContext = dbContext;
            operations = new Operations(dbContext);

        }

        //public MyController(ILogger<MyController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpPost]
        public bool Sale(int? userId, int productId, int quantity)
        {
            return operations.Sale(userId, productId, quantity);
        }
    }
}
