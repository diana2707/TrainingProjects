
namespace ReadingList.Infrastructure.DTOs
{
    public record BookStatsDto
    {
        public int BookCount;
        public int FinishedCount;
        public float AverageRating;
        public Dictionary<string, int>? PagesByGenre;
        public Dictionary<string, int>? Top3AuthorsByBookCount;
    }
}
