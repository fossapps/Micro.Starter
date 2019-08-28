using Microsoft.EntityFrameworkCore;

namespace Micro.Starter.Api.Models
{
    public class ApplicationContext
    {
        public DbSet<Weather> Weathers { set; get; }
    }
}
