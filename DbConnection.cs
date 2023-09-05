using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
//using System.Data.Entity;


namespace MyService
{
    public class DataBase:DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Personal> Personals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=!Program-storage.db");
            optionsBuilder.EnableThreadSafetyChecks();
            optionsBuilder.EnableDetailedErrors(true);
        }
    }


    

}
