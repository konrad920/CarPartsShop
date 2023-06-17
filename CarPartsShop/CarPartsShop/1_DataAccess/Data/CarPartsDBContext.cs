using CarPartsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarPartsShop.Data
{
    public class CarPartsDBContext : DbContext
    {
        //public DbSet<CarParts> CarParts =>  Set<CarParts>();

        //public DbSet<MotoParts> MotorParts => Set<MotoParts>();

        //public DbSet<LorriesParts> LorriesParts => Set<LorriesParts>();

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseInMemoryDatabase("StorageApp");
        //}
        public CarPartsDBContext(DbContextOptions<CarPartsDBContext> options)
            : base(options)
        {
        }

        public DbSet<CarParts> CarParts { get; set; }
    }
}
