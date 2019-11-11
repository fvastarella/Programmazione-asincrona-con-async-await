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
        public async Task<Area[]> GetAreas()
        {
            using (var db = new AppDbContext(this.optionsBuilder.Options))
            {
                var data = await db.Areas.ToListAsync();
                data.Insert(0, new Area() { Id = 0, Name = "-" });
                return data.ToArray();
            }
        }

        [Route("companies")]
        public Task<Company[]> GetCompanies()
        {
            return Task.Run(() =>
               {
                   using (var db = new AppDbContext(this.optionsBuilder.Options))
                   {

                       var data = db.Companies.ToList();
                       data.Insert(0, new Company() { Id = 0, Name = "-" });
                       return data.ToArray();
                   }
               });
        }


        [Route("resources")]
        public Task<Resource[]> GetResources()
        {
            using (var db = new AppDbContext(this.optionsBuilder.Options))
            {
                return db.Resources.ToListAsync()
                    .ContinueWith(dataTask =>
                    {
                        var data = dataTask.Result;
                        dataTask.Result.Insert(0, new Resource() { Id = 0, Name = "-" });
                        return data.ToArray();
                    });
            }
        }
    }
}