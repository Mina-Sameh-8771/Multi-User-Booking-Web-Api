using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Reservation_Task.Models;

namespace Reservation_Task.DataBaseManager
{
    public class ReservationContext: DbContext
    {
        public ReservationContext() : base("ReservationDB") { }

        public DbSet<User> usersTB { get; set; }
        public DbSet<Trip> tripsTB { get; set; }
        public DbSet<Reservation> reservationTB { get; set; }
    }
}
