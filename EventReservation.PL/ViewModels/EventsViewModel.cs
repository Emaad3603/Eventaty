﻿using EventReservation.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace EventReservation.PL.ViewModels
{
    public class EventsViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Event Code IS Required !!")]
        public string EventCode { get; set; }

        [Required(ErrorMessage = "Event Name IS Required !!")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Event Organizer IS Required !!")]
        public string EventOrganizer { get; set; }

        [Required(ErrorMessage = "Event Date IS Required !!")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Event Location IS Required !!")]
        public string EventLocation { get; set; }

        public string? ImageName { get; set; }

        public IFormFile? Image { get; set; }

        public string? EventDuration { get; set; }

        [Required(ErrorMessage = "Event Description IS Required !!")]

        public string EventDescription { get; set; }

        public ICollection<Voucher>? Vouchers { get; set; }
    }
}
