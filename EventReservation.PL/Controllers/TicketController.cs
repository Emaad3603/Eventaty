using EventReservation.BLL.InterFaces;
using EventReservation.DAL.Models;
using EventReservation.PL.Helper;
using EventReservation.PL.Helper.QrCodeHelper;
using EventReservation.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Extensions.Logging;
using QRCoder;
using System.Collections.Generic;
using System.Security.Claims;

namespace EventReservation.PL.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private ITicketRepository _ticketRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailSettings _mailSettings;
        private readonly IQrCodeGeneratorHelper _qrCodeGenerator;

        public TicketController(
            ITicketRepository ticketRepository ,
            UserManager<ApplicationUser> userManager,
            IMailSettings mailSettings,
            IQrCodeGeneratorHelper qrCodeGenerator)
        {
            _ticketRepository = ticketRepository;
            _userManager = userManager;
            _mailSettings = mailSettings;
            _qrCodeGenerator = qrCodeGenerator;
        }
        // GET: HomeController1
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            ViewData["CurrentEventID"] = id;
            var tickets = await _ticketRepository.GetEventTicket(id);
            if (tickets is null)
            {
                return BadRequest();
            }
            else
            {
                return View(tickets);
            }

        }
        [HttpGet]
        public IActionResult CreateTickets()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTickets([FromRoute] int id, TicketViewModel model)
        {
           
            int number = model.TicketsNumber;
            int count = 0;
            for (int i = 0; i < number; i++)
            {
                var newTicket = new Ticket()
                {
                    EventID = id,
                    TicketCode = Guid.NewGuid().ToString(),
                    TicketPrice = model.TicketPrice,
                    TicketCategory = model.TicketCategory,

                };

                count = _ticketRepository.Add(newTicket);
            }
            if (count > 0) { return RedirectToAction(nameof(Index), new {id = id}); }
            else { return View(model); }

        }
        [HttpGet]
        public async Task<IActionResult> DeleteEventTicket([FromRoute] int id)
        {
            ViewData["CurrentEventID"] = id;
            var tickets = await _ticketRepository.GetEventTicket(id);
            if (tickets is null)
            {
                return BadRequest();
            }
            else
            {
                return View(tickets);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEventTicket([FromRoute] int id , IEnumerable<Ticket> tickets)
        {
             await  _ticketRepository.DeleteEventTicketAsync(id);
            return RedirectToAction(nameof(Index), new { eventId = id }); // Redirect back to ManageTickets view
        }


        [HttpGet]
        public IActionResult BuyTicket([FromRoute] int id)
        {
            var categories = _ticketRepository.CategoriesToBeBought(id);
            ViewData["CurrentEventID"] = id;

            return View(categories);
           
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> BuyTicket([FromRoute]int id ,  string customData)
        {
            
            var ticket =   _ticketRepository.BuyTicket(id, customData);

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(userEmail);

            //var qrCode = _qrCodeGenerator.GenerateQrCode(ticket.TicketCode);

         

           

            var emailToSend = new Email()
            {
                Subject = "Your Ticket",
                Recipients = userEmail,
                Body = ticket.TicketCode
            };

            if (ticket is not null)
            {
                ticket.TicketState = true;
                ticket.OwnerID = user.Id;

                //string QRCodeAsImageBase64 = $"data:image/png;base64,{Convert.ToBase64String(qrCode)}";


                //await _mailSettings.SendEmailWithQRCodeAsync(emailToSend, QRCodeAsImageBase64);
                await _mailSettings.SendEmailAsync(emailToSend);
                //ViewData["QrCodeImage"] = QRCodeAsImageBase64;
                var categories = _ticketRepository.CategoriesToBeBought(id);
                ViewData["CurrentEventID"] = id;

                return RedirectToAction(nameof(ViewTicket), new { ticketId = id });

            }
            else
            {
                return BadRequest();
            }
        }



        [HttpGet]
        public async Task<IActionResult> ViewTicket( int ticketID)
        {
            var ticket =  await _ticketRepository.Get(ticketID);
            if (ticket is not null)
            {
                var ticketCode = ticket.TicketCode;
                var qrCode = _qrCodeGenerator.GenerateQrCode(ticketCode);
                string QRCodeAsImageBase64 = $"data:image/png;base64,{Convert.ToBase64String(qrCode)}";
                QrCodeViewModel qrCodeTestView = new QrCodeViewModel()
                {
                    QrCodeImageUrl = QRCodeAsImageBase64
                };

                return View(qrCodeTestView);
            }
            
            return View();

        }
       

    }
}
