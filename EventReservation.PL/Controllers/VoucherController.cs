using EventReservation.BLL.InterFaces;
using EventReservation.BLL.Repositories;
using EventReservation.DAL.Models;
using EventReservation.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventReservation.PL.Controllers
{
    [Authorize]
    public class VoucherController : Controller
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherController(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }
        // GET: HomeController1
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            ViewData["CurrentEventID"] = id;
            var vouchers = await _voucherRepository.GetVoucher(id);
            if (vouchers is null)
            {
                return BadRequest();
            }
            else
            {
                return View(vouchers);
            }

        }
        [HttpGet]
        public IActionResult CreateVouchers()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateVouchers([FromRoute] int id, VoucherViewModel model)
        {
            ViewData["CurrentEventID"] = id;
            int number = model.VouchersNumber;
            int count = 0;
            for (int i = 0; i < number; i++)
            {
                var newVoucher = new Voucher()
                {
                    EventID = id,
                    VoucherCode = Guid.NewGuid().ToString(),
                    VoucherDiscount = model.VoucherDiscount,
                   

                };

                count = +_voucherRepository.Add(newVoucher);
            }
            if (count > 0) { return RedirectToAction(nameof(Index), new {id=id}); }
            else { return View(model); }

        }
        [HttpGet]
        public async Task<IActionResult> DeleteEventVoucher([FromRoute] int id)
        {
            ViewData["CurrentEventID"] = id;
            var vouchers = await _voucherRepository.GetVoucher(id);
            if (vouchers is null)
            {
                return BadRequest();
            }
            else
            {
                return View(vouchers);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEventVoucher([FromRoute] int id, IEnumerable<Voucher> vouchers)
        {
            await _voucherRepository.DeleteEventVoucherAsync(id);
            return RedirectToAction(nameof(Index), new { eventId = id }); // Redirect back to Manage Vouchers view
        }
    }
}
