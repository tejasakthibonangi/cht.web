using Cht.HMS.Web.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cht.HMS.Web.API.DBConfiguration
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //from here add your db sets 

        public DbSet<Role> roles { get; set; }
        public DbSet<ConsultationDetails> consultationDetails { get; set; }
    }
}
