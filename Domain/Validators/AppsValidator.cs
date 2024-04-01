using Domain.Validators.Contracts;

namespace Domain.Validators
{
    public class AppsValidator : IAppsValidator
    {
        public (bool, string) Validate(NewAppDTO app)
        {
            if (app.Author.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                return (false, "Укажите идентификатор автора в формате Guid! Это обязательное поле.");
            }
            if (app.Name != null)
            {
                if (app.Name.Length > 100)
                    return (false, "Слишком длинное название! (Name). Значение не должно превышать 100 символов.");
            }
            if (app.Activity != null)
            {  
                if (app.Activity != "Report" && app.Activity != "Masterclass" && app.Activity != "Discussion")
                    return (false, "Некорректный формат поля \"Тип активности\"! (Activity) Выберите один из 3 вариантов - Report, Masterclass, Discussion"); 
            }
            if (app.Description != null)
            {
                if (app.Description.Length > 300)
                    return (false, "Слишком длинное описание! (Description). Значение не должно превышать 300 символов.");
            }
            if (app.Outline != null)
            {
                if (app.Outline.Length > 1000)
                    return (false, "Слишком длинный план! (Outline). Значение не должно превышать 1000 символов.");
            }

            return (true, "");
        }
    }
}
