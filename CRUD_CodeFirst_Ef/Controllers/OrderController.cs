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

namespace CRUD_CodeFirst_Ef.Controllers;

    public class OrderController : Controller
    {
        private readonly DataDbContext _context;

        public OrderController(DataDbContext context)
        {
            _context = context;
        }




        public IActionResult Index()
        {
            var orders = _context.Orders.
                Include(p => p.Product)
                .Include(c => c.Customer)
                .ToList();

            var model = new OrderIndexVM
            {
                Orders = orders
            };


            return View(model);
        }






        public IActionResult Add()
        {
            var model = new OrderAddVM();
            var customer = _context.Customers.ToList();
            var product = _context.Products.ToList();


            model.Customers = customer.Select(x => new SelectListItem
            {
                Text = x.LastName + " " + x.FirstName,
                Value = x.Id.ToString()
            }).ToList();

            model.Products = product.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);

        }





        [HttpPost]
        public IActionResult Add(OrderAddVM model)
        {
            if (!ModelState.IsValid)
            {
            //repopulate list items
                var customer = _context.Customers.ToList();
                model.Customers = customer.Select(x => new SelectListItem
                {
                    Text = x.LastName + " " + x.FirstName,
                    Value = x.Id.ToString()
                        
                }).ToList();


                var product = _context.Products.ToList();
                model.Products = product.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            return View(model);
            }


        var order = new Order
            {
                CustomerId = model.CustomerId,
                ProductId=model.ProductId,
                OrderDate= (DateTime)model.OrderDate
            };

            _context.Orders.Add(order);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }





        public IActionResult Update(int id)
        {
            var order = _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);

            if (order is null) return NotFound();

            var customer = _context.Customers.ToList();
            var product = _context.Products.ToList();


            var model = new OrderUpdateVM
            {
                OrderDate = order.OrderDate,

                Customers = customer.Select(x => new SelectListItem
                {
                    Text = x.FirstName+" "+x.LastName,
                    Value = x.Id.ToString()
                }).ToList(),

                CustomerId = order.Customer.Id,

                 Products = product.Select(x => new SelectListItem
                 {
                     Text = x.Name,
                     Value = x.Id.ToString()
                 }).ToList(),

                ProductId = order.Product.Id


            };

            return View(model);
        }



    [HttpPost]
    public IActionResult Update(OrderUpdateVM model)
    {


        if (!ModelState.IsValid)
        {
            //repopulate listitems
            var customer = _context.Customers.ToList();
            model.Customers = customer.Select(x => new SelectListItem
            {
                Text = x.LastName + " " + x.FirstName,
                Value = x.Id.ToString()

            }).ToList();


            var product = _context.Products.ToList();
            model.Products = product.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }
           

        var order = _context.Orders
                       .Include(x => x.Customer)
                       .Include(x => x.Product)
                       .FirstOrDefault(x => x.Id == model.Id);

        if (order is null) return NotFound();

       

        
        order.OrderDate = (DateTime)model.OrderDate;
        order.CustomerId = model.CustomerId;
        order.ProductId = model.ProductId;

        _context.Orders.Update(order);

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));

    }






    public IActionResult Delete(int id)
    {
        var order = _context.Orders
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .FirstOrDefault(x => x.Id == id);

        if (order is null) return NotFound();

        
        _context.Orders.Remove(order);

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }



}

