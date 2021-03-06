using Domain;
using MyHotels.WebAPI.EFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotels.WebAPI.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyHotelsDBContext _context;

        public UnitOfWork(MyHotelsDBContext context)
        {
            this._context = context;
        }

        public IGenericRepository<Country> Countries => new GenericRepository<Country>(_context);
        public IGenericRepository<Hotel> Hotels => new GenericRepository<Hotel>(_context);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(true);
        }
    }
}
