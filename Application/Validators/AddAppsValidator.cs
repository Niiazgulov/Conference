using Application.Validators.Contracts;
using Domain.Models;

namespace Application.Validators
{
    public class AddAppsValidator : IAddAppsValidator
    {
        public (bool, string) Validate(NewAppDTO app, bool exists)
        {
            if (exists)
                return (false, "У этого автора уже есть 1 заявка в черновиках, добавить новую невозможно!");
            
            if (app.Author.ToString() == "00000000-0000-0000-0000-000000000000" || app.Author.ToString() == "")
                return (false, "Укажите идентификатор автора в формате Guid! Это обязательное поле.");
           
            if (app.Name != null)
            {
                if (app.Name.Length > 100)
                    return (false, "Слишком длинное название! (Name). Значение не должно превышать 100 символов.");
                if (app.Name == String.Empty)
                    return (false, "Пустые строки недопустимы, введите значение в поле Name");
            }
            if (app.Activity != null)
            {
                if (app.Activity != "Report" && app.Activity != "Masterclass" && app.Activity != "Discussion")
                    return (false, "Некорректный формат поля \"Тип активности\"! (Activity) Выберите один из 3 вариантов - Report, Masterclass, Discussion");
                if (app.Activity == String.Empty)
                    return (false, "Пустые строки недопустимы, введите значение в поле Activity");
            }
            if (app.Description != null)
            {
                if (app.Description.Length > 300)
                    return (false, "Слишком длинное описание! (Description). Значение не должно превышать 300 символов.");
                if (app.Description == String.Empty)
                    return (false, "Пустые строки недопустимы, введите значение в поле Description");
            }
            if (app.Outline != null)
            {
                if (app.Outline.Length > 1000)
                    return (false, "Слишком длинный план! (Outline). Значение не должно превышать 1000 символов.");
                if (app.Outline == String.Empty)
                    return (false, "Пустые строки недопустимы, введите значение в поле Outline");
            }

            var dbCount = NonNullPropertiesCount(app);

            if (dbCount > 1)
            {
                return (true, String.Empty);
            }

            return (false, "Добавьте как минимум 1 поле в заявку кроме Id автора!");
        }

        public int NonNullPropertiesCount(object entity)
        {
            return entity.GetType()
                         .GetProperties()
                         .Select(x => x.GetValue(entity, null))
                         .Count(v => v != null);
        }
    }
}
