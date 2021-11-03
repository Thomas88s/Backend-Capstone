using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StonkMarket.Models;
using StonkMarket.Repositories;
using System.Security.Claims;

namespace StonkMarket.Controllers
{
  

    public class StonkController : Controller
    {
        private readonly IStonkRepository _stonkRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserStonkRepository _userStonkRepository;



        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public StonkController(
            IStonkRepository stonkRepository,
            IUserProfileRepository userProfileRepository,
            IUserStonkRepository userStonkRepository)
        {
            _stonkRepository = stonkRepository;
            _userProfileRepository = userProfileRepository;
            _userStonkRepository = userStonkRepository;
        }
       
        // GET: StonkController
        public ActionResult Index()
        {
            List<Stonk> stonks = _stonkRepository.GetAllStonks();
            return View(stonks);
        }

        // GET: StonkController/Details/5
        public ActionResult Details(int id)
        {
            Stonk stonk = _stonkRepository.GetStonkById(id);

            if (stonk == null)
            {
                return NotFound();
            }

            return View(stonk);
        }

       

       
        public ActionResult EditStonk(int id)
        {
            return View();
        }

        // POST: StonkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStonk(int id, IFormCollection collection)
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

        // GET: StonkController/Delete/5
        public ActionResult DeleteStonk(int id)
        {
            return View();
        }

        // POST: StonkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStonk(int id, IFormCollection collection)
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
       
        public ActionResult AddStonk(int id)
        {
            try
            {
                int userId = GetCurrentUserId();
                _stonkRepository.AddStonkToUserStonk(id, userId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

    }
}
