using System.Collections.Generic;
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

        [TestCase(new int[0], 0)]
        [TestCase(new[] { 3 }, 3)]
        [TestCase(new[] { 3, 4 }, 3.5)]
        public void CalculateAverageFor_ReturnsAverage(int [] reviewsRating, decimal expectedAverage)
        {
            foreach (var rating in reviewsRating)
            {
                reviewStore.LeaveReviewFor(exampleMovie, new Review { Rating = rating });
            }

            var averageRating = reviewStore.CalculateAverageFor(exampleMovie);

            Assert.AreEqual(expectedAverage, averageRating);
        }

        [Test]
        public void RatingMapHasZeroValuesWhenMovieHasNoReviews()
        {
            var expectedRatingMap = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } };

            var ratingMap = reviewStore.GetRatingMap(exampleMovie);

            CollectionAssert.AreEquivalent(expectedRatingMap, ratingMap);
        }

        [Test]
        public void RatingMapHasOneNonZeroValueWhenMovieHasOneReview()
        {
            var review = new Review { Rating = 3 };
            reviewStore.LeaveReviewFor(exampleMovie, review);
            var expectedRatingMap = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 1 }, { 4, 0 }, { 5, 0 } };

            var ratingMap = reviewStore.GetRatingMap(exampleMovie);

            CollectionAssert.AreEquivalent(expectedRatingMap, ratingMap);
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
