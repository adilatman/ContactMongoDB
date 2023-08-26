using ContactMongoDB.UI.DAL;
using ContactMongoDB.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMongoDB.UI.Controllers
{
    public class HomeController : Controller
    {
        ContactManager contactManager { get; set; }

        public HomeController(ContactManager contactManager)
        {
            this.contactManager=contactManager;
        }

        public IActionResult Index()
        {
            var contacts = contactManager.GetList();
            return View(contacts);
        }

        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            contactManager.Create(contact);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateContact(string Id)
        {
            var contact = contactManager.GetById(Id);
            return View(contact);
        }
        [HttpPost]
        public IActionResult UpdateContact(string Id, Contact contact)
        {
            var item = contactManager.GetById(Id);
            item.Name = contact.Name;
            item.Surname = contact.Surname;
            item.Phone = contact.Phone;
            item.IsActive = contact.IsActive;
            contactManager.Update(Id, item);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteContact(string Id)
        {
            var contact = contactManager.GetById(Id);
            return View(contact);
        }
        [HttpPost]
        public IActionResult DeleteContact(string Id, IFormCollection collection)
        {
            contactManager.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
