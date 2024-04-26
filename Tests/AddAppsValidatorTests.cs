using Xunit;
using AutoFixture;
using Domain.Models;
using Application.Validators;

namespace Tests
{   
    public class AddAppsValidatorTests
    {
        private AddAppsValidator _target = new();

        [Fact]
        public void AddAppsValidator_ValidArguments_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            NewAppDTO newApp = fixture.Create<NewAppDTO>();
            newApp.Activity = "Report";
            var result = _target.Validate(newApp, false);
            Assert.Equal((true, String.Empty), result);
        }

        [Fact]
        public void AddAppsValidator_WrongActivityField_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            NewAppDTO newApp = fixture.Create<NewAppDTO>();
            var result = _target.Validate(newApp, false);
            var expected = "Некорректный формат поля \"Тип активности\"! (Activity) Выберите один из 3 вариантов - Report, Masterclass, Discussion";
            Assert.Equal((false, expected), result);
        }

        [Fact]
        public void AddAppsValidator_WrongAuthorField_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            NewAppDTO newApp = fixture.Create<NewAppDTO>();
            newApp.Author = new Guid("00000000-0000-0000-0000-000000000000");
            var result = _target.Validate(newApp, false);
            var expected = "Укажите идентификатор автора в формате Guid! Это обязательное поле.";
            Assert.Equal((false, expected), result);
        }

        [Fact]
        public void AddAppsValidator_AuthorAlreadyHasApp_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            NewAppDTO newApp = fixture.Create<NewAppDTO>();
            var result = _target.Validate(newApp, true);
            var expected = "У этого автора уже есть 1 заявка в черновиках, добавить новую невозможно!";
            Assert.Equal((false, expected), result);
        }

        [Fact]
        public void AddAppsValidator_EmptyStrings_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            NewAppDTO newApp = fixture.Create<NewAppDTO>();
            newApp.Name = String.Empty;
            newApp.Activity = "Report";
            var result = _target.Validate(newApp, false);
            var expected = "Пустые строки недопустимы, введите значение в поле Name";

            NewAppDTO newApp2 = fixture.Create<NewAppDTO>();
            newApp2.Description = String.Empty;
            newApp2.Activity = "Report";
            var result2 = _target.Validate(newApp2, false);
            var expected2 = "Пустые строки недопустимы, введите значение в поле Description";

            NewAppDTO newApp3 = fixture.Create<NewAppDTO>();
            newApp3.Outline = String.Empty;
            newApp3.Activity = "Report";
            var result3 = _target.Validate(newApp3, false);
            var expected3 = "Пустые строки недопустимы, введите значение в поле Outline";

            Assert.Equal((false, expected), result);
            Assert.Equal((false, expected2), result2);
            Assert.Equal((false, expected3), result3);
        }

        [Fact]
        public void AddAppsValidator_ExcessLength_ValidatedCorrectly()
        {
            Fixture fixture = new Fixture();

            NewAppDTO newApp = fixture.Create<NewAppDTO>();
            newApp.Name = string.Join(string.Empty, fixture.CreateMany<string>(101));
            newApp.Activity = "Report";
            var result = _target.Validate(newApp, false);
            var expected = "Слишком длинное название! (Name). Значение не должно превышать 100 символов.";
            
            NewAppDTO newApp2 = fixture.Create<NewAppDTO>();
            newApp2.Description = string.Join(string.Empty, fixture.CreateMany<string>(301));
            newApp2.Activity = "Report";
            var result2 = _target.Validate(newApp2, false);
            var expected2 = "Слишком длинное описание! (Description). Значение не должно превышать 300 символов.";
            
            NewAppDTO newApp3 = fixture.Create<NewAppDTO>();
            newApp3.Outline = string.Join(string.Empty, fixture.CreateMany<string>(1001));
            newApp3.Activity = "Report";
            var result3 = _target.Validate(newApp3, false);
            var expected3 = "Слишком длинный план! (Outline). Значение не должно превышать 1000 символов.";
            
            Assert.Equal((false, expected), result);
            Assert.Equal((false, expected2), result2);
            Assert.Equal((false, expected3), result3);
        }

        [Fact]
        public void AddAppsValidator_OnlyAuthorField_ValidatedCorrectly()
        {
            NewAppDTO newApp = new();
            newApp.Author = Guid.NewGuid();
            var result = _target.Validate(newApp, false);
            var expected = "Добавьте как минимум 1 поле в заявку кроме Id автора!";
            Assert.Equal((false, expected), result);
        }
    }
}
