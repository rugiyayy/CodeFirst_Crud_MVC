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

namespace CRUD_CodeFirst_Ef.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DataDbContext _context;

        public CustomerController(DataDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();

            var model = new CustomerIndexVM
            {
                Customers = customers
            };

            return View(model);
        }





        public IActionResult Add()
        {
            var model = new CustomerAddVM();

            return View(model);
        }




        [HttpPost]
        public IActionResult Add(CustomerAddVM model)
        {
            if (!ModelState.IsValid) return View(model);


            if (_context.Customers.Any(c => c.FirstName == model.FirstName && c.LastName == model.LastName && c.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Customer with the same First Name, Last Name and Email already exists.");
                return View(model);
            }

            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            _context.Customers.Add(customer);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }




        public IActionResult Delete(int id)
        {
            var customer = _context.Customers
                .FirstOrDefault(x => x.Id == id);


            _context.Customers.Remove(customer);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }





        public IActionResult Update(int id)
        {
            var customer = _context.Customers
                .FirstOrDefault(x => x.Id == id);

            if (customer is null) return NotFound();



            var model = new CustomerUpdateVM
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email

            };


            return View(model);
        }





        [HttpPost]
        public IActionResult Update(CustomerUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var customer = _context.Customers
                .FirstOrDefault(x => x.Id == model.Id);

            if (customer is null) return NotFound();


            if (_context.Customers.Any(c => c.FirstName == model.FirstName && c.LastName == model.LastName && c.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Customer with the same First Name, Last Name and Email already exists.");
                return View(model);
            }

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;


            _context.Customers.Update(customer);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }


    }
}
