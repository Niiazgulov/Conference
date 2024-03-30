using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Contracts
{
    public interface IRepositoryHandler
    {
        Task<Applications> AddApps(NewAppDTO app);
        Task<Applications> EditApps(Guid id, EditedAppDTO app);
        Task DeleteApps(Guid id);
    }
}
