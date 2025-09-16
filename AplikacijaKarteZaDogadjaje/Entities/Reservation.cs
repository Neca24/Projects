using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public Status ReservationStatus { get; set; } = Status.PENDING;
    }
}
