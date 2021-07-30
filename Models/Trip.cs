using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation_Task.Models
{
    [Serializable]
    public class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(64), MinLength(3)]
        public string name { set; get; }

        public string cityName { set; get; }

        [Range(300, double.MaxValue, ErrorMessage = "Please enter value not less than 300")]
        public double price { set; get; }

        public string imageUrl { set; get; }

        public DateTime creationDate { set; get; }
    }
}
