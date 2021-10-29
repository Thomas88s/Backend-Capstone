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
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IStonkRepository _stonkRepository;
        private readonly IMessageRepository _messageRepository;

        // ASP.NET will give us an instance of our Repository. This is called "Dependency Injection"
        public UserStonkController(
            IUserProfileRepository userProfileRepository,
            IUserStonkRepository userStonkRepository,
            IStonkRepository stonkRepository,
            IMessageRepository messageRepository)
        {
            _userStonkRepository = userStonkRepository;
            _userProfileRepository = userProfileRepository;
            _stonkRepository = stonkRepository;
            _messageRepository = messageRepository;
        }

        // GET: UserStonkController
        public ActionResult Index()
        {
            List<UserStonk> userStonks = _userStonkRepository.GetAllUserStonks();
            return View(userStonks);
        }

        // GET: UserStonkController/TopIndex
        public ActionResult TopIndex()
        {
            List<UserStonk> topStonks = _userStonkRepository.GetTopStonks();
            return View(topStonks);
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
        public ActionResult Create(UserStonk userStonk)
        {
            try
            {
                _userStonkRepository.Add(userStonk);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: UserStonkController/Edit/5
        public ActionResult Edit(int id)
        {
            UserStonk userStonk = _userStonkRepository.GetUserStonkById(id);

            if (userStonk == null)
            {
                return NotFound();
            }

            return View(userStonk);
        }

     

        // POST: UserStonkController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserStonk userStonk)
        {
            try
            {
                _userStonkRepository.Update(userStonk);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(userStonk);
            }
        }


        // GET: UserStonkController/Delete/5
        public ActionResult Delete(int id)
        {
            UserStonk userStonk = _userStonkRepository.GetUserStonkById(id);

            return View(userStonk);
        }

        // POST: UserStonkController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserStonk userStonk)
        {
            try
            {
                _userStonkRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(userStonk);
            }
        }
    }
}
