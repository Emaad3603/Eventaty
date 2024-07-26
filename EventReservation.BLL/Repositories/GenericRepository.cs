using DemoBLL.Interfaces;
using EventReservation.DAL;
using EventReservation.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T: BaseEntity
    {
        private protected  readonly AppDbContext _context;//NULL


        public GenericRepository(AppDbContext context)  //ASK CLR to create object from DBContexts 
        {
            _context = context;
        }

        public int Add(T entity)
        {

           
             _context.Add(entity);
            var result = _context.SaveChanges();
            return result;

                
           
        }
        public int Update(T entity)
        {

            _context.Update(entity);
            return _context.SaveChanges();

        }

        public int Delete(T entity)
        {
           
                _context.Remove(entity);
            return _context.SaveChanges();
            
            
        }

        public async Task<T> Get(int id)
        {
            
            var result = await _context.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
          
                return await _context.Set<T>().ToListAsync();
            
        }

       
    }
}
