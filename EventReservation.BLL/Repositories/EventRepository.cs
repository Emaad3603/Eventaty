using DemoBLL.Repositories;
using EventReservation.BLL.InterFaces;
using EventReservation.DAL;
using EventReservation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.BLL.Repositories
{
    public class EventRepository : GenericRepository<Events>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
            
        }
       public async Task<IEnumerable<Events>> GetByName(string name)
        {
            return await _context.Events.Where(E=>E.EventName.ToLower().Contains(name.ToLower())).ToListAsync();
        }

       
    }
}
