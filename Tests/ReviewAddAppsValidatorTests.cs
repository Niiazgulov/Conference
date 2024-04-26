using Xunit;
using AutoFixture;
using Domain.Models;
using Application.Validators;

namespace Tests
{
    public class ReviewAddAppsValidatorTests
    {
        private ReviewAddAppsValidator _target = new();

        [Fact]
        public void ReviewAddAppsAppsValidator_ValidArguments_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            Applications dbApp = fixture.Create<Applications>();
            string sended = "NO";

            var result = _target.Validate(dbApp, sended);

            Assert.Equal((true, String.Empty), result);
        }

        [Fact]
        public void ReviewAddAppsAppsValidator_WrongId_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            Applications? dbApp = null;
            string sended = "NO";
            var expected = "ОШИБКА! Такой заявки не существует!";

            var result = _target.Validate(dbApp, sended);

            Assert.Equal((false, expected), result);
        }

        [Fact]
        public void ReviewAddAppsAppsValidator_AlreadySended_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            Applications dbApp = fixture.Create<Applications>();
            string sended = "YES";
            var expected = "ОШИБКА! Невозможно выполнить, заявка уже направлена на рассмотрение.";

            var result = _target.Validate(dbApp, sended);

            Assert.Equal((false, expected), result);
        }
    }
}
