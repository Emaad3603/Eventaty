using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.DAL.Models
{
    public class Ticket : BaseEntity
    {
        [Required(ErrorMessage = "Ticket Code IS Required !!")]
        public string TicketCode { get; set; }

        [Required(ErrorMessage = "Ticket Price IS Required !!")]
        public decimal TicketPrice { get; set; }

        [Required(ErrorMessage = "Ticket Category IS Required !!")]
        public string TicketCategory { get; set; }

        public bool TicketState { get; set; }


        public string? OwnerID { get; set; }

        public Events Event { get; set; }
        [Required(ErrorMessage = "Event ID IS Required !!")]
        public int EventID { get; set; }


    }
}
