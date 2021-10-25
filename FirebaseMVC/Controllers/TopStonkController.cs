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
    public class TopStonkController : Controller
    {
        private readonly ITopStonkRepository _topStonkRepository;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public TopStonkController(ITopStonkRepository topStonkRepository)
        {
            _topStonkRepository = topStonkRepository;
        }

        // GET: TopStonkController
        public ActionResult Index()
        {
            List<TopStonk> topStonks = _topStonkRepository.GetAllTopStonks();
            return View(topStonks);
        }

        // GET: TopStonkController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TopStonkController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TopStonkController/Create
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

        // GET: TopStonkController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TopStonkController/Edit/5
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

        // GET: TopStonkController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TopStonkController/Delete/5
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
