using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD_CodeFirst_Ef.Data;
using CRUD_CodeFirst_Ef.Entities;
using CRUD_CodeFirst_Ef.Models;
using CRUD_CodeFirst_Ef.Services;

namespace CRUD_CodeFirst_Ef.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataDbContext _context;

        public CategoryController(DataDbContext context)
        {
            _context = context;
        }




        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();

            var model = new CategoryIndexVM
            {
                Categories = categories
            };

            return View(model);
        }





        public IActionResult Add()
        {
            var model = new CategoryAddVM();

            return View(model);
        }



        [HttpPost]
        public IActionResult Add(CategoryAddVM model)
        {
            if (!ModelState.IsValid) return View(model);


            if (_context.Categories.Any(c => c.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Category with the same name already exists.");

                return View(model);
            }
            else
            {
                var category = new Category { Name = model.Name };

                _context.Categories.Add(category);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

        }




        public IActionResult Delete(int id)
        {
            var category = _context.Categories
                .FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();

            _context.Categories.Remove(category);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }






        public IActionResult Update(int id)
        {
            var category = _context.Categories
                .FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();



            var model = new CategoryUpdateVM
            {
                Id = category.Id,
                Name = category.Name,
               
            };


            return View(model);
        }





        [HttpPost]
        public IActionResult Update(CategoryUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = _context.Categories
                .FirstOrDefault(x => x.Id == model.Id);

            if (_context.Categories.Any(c => c.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Category with the same name already exists.");

                return View(model);
            }

            category.Name = model.Name;
          

            _context.Categories.Update(category);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }




    }
}
