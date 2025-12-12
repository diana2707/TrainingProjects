using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public void Add(T entity);
        public void Update(T entity);
        public bool Delete(T entity);
        public T Get(int id);
        public IEnumerable<T> GetAll();
    }
}
