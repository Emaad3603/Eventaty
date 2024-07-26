using EventReservation.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace EventReservation.PL.ViewModels
{
    public class VoucherViewModel
    {
        public int ID { get; set; }
        public int VouchersNumber { get; set; }

        [Required(ErrorMessage = "Voucher Code IS Required !!")]
        public string VoucherCode { get; set; }

        public Events Event { get; set; }

        [Required(ErrorMessage = "Event ID IS Required !!")]
        public int EventID { get; set; }

        [Required(ErrorMessage = " Discount IS Required !!")]
        public decimal VoucherDiscount { get; set; }
    }
}
