using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Models
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