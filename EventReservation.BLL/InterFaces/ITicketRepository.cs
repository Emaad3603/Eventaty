using DemoBLL.Interfaces;
using EventReservation.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.BLL.InterFaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        
        public Task<int> DeleteEventTicketAsync(int id);

        public Task<IEnumerable<Ticket>> GetEventTicket(int id);

        public IEnumerable<string> CategoriesToBeBought(int id);

        public Ticket BuyTicket(int id, string ticketCategory);
        
    }
}
