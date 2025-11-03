using ReadingList.Domain.Models;
using ReadingList.Domain.Shared;

namespace ReadingList.Infrastructure.Interfaces
{
    public interface IMapper <TInput, TOutput>
    {
        public TOutput Map(TInput input);
    }
}
