using HotDesc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HotDesc.Repositories
{
    public class WorkplaceRepository : IWorkplaceRepository
    {
        private ApplicationContext db;
        public WorkplaceRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Workplace> GetWorkplaceById(int id)
        {
            return await db.Workplaces.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Workplace> GetWorkplaceByCurrentUserId(int id)
        {
            var reservation = await db.Reservations.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();

            return await db.Workplaces.Where(x => x.ReservationId == reservation.Id).FirstOrDefaultAsync();
        }

        public Task<List<Workplace>> GetAllData()
        {
            return db.Workplaces.ToListAsync();
        }

        public async Task ReserveWorkplace(int id, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook, int userId)
        {
            string chosenDevices = "";

            if (mouse)
                chosenDevices += "mouse ";
            if (keyboard)
                chosenDevices += "keyboard ";
            if (systemUnit)
                chosenDevices += "systemUnit ";
            if (monitor)
                chosenDevices += "monitor ";
            if (notebook)
                chosenDevices += "notebook ";

            var workplace = db.Workplaces.Where(x => x.Id == Convert.ToInt32(id)).FirstOrDefault();

            workplace.IsInUse = true;
            workplace.Devices = chosenDevices;
            var reservation = new Reservation { EmployeeId = userId, Date = DateTime.Now.Date };
            workplace.Reservation = reservation;

            db.Reservations.Add(reservation);
            await db.SaveChangesAsync();
        }

        public async Task AddWorkplace(string description, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook)
        {
            var workplace = new Workplace();

            string chosenDevices = "";

            if (mouse)
                chosenDevices += "mouse ";
            if (keyboard)
                chosenDevices += "keyboard ";
            if (systemUnit)
                chosenDevices += "systemUnit ";
            if (monitor)
                chosenDevices += "monitor ";
            if (notebook)
                chosenDevices += "notebook ";

            workplace.Description = description;
            workplace.Devices = chosenDevices;

            db.Workplaces.Add(workplace);
            await db.SaveChangesAsync();
        }

        public async Task EditWorkplace(Workplace givenWorkplace, string description, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook)
        {
            Workplace workplace = givenWorkplace;
            string chosenDevices = "";

            if (mouse)
                chosenDevices += "mouse ";
            if (keyboard)
                chosenDevices += "keyboard ";
            if (systemUnit)
                chosenDevices += "systemUnit ";
            if (monitor)
                chosenDevices += "monitor ";
            if (notebook)
                chosenDevices += "notebook ";

            workplace.Description = description;
            workplace.Devices = chosenDevices;

            await db.SaveChangesAsync();
        }

        public async Task DeleteWorkplace(Workplace workplace)
        {
            if (workplace.ReservationId != null)
            {
                var reservation = db.Reservations.Where(x => x.Id == workplace.ReservationId).FirstOrDefault();
                db.Remove(reservation);
            }
            db.Remove(workplace);
            await db.SaveChangesAsync();
        }
    }
}