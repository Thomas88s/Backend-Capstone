using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StonkMarket.Models;
using StonkMarket.Models.ViewModels;
using StonkMarket.Repositories;
using System.Collections.Generic;
using System.Security.Claims;



namespace StonkMarket.Controllers
{
    public class MessageController : Controller
    {
       
        private readonly IMessageRepository _messageRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public MessageController(IMessageRepository messageRepository,
             IUserProfileRepository userProfileRepository)
        {
            _messageRepository = messageRepository;
            _userProfileRepository = userProfileRepository;
        }

        // GET: MessageController
        public ActionResult Index()
        {
            List<UserProfile> userProfiles = _userProfileRepository.GetAllUserProfiles();
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Message> messages = _messageRepository.GetAllMessagesByUserId(userProfileId);
            return View(messages);
        }

        // GET: MessageController/Details/5
        public ActionResult Details()
        {
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Message> messages = _messageRepository.GetAllMessagesByUserId(userProfileId);
         
            return View(messages);
        }

        // Post: MessageController/Create
        public ActionResult Create(Message message)
        {
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<UserProfile> userProfiles = _userProfileRepository.GetAllUserProfiles();
            UserProfile userProfile = _userProfileRepository.GetById(userProfileId);
            
            MessageViewModel vm = new MessageViewModel()
            {

                Message = new Message(),
                UserProfiles = userProfiles,
                
            };

            return View(vm);
        }

        // POST: MessageController/Create
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

        // GET: MessageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MessageController/Edit/5
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

        // GET: MessageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MessageController/Delete/5
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

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
