using Microsoft.EntityFrameworkCore;
using WebApi.netcoreMvcDemo.Models;

namespace WebApi.netcoreMvcDemo.Models
{
    public class EmpContext : DbContext
    {
        public EmpContext(DbContextOptions dbContextOptions) :
            base(dbContextOptions)
        {

        }

        public DbSet<Employee> employees { set; get; }

    }
}