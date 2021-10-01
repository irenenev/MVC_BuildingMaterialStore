using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store_DataAccess.Repository.IRepository;
using Store_Models;
using Store_Utility;
using System.Collections.Generic;

namespace Rocky.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _appRepo;
        public ApplicationTypeController(IApplicationTypeRepository appRepo)
        {
            _appRepo = appRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _appRepo.GetAll();
            return View(objList);
        }
        //GET - Create
        public IActionResult Create()
        {
            return View();
        }
        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken] //для защиты данных
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appRepo.Add(obj);
                _appRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _appRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken] //для защиты данных
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appRepo.Update(obj);
                _appRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _appRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken] //для защиты данных
        public IActionResult DeletePost(int? id)
        {
            var obj = _appRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            _appRepo.Remove(obj);
            _appRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
