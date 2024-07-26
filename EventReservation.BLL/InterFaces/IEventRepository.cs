using DemoBLL.Interfaces;
using EventReservation.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.BLL.InterFaces
{
    public interface IEventRepository : IGenericRepository<Events>
    {
        Task<IEnumerable<Events>> GetByName(string name);
    }

}
