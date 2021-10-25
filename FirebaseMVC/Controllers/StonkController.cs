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

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public StonkController(IStonkRepository stonkRepository)
        {
            _stonkRepository = stonkRepository;
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: StonkController/Create
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

        // GET: StonkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StonkController/Edit/5
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

        // GET: StonkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StonkController/Delete/5
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
