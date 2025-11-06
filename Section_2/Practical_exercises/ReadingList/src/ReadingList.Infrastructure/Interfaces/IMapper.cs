
namespace ReadingList.Infrastructure.Interfaces
{
    public interface IMapper <TInput, TOutput>
    {
        public TOutput Map(TInput input);
    }
}
