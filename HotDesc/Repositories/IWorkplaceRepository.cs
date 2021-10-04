using HotDesc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesc.Repositories
{
    public interface IWorkplaceRepository
    {
        Task<Workplace> GetWorkplaceById(int id);
        Task<Workplace> GetWorkplaceByCurrentUserId(int id);
        Task<List<Workplace>> GetAllData();
        Task ReserveWorkplace(int id, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook, int userId);
        Task AddWorkplace(string description, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook);
        Task EditWorkplace(Workplace givenWorkplace, string description, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook);
        Task DeleteWorkplace(Workplace workplace);
    }
}
