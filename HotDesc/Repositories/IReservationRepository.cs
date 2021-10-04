using HotDesc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesc.Repositories
{
    public interface IReservationRepository
    {
        Task DeleteReservationOnStart();
        Task DeleteReservation(int id);
        Task<List<Reservation>> GetAllData();
        Task<bool> IsUserInTable(int userId);
    }
}
