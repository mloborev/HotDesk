using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesc.Models
{
    public class Workplace
    {
        public int Id { get; set; }
        public int? ReservationId { get; set; }
        public string Description { get; set; }
        public string Devices { get; set; }
        public bool IsInUse { get; set; }

        public Reservation Reservation { get; set; }
    }
}
