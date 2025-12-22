
namespace AirportTool.Infrastructure.Utils
{
    internal class PendingPair<TDomainModel, TDbModel> : IPendingPair
    {
        private readonly TDomainModel _domain;
        private readonly TDbModel _db;
        private readonly Action<TDomainModel, TDbModel> _apply;

        public PendingPair(TDomainModel domain, TDbModel db, Action<TDomainModel, TDbModel> apply)
        {
            _domain = domain;
            _db = db;
            _apply = apply;
        }

        public void Apply() => _apply(_domain, _db);
    }
}
