using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation_Task.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string reservedBy { set; get; }
        public string customerName { set; get; }
        public DateTime reservationDate { set; get; }
        public string notes { set; get; }
        public DateTime creationDate { set; get; }

        public Trip trip { set; get; }
    }
}
