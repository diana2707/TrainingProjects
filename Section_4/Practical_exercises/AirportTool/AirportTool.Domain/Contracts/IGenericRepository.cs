using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Domain.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> AddAsync(T entity);
        public T Update(T entity);
        public bool Delete(T entity);
        public Task<T> GetAsync(int id, CancellationToken cancellationToken);
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}
