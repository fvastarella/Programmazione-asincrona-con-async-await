using System.Linq;
using System.Threading.Tasks;
using asyncawait.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

            Task.WhenAll(areas, companies, resources);

            return Ok(new { areas = areas.Result, companies = companies.Result, resources = resources.Result });
        }

        [Route("areas")]
        public async Task<Area[]> GetAreas() 
        {
            var data = await this.db.Areas.ToListAsync();
            data.Insert(0, new Area() { Id = 0, Name = "-"});
            return data.ToArray();
        }

        [Route("companies")]
        public Task<Company[]> GetCompanies() 
        {
            return this.db.Companies.ToArrayAsync();
        }

        [Route("resources")]
        public Task<Resource[]> GetResources() 
        {
            return this.db.Resources.ToArrayAsync();
        }
    }
}