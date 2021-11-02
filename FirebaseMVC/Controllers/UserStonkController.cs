using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StonkMarket.Models;
using StonkMarket.Models.ViewModels;
using StonkMarket.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userProfile = _userProfileRepository.GetById(userProfileId);

            List<UserStonk> userStonks = _userStonkRepository.GetAllUserStonksById(userProfileId);
            List<Stonk> stonks = _stonkRepository.GetAllStonks();
            List<UserStonk> noDupes = userStonks.Distinct().ToList();
           


            UserStonksViewModel vm = new UserStonksViewModel()
            {
                UserProfile = userProfile,
                UserStonk = userStonks,
                Stonks = stonks
            };

            return View(noDupes);
        }

        // GET: UserStonkController/TopIndex
        public ActionResult TopIndex()
        {
            List<UserStonk> topStonks = _userStonkRepository.GetTopStonks();
            List<UserStonk> noDupes = topStonks.Distinct().ToList();

            return View(noDupes);
        }

        // GET: UserStonkController/Details/5
        public ActionResult Details(int id)
        {

            UserStonk userStonk = _userStonkRepository.GetUserStonkById(id);

            if (userStonk == null)
            {
                return NotFound();
            }

            return View(userStonk);
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
                //_userStonkRepository.Add(userStonk);
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
        public ActionResult AddTopStonk(int id)
        {
            try
            {
                int userId = GetCurrentUserId();
                _userStonkRepository.AddTopStonkToUserStonk(id, userId);
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
