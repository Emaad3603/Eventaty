using DemoBLL.Repositories;
using EventReservation.BLL.InterFaces;
using EventReservation.DAL;
using EventReservation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.BLL.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }

        

        public async Task<int> DeleteEventTicketAsync(int id)
        {
           var tickets = await  _context.Tickets.Where(T => T.EventID == id).ToListAsync();
            var result = 0;
           foreach (var ticket in tickets)
            {
                _context.Remove(ticket);
                result =  _context.SaveChanges();
            }
            return result;
        }

        public async Task<IEnumerable<Ticket>> GetEventTicket(int id)
        {
            var tickets = await _context.Tickets.Where(T => T.EventID == id).ToListAsync();
            return tickets;
            
        }

        public  IEnumerable<string> CategoriesToBeBought(int id)
        {
          

            var ticketCategory =  _context.Tickets.Where(t => t.EventID == id && t.TicketState == false)
                                       .Select(t => t.TicketCategory)
                                       .Distinct()
                                       .ToList();

            
            return ticketCategory;
        }

        public Ticket BuyTicket(int id, string ticketCategory)
        {
            var ticket = _context.Tickets.Where(t=>t.EventID == id && t.TicketCategory
                                                    == ticketCategory).FirstOrDefault();

            return ticket;

        }
    }
}
