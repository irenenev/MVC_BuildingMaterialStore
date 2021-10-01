using Microsoft.AspNetCore.Mvc;
using Store_Models;
using Store_Models.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Store_Utility;
using Store_DataAccess.Repository.IRepository;

namespace Rocky.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _prodRepo;
        private readonly IWebHostEnvironment _env;
        public ProductController(IProductRepository prodRepo, IWebHostEnvironment env)
        {
            _prodRepo = prodRepo;
            _env = env;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objList = _prodRepo.GetAll(includeProperties:"Category,ApplicationType");
            return View(objList);
        }
        //GET - Upsert (Create + Edit)
        public IActionResult Upsert(int? id)
        {
            ProductVM _product = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _prodRepo.GetAllDropdownList(WebConstants.CategoryName),
                ApplicationTypeSelectList = _prodRepo.GetAllDropdownList(WebConstants.ApplicationTypeName)
            };
            if (id == null) 
                return View(_product); //for create
            else
            {
                _product.Product = _prodRepo.Find(id.GetValueOrDefault()); //for viewmodel
                if (_product.Product == null) //for viewmodel
                    return NotFound();
                return View(_product); //for edit
            }
        }
        //POST - Upsert
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Upsert(ProductVM _product)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string path = _env.WebRootPath;
                if (_product.Product.Id == 0) //for create
                {
                    string upload = path + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension),
                        FileMode.Create)) 
                    {
                        files[0].CopyTo(fileStream);
                    }
                    _product.Product.Image = fileName + extension;
                    _prodRepo.Add(_product.Product);
                }
                else //for edit
                {
                    var obj = _prodRepo.FirstOrDefault(u => u.Id == _product.Product.Id, isTracking:false);
                    if (files.Count > 0)
                    {
                        string upload = path + WebConstants.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        var oldFile = Path.Combine(upload, obj.Image);
                        if (System.IO.File.Exists(oldFile)) System.IO.File.Delete(oldFile);
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension),
                        FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        _product.Product.Image = fileName + extension;
                    }
                    else 
                    {
                        _product.Product.Image = obj.Image;
                    }
                    _prodRepo.Update(_product.Product);
                }
                _prodRepo.Save();
                return RedirectToAction("Index");
            }
            _product.CategorySelectList = _prodRepo.GetAllDropdownList(WebConstants.CategoryName);
            _product.ApplicationTypeSelectList = _prodRepo.GetAllDropdownList(WebConstants.ApplicationTypeName);
            return View(_product);
        }
        //GET - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _prodRepo.FirstOrDefault(u => u.Id == id, includeProperties:"Category, ApplicationType");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public IActionResult DeletePost(int? id)
        {
            var obj = _prodRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            string upload = _env.WebRootPath + WebConstants.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);
            if (System.IO.File.Exists(oldFile)) System.IO.File.Delete(oldFile);

            _prodRepo.Remove(obj);
            _prodRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
