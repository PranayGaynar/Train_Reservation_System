using Microsoft.EntityFrameworkCore;
using TrainTicketReservationSystem.Models;


public class MyNewContext : DbContext
{
    public MyNewContext(DbContextOptions<MyNewContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Train> Train { get; set; }

    public DbSet<TrainRoute> TrainRoute { get; set; }

    public DbSet<Booking> Booking { get; set; }
    public DbSet<Payment> Payment { get; set; }

   

}