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
        private readonly DbContextOptionsBuilder<AppDbContext> optionsBuilder = null;

        public DataController(IConfiguration configuration)
        {
            this.optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(configuration.GetConnectionString("DefaultConnection"));
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
        public Task<Area[]> GetAreas() 
        {
            using(var db = new AppDbContext(this.optionsBuilder.Options))
            {
                return db.Areas.ToArrayAsync();
            }
        }

        [Route("companies")]
        public Task<Company[]> GetCompanies() 
        {
            using(var db = new AppDbContext(this.optionsBuilder.Options))
            {
                return db.Companies.ToArrayAsync();
            }
        }

        [Route("resources")]
        public Task<Resource[]> GetResources() 
        {
            using(var db = new AppDbContext(this.optionsBuilder.Options))
            {
                return db.Resources.ToArrayAsync();
            }
        }
    }
}