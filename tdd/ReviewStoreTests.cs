using NUnit.Framework;

namespace tdd
{
    [TestFixture]
    class ReviewStoreTests
    {
        [Test]
        public void CanLeaveReview()
        {
            var reviewStore = new ReviewStore();
            var m = new Movie("The Abyss");
            // reviewStore.Register(m);
            var r = new Review
            {
                Rating = 1,
                ReviewerName = "Bob",
                ReviewText = "Couldn't get into"

            };

            reviewStore.LeaveReviewFor(m, r);

            Assert.IsTrue(reviewStore.ContainsReviewFor(m, r));
        }

        [Test]
        public void AverageForNoReviewsIsZero()
        {
            var reviewStore = new ReviewStore();
            var m = new Movie("The Abyss");

            var averageScore = reviewStore.CalculateAverageFor(m);

            Assert.Zero(averageScore);
        }

        [Test]
        public void AverageForOneReviewIsValue()
        {
            var reviewStore = new ReviewStore();
            var m = new Movie("The Abyss");
            var review = new Review
            {
                Rating = 3
            };
            reviewStore.LeaveReviewFor(m, review);

            var averageRating = reviewStore.CalculateAverageFor(m);

            Assert.AreEqual(3, averageRating);
        }

        [Test]
        public void AverageForTwoReviewsIsMean()
        {
            var reviewStore = new ReviewStore();
            var m = new Movie("The Abyss");
            var review = new Review
            {
                Rating = 3
            };
            var review2 = new Review
            {
                Rating = 4
            };
            reviewStore.LeaveReviewFor(m, review);
            reviewStore.LeaveReviewFor(m, review2);

            var averageRating = reviewStore.CalculateAverageFor(m);

            Assert.AreEqual(3.5, averageRating);
        }


        [Test]
        public void TableForMovieIsZeroIfNoReviews()
        {
            var reviewStore = new ReviewStore();
            var m = new Movie("The Abyss");

            var table = reviewStore.GetTableFor(m);

            for (var i = 1; i <= 5; i++)
            {
                Assert.Zero(table[i]);
            }
        }

        [Test]
        public void TableForMovieIsValueIfOneReview()
        {
            var reviewStore = new ReviewStore();
            var m = new Movie("The Abyss");
            var review = new Review
            {
                Rating = 3
            };
            reviewStore.LeaveReviewFor(m, review);

            var table = reviewStore.GetTableFor(m);

            Assert.AreEqual(1, table[3]);
            Assert.AreEqual(0, table[1]);
            Assert.AreEqual(0, table[2]);
            Assert.AreEqual(0, table[4]);
            Assert.AreEqual(0, table[5]);
        }
    }
}
