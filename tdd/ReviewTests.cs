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
    }
}
