using HotDesc.Models;
using HotDesc.Repositories;
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

namespace HotDesc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkplaceRepository _workplaceRepository;
        private readonly IReservationRepository _reservationRepository;
        public HomeController(IWorkplaceRepository workplaceRepository, IReservationRepository reservationRepository)
        {
            _workplaceRepository = workplaceRepository;
            _reservationRepository = reservationRepository;
        }

        [Authorize(Roles = "admin, user")]
        public void DeleteReservationOnStart()
        {
            _reservationRepository.DeleteReservationOnStart();
        }

        [Authorize(Roles = "admin, user")]
        public IActionResult RedirectUser()
        {
            //todo Достучаться до роли

            DeleteReservationOnStart();
            if (User.Identity.Name == "admin")
                return RedirectToAction("Index", "Admin");
            else 
                return RedirectToAction("MyWorkplace", "Home");
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> MyWorkplace()
        {
            int userId = Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value);

            if (!await _reservationRepository.IsUserInTable(userId))
                return RedirectToAction("Index", "Home");

            var workplace = await _workplaceRepository.GetWorkplaceByCurrentUserId(userId);

            ViewBag.Workplace = workplace.Description;
            ViewBag.Devices = workplace.Devices;

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        //todo Менял
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _workplaceRepository.GetAllData());
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public IActionResult Index(int id, bool mouse, bool keyboard, bool systemUnit, bool monitor, bool notebook)
        {
            int userId = Convert.ToInt32(User.Claims.First(x => x.Type == "Id").Value);
            _workplaceRepository.ReserveWorkplace(id, mouse, keyboard, systemUnit, monitor, notebook, userId);
            return RedirectToAction("MyWorkplace", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
