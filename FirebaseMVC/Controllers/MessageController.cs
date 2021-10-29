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
            List<Message> messages = _messageRepository.GetAllMessages();
            return View(messages);
        }

        // GET: MessageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MessageController/Create
        public ActionResult Create()
        {
            return View();
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
    }
}
