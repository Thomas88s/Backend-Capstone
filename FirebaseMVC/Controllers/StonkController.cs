using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StonkMarket.Models;
using StonkMarket.Repositories;


namespace StonkMarket.Controllers
{
  

    public class StonkController : Controller
    {
        private readonly IStonkRepository _stonkRepository;
        private readonly IUserProfileRepository _userProfileRepository;

      

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public StonkController(
            IStonkRepository stonkRepository,
            IUserProfileRepository userProfileRepository)
        {
            _stonkRepository = stonkRepository;
            _userProfileRepository = userProfileRepository;
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
            return View();
        }

        // GET: StonkController/Create
        public ActionResult CreateStonk()
        {
            return View();
        }

        // POST: StonkController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStonk(Stonk stonk)
        {
            try
            {
                _stonkRepository.AddStonk(stonk);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(stonk);
            }
        }

        // GET: StonkController/Edit/5
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
    }
}
