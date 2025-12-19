using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Utils
{
    public class PendingEntitiesService
    {
        private readonly List<IPendingPair> _pending = new();

        public void Add<TDomainModel, TDbModel>(TDomainModel domain, TDbModel db, Action<TDomainModel, TDbModel> applyId)
        {
            _pending.Add(new PendingPair<TDomainModel, TDbModel>(domain, db, applyId));
        }

        public IReadOnlyList<IPendingPair> GetAll() => _pending;

        public void Clear() => _pending.Clear();
    }

}
