using System;
using CzwartkKolos.Models;
using CzwartkKolos.Services;
using Microsoft.AspNetCore.Mvc;

namespace CzwartkKolos.Controllers
{
    [ApiController]
    [Route("/api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IDbservice _dbService;
        public ProjectController(IDbservice dbService)
        {
            _dbService = dbService;
        }
        private string sqlConGlobal =
            "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=s18935; Integrated Security = true";
        
        // GET
        [HttpGet]
        [Route("{index}")]
        public IActionResult Index(String index)
        {
            ProjectResult myProjResult = _dbService.getProjects(index);
            return Ok(myProjResult);
        }
        

    }
}