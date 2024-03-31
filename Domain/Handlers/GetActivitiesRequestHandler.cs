using Domain.Handlers.Contracts;

namespace Domain.Handlers
{
    public class GetActivitiesRequestHandler : IGetActivitiesRequestHandler
    {
        public Activities[] _activities =
        {
            new Activities(){Activity = "Report", Description = "Доклад, 35-45 минут" },
            new Activities(){Activity = "Masterclass", Description = "Мастеркласс, 1-2 часа" },
            new Activities(){Activity = "Discussion", Description = "Дискуссия / круглый стол, 40-50 минут" },
        };

        public Activities[] GetActivities()
        {
            return _activities;
        }
    }
}
