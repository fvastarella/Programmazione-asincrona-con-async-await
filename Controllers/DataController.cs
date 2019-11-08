using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asyncawait.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var areas = Task.Run(() => this.GetAreas());
            var companies = Task.Run(() => this.GetCompanies());
            var resources = Task.Run(() => this.GetResources());

            Task.WhenAll(areas, companies, resources);

            return Ok(new { areas = areas.Result, companies = companies.Result, resources = resources.Result });
        }

        [Route("areas")]
        public Area[] GetAreas() 
        {
            return this.db.Areas.ToArray();
        }

        [Route("companies")]
        public Company[] GetCompanies() 
        {
            return this.db.Companies.ToArray();
        }

        [Route("resources")]
        public Resource[] GetResources() 
        {
            return this.db.Resources.ToArray();
        }
    }
}