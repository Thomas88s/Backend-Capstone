using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StonkMarket.Models;
using StonkMarket.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StonkMarket.Controllers
{
    public class UserStonkController : Controller
    {
        private readonly IUserStonkRepository _userStonkRepository;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public UserStonkController(IUserStonkRepository userStonkRepository)
        {
            _userStonkRepository = userStonkRepository;
        }

        // GET: UserStonkController
        public ActionResult Index()
        {
            List<UserStonk> userStonks = _userStonkRepository.GetAllUserStonks();
            return View(userStonks);
        }

        // GET: UserStonkController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserStonkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserStonkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserStonkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserStonkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserStonkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserStonkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
