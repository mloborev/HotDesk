using HotDesc.Models;
using HotDesc.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWorkplaceRepository _workplaceRepository;
        private readonly IReservationRepository _reservationRepository;
        public AdminController(IWorkplaceRepository workplaceRepository, IReservationRepository reservationRepository)
        {
            this._workplaceRepository = workplaceRepository;
            this._reservationRepository = reservationRepository;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> AddWorkplace()
        {
            return View(await _workplaceRepository.GetAllData());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddWorkplace(string description, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook)
        {
            await _workplaceRepository.AddWorkplace(description, mouse, keyboard, systemUnit, monitor, notebook);

            return View(await _workplaceRepository.GetAllData());
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditWorkplace()
        {
            return View(await _workplaceRepository.GetAllData());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditWorkplace(int id, string description, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook)
        {
            var workplace = await _workplaceRepository.GetWorkplaceById(id);
            await _workplaceRepository.EditWorkplace(workplace, description, mouse, keyboard, systemUnit, monitor, notebook);

            return View(await _workplaceRepository.GetAllData());
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteWorkplace()
        {
            return View(await _workplaceRepository.GetAllData());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteWorkplace(int id)
        {
            var workplace = await _workplaceRepository.GetWorkplaceById(id);
            await _workplaceRepository.DeleteWorkplace(workplace);

            return View(await _workplaceRepository.GetAllData());
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteReservation()
        {
            return View(await _reservationRepository.GetAllData());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationRepository.DeleteReservation(id);
            return View(await _reservationRepository.GetAllData());           
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
