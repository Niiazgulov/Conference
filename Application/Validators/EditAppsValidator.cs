using Application.Validators.Contracts;
using Domain.Models;

namespace Application.Validators
{
    public class EditAppsValidator : IEditAppsValidator
    {
        public (bool, string) Validate(EditedAppDTO app, Applications? dbApp, string? sended)
        {
            if (dbApp == null)
                return (false, "ОШИБКА! Такой заявки не существует!");

            if (sended == "YES")
                return (false, "ОШИБКА! Невозможно выполнить, заявка уже направлена на рассмотрение.");

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

            return (true, String.Empty);
        }
    }
}
