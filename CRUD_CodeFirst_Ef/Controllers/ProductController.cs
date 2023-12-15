using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_CodeFirst_Ef.Data;
using CRUD_CodeFirst_Ef.Entities;
using CRUD_CodeFirst_Ef.Models;
using CRUD_CodeFirst_Ef.Services;

namespace CRUD_CodeFirst_Ef.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataDbContext _context;
        private readonly FileService _fileService;

        public ProductController(DataDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }


        public IActionResult Index()
        {
            var products = _context.Products.
                Include(p => p.Category)
                .Include(p => p.ProductImage)
                .ToList();

            var model = new ProductIndexVM
            {
                Products = products
            };


            return View(model);
        }
   

        public IActionResult Add()
        {
            var model = new ProductAddVM();
            var category = _context.Categories.ToList();

            model.Categories = category.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);

        }

        [HttpPost]
        public IActionResult Add(ProductAddVM model)
        {
            if (!ModelState.IsValid)
            {
                //repopulate selectList data!!!
                var category = _context.Categories.ToList();
                model.Categories = category.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                return View(model);
            };

            var imgName = _fileService.UploadFile(model.Photo);
           
            var product = new Product
            {
                Name = model.Name,
                Price = (decimal)model.Price,
                CategoryId = model.CategoryId,
                ProductImage = new ProductImage
                {
                    ProductImageName = imgName
                }
            };

            _context.Products.Add(product);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }




        public IActionResult Delete(int id)
        {
            var product = _context.Products
                .Include(x => x.ProductImage)
                .FirstOrDefault(x => x.Id == id);


            if (product.ProductImage != null)
            {
                _fileService.DeleteFile(product.ProductImage.ProductImageName);
            }

            _context.Products.Remove(product);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Update(int id)
        {

            var product = _context.Products
                .Include(x => x.ProductImage)
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            var categories = _context.Categories.ToList();

            var model = new ProductUpdateVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductImageName = product.ProductImage?.ProductImageName,
                Categories = categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                CategoryId = product.Category.Id
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Update(ProductUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                //repopulate selectList 
                var category = _context.Categories.ToList();
                model.Categories = category.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                return View(model);
            }

            var product = _context.Products
                .Include(x => x.ProductImage)
                .FirstOrDefault(x => x.Id == model.Id);


            if (model.Photo != null)
            {
                if (product.ProductImage != null)
                {
                    _fileService.DeleteFile(product.ProductImage.ProductImageName);
                }

                product.ProductImage.ProductImageName = _fileService.UploadFile(model.Photo);
            }

            product.Name = model.Name;
            product.Price = (decimal)model.Price;
            product.CategoryId = model.CategoryId;

            _context.Products.Update(product);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }






    }
}
