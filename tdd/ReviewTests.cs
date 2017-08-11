using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    new Review
                    {
                        Rating = ratingWhichExceedRatingRange,
                        ReviewerName = "Alice",
                        ReviewText = "Out of this world"
                    };
                }
            );


        }

        [Test]
        public void DefaultReviewerNameIsAnonymous()
        {
            Assert.AreEqual("Anonymous", new Review().ReviewerName);
        }
    }
}
