using System;
using CzwartkKolos.Services;
using Microsoft.AspNetCore.Mvc;

namespace CzwartkKolos.Controllers
{
    [ApiController]
    [Route("/api/tasks")]
    public class TasksController : ControllerBase
    {
        IDbservice _dbService;
        public TasksController(IDbservice dbService)
        {
            _dbService = dbService;
        }
        private string sqlConGlobal =
            "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=s18935; Integrated Security = true";
        
        [HttpPost]
        public IActionResult Index(Object Json)
        {

           
            
            return Ok( _dbService.postTask(Json));
        }
    }
}