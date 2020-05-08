using Microsoft.AspNetCore.Mvc;

namespace CzwartkKolos.Controllers
{
    [ApiController]
    [Route("/api")]
    public class An : Controller
    {
        // GET
        public IActionResult Index()
        {
            
            return Ok(200);
        }
    }
}