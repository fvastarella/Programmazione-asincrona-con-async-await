using System.Collections.Generic;
using System.Linq;
using asyncawait.Data;
using Microsoft.AspNetCore.Mvc;

namespace asyncawait.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly AppDbContext db = null;

        public DataController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Get()
        {
            var areas = this.GetAreas();
            var companies = this.GetCompanies();
            var resources = this.GetResources();
            return Ok(new { areas = areas, companies = companies, resources = resources });
        } 


        [Route("areas")]
        public IEnumerable<Area> GetAreas() 
        {
            return this.db.Areas.ToList();
        }

        [Route("companies")]
        public IEnumerable<Company> GetCompanies() 
        {
            return this.db.Companies.ToList();
        }

        [Route("resources")]
        public IEnumerable<Resource> GetResources() 
        {
            return this.db.Resources.ToList();
        }
    }
}