using Xunit;
using AutoFixture;
using Application.Validators;
using Domain.Models;

namespace Tests
{
    public class GetSubOrUnsubValidatorTests
    {
        private GetSubOrUnsubValidator _target = new();

        [Fact]
        public void GetSubOrUnsubValidator_ValidArguments_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            SubOrUnsubDTO newApp = fixture.Create<SubOrUnsubDTO>();
            newApp.submittedAfter = null;

            SubOrUnsubDTO newApp2 = fixture.Create<SubOrUnsubDTO>();
            newApp2.unsubmittedOlder = null;

            var result = _target.Validate(newApp);
            var result2 = _target.Validate(newApp2);

            Assert.Equal((true, false, newApp.unsubmittedOlder, String.Empty), result);
            Assert.Equal((true, true, newApp2.submittedAfter, String.Empty), result2);
        }

        [Fact]
        public void GetSubOrUnsubValidator_EmptyArguments_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            SubOrUnsubDTO newApp = fixture.Create<SubOrUnsubDTO>();
            newApp.submittedAfter = null;
            newApp.unsubmittedOlder = null;

            var result = _target.Validate(newApp);
            var expected = "Укажите один из параметров (submittedAfter или unsubmittedOlder).";

            Assert.Equal((false, false, null, expected), result);
        }

        [Fact]
        public void GetSubOrUnsubValidator_BothArguments_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            SubOrUnsubDTO newApp = fixture.Create<SubOrUnsubDTO>();

            var result = _target.Validate(newApp);
            var expected = "Укажите только один из параметров (submittedAfter или unsubmittedOlder).";

            Assert.Equal((false, false, null, expected), result);
        }
    }
}
