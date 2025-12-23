using AirportTool.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTool.Infrastructure.Persistance
{
    public interface IPendingEntitiesService
    {
        public void Add<TDomainModel, TDbModel>(TDomainModel domain, TDbModel db, Action<TDomainModel, TDbModel> applyId);
        public IReadOnlyList<IPendingPair> GetAll();
        public void Clear();
    }
}
