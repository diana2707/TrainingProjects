using AirportTool.Domain.Contracts;
using AirportTool.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AirportDbContext _context;

        public GenericRepository(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Set<T>().AddAsync(entity);

            return entity;
        }

        // verify here if the entity exists in the database before updating
        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Update(entity);

            return entity;
        }

        public bool Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }

            _context.Set<T>().Remove(entity);

            return true;
        }

        public async Task<T> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}
