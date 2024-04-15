using Xunit;
using AutoFixture;
using Domain.Models;
using Application.Validators;

namespace Tests
{
    public class EditAppsValidatorTests
    {
        private EditAppsValidator _target = new();

        [Fact]
        public void EditAppsValidator_ValidArguments_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            EditedAppDTO newApp = fixture.Create<EditedAppDTO>();
            newApp.Activity = "Report";
            Applications dbApp = fixture.Create<Applications>();
            string sended = "NO";
            
            var result = _target.Validate(newApp, dbApp, sended);

            Assert.Equal((true, String.Empty), result);
        }

        [Fact]
        public void EditAppsValidator_WrongId_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            EditedAppDTO newApp = fixture.Create<EditedAppDTO>();
            newApp.Activity = "Report";
            Applications? dbApp = null;
            string sended = "NO";
            var expected = "ОШИБКА! Такой заявки не существует!";

            var result = _target.Validate(newApp, dbApp, sended);

            Assert.Equal((false, expected), result);
        }

        [Fact]
        public void EditAppsValidator_AlreadySended_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            EditedAppDTO newApp = fixture.Create<EditedAppDTO>();
            newApp.Activity = "Report";
            Applications dbApp = fixture.Create<Applications>();
            string sended = "YES";
            var expected = "ОШИБКА! Невозможно выполнить, заявка уже направлена на рассмотрение.";

            var result = _target.Validate(newApp, dbApp, sended);

            Assert.Equal((false, expected), result);
        }

        [Fact]
        public void EditAppsValidator_WrongActivityField_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            EditedAppDTO newApp = fixture.Create<EditedAppDTO>();
            Applications dbApp = fixture.Create<Applications>();
            string sended = "NO";
            var expected = "Некорректный формат поля \"Тип активности\"! (Activity) Выберите один из 3 вариантов - Report, Masterclass, Discussion";

            var result = _target.Validate(newApp, dbApp, sended);

            Assert.Equal((false, expected), result);
        }

        [Fact]
        public void EditAppsValidator_EmptyStrings_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            Applications dbApp = fixture.Create<Applications>();
            string sended = "NO";

            EditedAppDTO newApp = fixture.Create<EditedAppDTO>();
            newApp.Name = String.Empty;
            newApp.Activity = "Report";
            var result = _target.Validate(newApp, dbApp, sended);
            var expected = "Пустые строки недопустимы, введите значение в поле Name";

            EditedAppDTO newApp2 = fixture.Create<EditedAppDTO>();
            newApp2.Description = String.Empty;
            newApp2.Activity = "Report";
            var result2 = _target.Validate(newApp2, dbApp, sended);
            var expected2 = "Пустые строки недопустимы, введите значение в поле Description";

            EditedAppDTO newApp3 = fixture.Create<EditedAppDTO>();
            newApp3.Outline = String.Empty;
            newApp3.Activity = "Report";
            var result3 = _target.Validate(newApp3, dbApp, sended);
            var expected3 = "Пустые строки недопустимы, введите значение в поле Outline";

            Assert.Equal((false, expected), result);
            Assert.Equal((false, expected2), result2);
            Assert.Equal((false, expected3), result3);
        }

        [Fact]
        public void EditAppsValidator_ExcessLength_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            Applications dbApp = fixture.Create<Applications>();
            string sended = "NO";

            EditedAppDTO newApp = fixture.Create<EditedAppDTO>();
            newApp.Name = string.Join(string.Empty, fixture.CreateMany<string>(101));
            newApp.Activity = "Report";
            var result = _target.Validate(newApp, dbApp, sended);
            var expected = "Слишком длинное название! (Name). Значение не должно превышать 100 символов.";

            EditedAppDTO newApp2 = fixture.Create<EditedAppDTO>();
            newApp2.Description = string.Join(string.Empty, fixture.CreateMany<string>(301));
            newApp2.Activity = "Report";
            var result2 = _target.Validate(newApp2, dbApp, sended);
            var expected2 = "Слишком длинное описание! (Description). Значение не должно превышать 300 символов.";

            EditedAppDTO newApp3 = fixture.Create<EditedAppDTO>();
            newApp3.Outline = string.Join(string.Empty, fixture.CreateMany<string>(1001));
            newApp3.Activity = "Report";
            var result3 = _target.Validate(newApp3, dbApp, sended);
            var expected3 = "Слишком длинный план! (Outline). Значение не должно превышать 1000 символов.";

            Assert.Equal((false, expected), result);
            Assert.Equal((false, expected2), result2);
            Assert.Equal((false, expected3), result3);
        }
    }
}
