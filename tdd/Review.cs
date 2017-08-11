using System;

namespace tdd
{
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