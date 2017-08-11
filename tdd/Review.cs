using System;

namespace tdd
{
    class Review
    {
        public Review(int rating)
        {
            Rating = rating;
        }

        public int Rating
        {
            get => m_Rating;
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

        private int m_Rating;
    }
}