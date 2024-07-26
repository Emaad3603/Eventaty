using DemoBLL.Interfaces;
using EventReservation.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.BLL.InterFaces
{
    public interface IVoucherRepository : IGenericRepository<Voucher>
    {
        public Task<int> DeleteEventVoucherAsync(int id);

        public Task<IEnumerable<Voucher>> GetVoucher(int id);
    }
}
