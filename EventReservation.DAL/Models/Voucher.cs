using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.DAL.Models
{
    public class Voucher : BaseEntity
    {
        [Required(ErrorMessage = "Voucher Code IS Required !!")]
        public string VoucherCode { get; set; }

        public Events Event { get; set; }

        [Required(ErrorMessage = "Event ID IS Required !!")]
        public int EventID { get; set; }

        [Required(ErrorMessage = " Discount IS Required !!")]
        public decimal VoucherDiscount { get; set; }

    }
}
