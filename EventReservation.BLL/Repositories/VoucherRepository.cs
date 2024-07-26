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
    public class VoucherRepository : GenericRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<int> DeleteEventVoucherAsync(int id)
        {
            var vouchers = await _context.Vouchers.Where(T => T.EventID == id).ToListAsync();
            var result = 0;
            foreach (var voucher in vouchers)
            {
                _context.Remove(voucher);
                result = _context.SaveChanges();
            }
            return result;
        }

        public async Task<IEnumerable<Voucher>> GetVoucher(int id)
        {
            var vouchers = await _context.Vouchers.Where(T => T.EventID == id).ToListAsync();
            return vouchers;

        }
    }
}
