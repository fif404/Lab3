using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ModelDB:DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
           Database.EnsureCreated();
        }

        public DbSet<Stoks> Stoks { get; set; }
        public DbSet<collectors> collectors { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stoks>().HasData(
                new Stoks { Id = 1, name = "Brim", cost = "$50.00", dividends = 20 },
                new Stoks { Id = 2, name = "Neon", cost = "$60.00", dividends = 5 },
                new Stoks { Id = 3, name = "Super", cost = "$49.00", dividends = 10 },
                new Stoks { Id = 4, name = "Sber", cost = "$54.00", dividends = 70 },
                new Stoks { Id = 5, name = "Sky", cost = "$59.00", dividends = 44 },                                
            modelBuilder.Entity<collectors>().HasData(
                

    new collectors { Id = 1, Date = new DateTime(2023, 1, 5), Full_name = "John Doe", Share = 100,
        Cost = "$50.00", Number = 12345, Accruals = "$500.00" },
    new collectors { Id = 2, Date = new DateTime(2023, 2, 12), Full_name = "Jane Doe", Share = 150,
        Cost = "$45.00", Number = 67890, Accruals = "$700.00" },
    new collectors { Id = 3, Date = new DateTime(2023, 3, 20), Full_name = "Alice Smith", Share = 200,
        Cost = "$55.00", Number = 11223, Accruals = "$800.00" },
    new collectors { Id = 4, Date = new DateTime(2023, 4, 7), Full_name = "Bob Johnson", Share = 75,
        Cost = "$60.00", Number = 44556, Accruals = "$400.00" },
    new collectors { Id = 5, Date = new DateTime(2023, 5, 15), Full_name = "Charlie Brown", Share = 120,
        Cost = "$52.00", Number = 77889, Accruals = "$600.00" },
    new collectors { Id = 6, Date = new DateTime(2023, 6, 23), Full_name = "John Doe", Share = 180,
        Cost = "$48.00", Number = 33445, Accruals = "$750.00" },
    new collectors { Id = 7, Date = new DateTime(2023, 7, 1), Full_name = "Jane Doe", Share = 90,
        Cost = "$53.00", Number = 66778, Accruals = "$450.00" },
    new collectors { Id = 8, Date = new DateTime(2023, 8, 8), Full_name = "Alice Smith", Share = 130,
        Cost = "$57.00", Number = 11234, Accruals = "$550.00" },
    new collectors { Id = 9, Date = new DateTime(2023, 9, 16), Full_name = "Bob Johnson", Share = 160,
        Cost = "$59.00", Number = 44567, Accruals = "$680.00" },
    new collectors { Id = 10, Date = new DateTime(2023, 10, 24), Full_name = "Charlie Brown", Share = 110,
        Cost = "$51.00", Number = 77890, Accruals = "$720.00" },
    new collectors { Id = 11, Date = new DateTime(2023, 11, 2), Full_name = "John Doe", Share = 140,
        Cost = "$49.00", Number = 33456, Accruals = "$490.00" },
    new collectors { Id = 12, Date = new DateTime(2023, 12, 10), Full_name = "Jane Doe", Share = 170,
        Cost = "$56.00", Number = 66789, Accruals = "$620.00" },
    new collectors { Id = 13, Date = new DateTime(2023, 1, 18), Full_name = "Alice Smith", Share = 95,
        Cost = "$54.00", Number = 11245, Accruals = "$580.00" },
    new collectors { Id = 14, Date = new DateTime(2023, 2, 26), Full_name = "Bob Johnson", Share = 125,
        Cost = "$58.00", Number = 44578, Accruals = "$670.00" },
    new collectors { Id = 15, Date = new DateTime(2023, 3, 6), Full_name = "Charlie Brown", Share = 105,
        Cost = "$47.00", Number = 77891, Accruals = "$510.00" }              
                );
            modelBuilder.Entity<User>().HasData(
                new User{Id=1,Email="wertsjfg@gmail.com", Password="1265" },
                new User{Id=2,Email="bglev@gmail.com", Password="22323" }
                );
        }
    }
}
