using EventReservation.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace EventReservation.PL.ViewModels
{
    public class TicketViewModel
    {
        public int ID { get; set; }
        public int TicketsNumber { get; set; }

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
