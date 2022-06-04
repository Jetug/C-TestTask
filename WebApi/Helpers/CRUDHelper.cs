using EntityFramework.Data;
using EntityFramework.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace WebApi.Helpers
{
    public class CRUDHelper
    {
        private readonly Controller controller;
        private readonly MyDbContext context;
        private readonly ILogger logger;
        private readonly string name;

        public CRUDHelper(Controller controller, MyDbContext context, ILogger logger, string name)
        {
            this.controller = controller;
            this.context = context;
            this.logger = logger;
            this.name = name;
        }

        public IActionResult Read<T>() where T: class
        {
            logger.LogInformation(name + ": get all...");
            var list = context.GetTable<T>().ToList();

            if(list == null)
            {
                logger.LogInformation(name + ": get all - fail");
                return controller.NotFound();
            }

            logger.LogInformation(name + ": get all - success");
            return controller.Ok(list); 
        }

        public IActionResult Create<T>(T model) where T : class, IIdEntity
        {
            logger.LogInformation(name + ": create");

            if(model.Id == 0)
            {
                context.AddNSave(model);
                logger.LogInformation(name + ": create - success");
                return controller.Ok(model.Id);
            }
            logger.LogInformation(name + ": create - fail");
            return controller.NotFound();
        }

        public IActionResult Update<T>(T model) where T : class
        {
            logger.LogInformation(name + ": update");

            if (context.GetTable<T>().Contains(model))
            {
                context.UpdateNSave(model);
                logger.LogInformation(name + ": update - success");
                return controller.NoContent();
            }

            logger.LogInformation(name + ": update - fail");
            return controller.NotFound();
        }    

        public IActionResult Delete<T>(int id) where T : class
        {
            logger.LogInformation(name + ": delete");
            var model = context.GetTable<T>().Find(id);

            if (model == null)
            {
                logger.LogInformation(name + ": delete - fail");
                return controller.NotFound();
            }

            context.DeleteNSave(model);
            logger.LogInformation(name + ": delete - success");
            return controller.NoContent();
        }
    }
}
