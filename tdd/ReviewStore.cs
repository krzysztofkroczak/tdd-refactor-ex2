using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tdd
{
    class ReviewStore
    {
        public void LeaveReviewFor(Movie movie, Review review)
        {
            if (!m_Reviews.ContainsKey(movie))
            {
                Register(movie);
            }

            m_Reviews[movie].Add(review);
        }

        public bool ContainsReviewFor(Movie movie, Review review)
        {
            return m_Reviews[movie].Contains(review);
        }

        public double CalculateAverageFor(Movie movie)
        {
            if (!m_Reviews.ContainsKey(movie))
            {
                return 0;
            }
            return m_Reviews[movie].Average(x => x.Rating);
        }

        public IDictionary<int, int> GetRatingMap(Movie movie)
        {
            var t = new Dictionary<int, int>();
            for (var i = 1; i <= 5; i++)
            {
                t[i] = 0;
            }


            var reviews = GetReviewsFor(movie);

            foreach (var review in reviews)
            {
                t[review.Rating]++;
            }


            return t;
        }

        private void Register(Movie movie)
        {
            m_Reviews.Add(movie, new List<Review>());
        }

        private IEnumerable<Review> GetReviewsFor(Movie movie)
        {
            if (m_Reviews.ContainsKey(movie))
            {
                return m_Reviews[movie];
            }
            else
            {
                return new List<Review>();
            }
        }

        private Dictionary<Movie, List<Review>> m_Reviews = new Dictionary<Movie, List<Review>>();
    }
}
