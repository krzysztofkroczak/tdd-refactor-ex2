using NUnit.Framework;

namespace tdd
{
    [TestFixture]
    class ReviewStoreTests
    {
        [Test]
        public void CanLeaveReview()
        {
            var r = new Review
            {
                Rating = 1,
                ReviewerName = "Bob",
                ReviewText = "Couldn't get into"

            };

            reviewStore.LeaveReviewFor(exampleMovie, r);

            Assert.IsTrue(reviewStore.ContainsReviewFor(exampleMovie, r));
        }

        [Test]
        public void AverageForNoReviewsIsZero()
        {
            var averageScore = reviewStore.CalculateAverageFor(exampleMovie);

            Assert.Zero(averageScore);
        }

        [Test]
        public void AverageForOneReviewIsValue()
        {
            var review = new Review { Rating = 3 };
            reviewStore.LeaveReviewFor(exampleMovie, review);

            var averageRating = reviewStore.CalculateAverageFor(exampleMovie);

            Assert.AreEqual(3, averageRating);
        }

        [Test]
        public void AverageForTwoReviewsIsMean()
        {
            var review = new Review { Rating = 3 };
            var review2 = new Review { Rating = 4 };
            reviewStore.LeaveReviewFor(exampleMovie, review);
            reviewStore.LeaveReviewFor(exampleMovie, review2);

            var averageRating = reviewStore.CalculateAverageFor(exampleMovie);

            Assert.AreEqual(3.5, averageRating);
        }


        [Test]
        public void TableForMovieIsZeroIfNoReviews()
        {
            var table = reviewStore.GetTableFor(exampleMovie);

            for (var i = 1; i <= 5; i++)
            {
                Assert.Zero(table[i]);
            }
        }

        [Test]
        public void TableForMovieIsValueIfOneReview()
        {
            var review = new Review
            {
                Rating = 3
            };
            reviewStore.LeaveReviewFor(exampleMovie, review);

            var table = reviewStore.GetTableFor(exampleMovie);

            Assert.AreEqual(1, table[3]);
            Assert.AreEqual(0, table[1]);
            Assert.AreEqual(0, table[2]);
            Assert.AreEqual(0, table[4]);
            Assert.AreEqual(0, table[5]);
        }

        [SetUp]
        public void InitializeReviewStore()
        {
            reviewStore = new ReviewStore();
        }

        [SetUp]
        public void InitializeExampleMovie()
        {
            exampleMovie = new Movie("The Abyss");
        }


        private ReviewStore reviewStore;
        private Movie exampleMovie;
    }
}
