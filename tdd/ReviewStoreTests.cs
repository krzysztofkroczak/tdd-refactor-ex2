using System.Collections;
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
        public void CalculateAverageFor_ReturnsAverage(int[] reviewsRatings, decimal expectedAverage)
        {
            foreach (var rating in reviewsRatings)
            {
                reviewStore.LeaveReviewFor(exampleMovie, new Review { Rating = rating });
            }

            var averageRating = reviewStore.CalculateAverageFor(exampleMovie);

            Assert.AreEqual(expectedAverage, averageRating);
        }

        [Test, TestCaseSource(typeof(ReviewStoreTests), nameof(ReviewRatingPerRatingMap))]
        public void GetRatingMap_ReturnsRatingValuePerCountReview(int[] reviewsRatings, IDictionary expectedRatingMap)
        {
            foreach (var rating in reviewsRatings)
            {
                reviewStore.LeaveReviewFor(exampleMovie, new Review { Rating = rating });
            }

            var ratingMap = reviewStore.GetRatingMap(exampleMovie);

            CollectionAssert.AreEquivalent(expectedRatingMap, ratingMap);
        }

        public static IEnumerable ReviewRatingPerRatingMap
        {
            get
            {
                yield return new TestCaseData(new int[0], new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } });
                yield return new TestCaseData(new[] { 3 }, new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 1 }, { 4, 0 }, { 5, 0 } });
                yield return new TestCaseData(new[] { 3, 3 }, new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 2 }, { 4, 0 }, { 5, 0 } });
            }
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
