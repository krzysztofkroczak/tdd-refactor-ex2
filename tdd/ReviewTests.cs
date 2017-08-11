using System;
using NUnit.Framework;

namespace tdd
{
    [TestFixture]
    class ReviewTests
    {
        [TestCase(6)]
        [TestCase(0)]
        public void Review_ThrowsArgumentException_WhenRatingExceedsLimits(int ratingWhichExceedRatingRange)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    new Review(ratingWhichExceedRatingRange)
                    {
                        ReviewerName = "Alice",
                        ReviewText = "Out of this world"
                    };
                }
            );
        }

        [Test]
        public void DefaultReviewerNameIsAnonymous()
        {
            Assert.AreEqual("Anonymous", new Review(rating:1).ReviewerName);
        }
    }
}
