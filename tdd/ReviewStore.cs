using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tdd
{
    class ReviewStore
    {
        
        private Dictionary<Movie, List<Review>> m_Reviews = new Dictionary<Movie, List<Review>>();

        private void Register(Movie movie)
        {
            m_Reviews.Add(movie, new List<Review>());
        }

        public void LeaveReviewFor(Movie movie, Review review)
        {
            if (!m_Reviews.ContainsKey(movie))
            {
                Register(movie);
            }

            m_Reviews[movie].Add(review);
        }

        public bool Contains(Movie movie)
        {
            return m_Reviews.ContainsKey(movie);
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

        public IDictionary<int, int> GetTableFor(Movie movie)
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
    }


    class Movie
    {
        public string Name { get; set; }

        public Movie(string name)
        {
            Name = name;
        }
    }

    class Review
    {

        private int m_Rating;

        public int Rating
        {
            get { return m_Rating;}

            set
            {
                 if (value > 5 || value < 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                m_Rating = value;
            }
        }


        public string ReviewerName { get; set; } = "Anonymous";
        public string ReviewText { get; set;  }

 
    }

}
