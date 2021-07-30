using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation_Task.Models
{
    public class ReservationInformation
    {
        public int id { get; set; }

        public string reservedBy { get; set; }

        public string customerName { get; set; }

        public DateTime reservationDate { get; set; }

        public string notes { get; set; }

        public DateTime creationDate { get; set; }

        public string cityName { get; set; }

        public double price { get; set; }

        public string imageUrl { get; set; }

        public string name { get; set; }
    }
}
