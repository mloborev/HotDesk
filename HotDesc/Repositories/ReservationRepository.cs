using HotDesc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesc.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private ApplicationContext db;
        public ReservationRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task DeleteReservationOnStart()
        {
            while (db.Reservations.Any(x => x.Date != DateTime.Now.Date))
            {
                var reservation = db.Reservations.Where(x => x.Date != DateTime.Now.Date).FirstOrDefault();
                var workplace = db.Workplaces.Where(x => x.ReservationId == reservation.Id).FirstOrDefault();

                workplace.ReservationId = null;
                workplace.IsInUse = false;
                workplace.Devices = null;
                db.Remove(reservation);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = db.Reservations.Where(x => x.Id == id).FirstOrDefault();
            var workplace = db.Workplaces.Where(x => x.ReservationId == id).FirstOrDefault();
            workplace.ReservationId = null;
            workplace.IsInUse = false;
            workplace.Devices = null;
            db.Remove(reservation);
            await db.SaveChangesAsync();
        }

        public Task<List<Reservation>> GetAllData()
        {
            return db.Reservations.ToListAsync();
        }

        public async Task<bool> IsUserInTable(int userId)
        {
            return await db.Reservations.AnyAsync(x => x.EmployeeId == userId);
        }
    }
}
