namespace ReadingList.Domain.Models
{
    public class Book
    {
        private readonly int _id;
        private float _rating;

        // how should i treat the null casses for string properties?
        public Book(int id)
        {
            _id = id;
        }

        public int Id => _id;
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public bool Finished { get; set; }
        public float Rating
        {
            get
            {
                return _rating;
            }

            set
            {
                if (value < 0 || value > 5)
                {
                    throw new ArgumentOutOfRangeException("Rating must be between 0 and 5.");
                }
                _rating = value;
            }
        }
    }
}
