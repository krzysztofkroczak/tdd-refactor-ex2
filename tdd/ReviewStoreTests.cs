using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
        public void ReviewScoreRangeIsChecked()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    new Review
                    {
                        Rating = 6,
                        ReviewerName = "Alice",
                        ReviewText = "Out of this world"
                    };
                }
            );


        }

        [Test]
        public void DefaultReviewerNameIsAnonymous()
        {
            Assert.AreEqual("Anonymous", (new Review()).ReviewerName);
        }

        [Test]
        public void AverageForNoReviewsIsZero()
        {
            var reviewStore = new ReviewStore();
            var m = new Movie("The Abyss");

            Assert.Zero(reviewStore.CalculateAverageFor(m));
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
            Assert.AreEqual(3, reviewStore.CalculateAverageFor(m));
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
            Assert.AreEqual(3.5, reviewStore.CalculateAverageFor(m));
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
