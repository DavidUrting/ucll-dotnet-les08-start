using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using AdventureWorks.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Web.Controllers
{
    public class CustomerController : Controller
    {
        // Tonen van een zoekcriteria scherm
        public IActionResult Search()
        {
            SearchViewModel vm = new SearchViewModel();
            return View("SearchCriteria", vm);
        }

        // Tonen van zoekresultaten
        [HttpPost]
        public IActionResult Search(string keyword)
        {
            CustomerManager cm = new CustomerManager();
            var customers = cm.SearchCustomers(keyword)
                .Select(c => new CustomerViewModel() 
                { 
                    Id = c.Id, 
                    FirstName = c.FirstName, 
                    LastName = c.LastName,
                    Email = c.Email
                });
            return View("SearchResults", customers);
        }

        public IActionResult SearchV2()
        {
            SearchViewModel vm = new SearchViewModel();
            return View(vm);
        }

        public IActionResult SearchV2_Json(string keyword)
        {
            CustomerManager cm = new CustomerManager();
            var customers = cm.SearchCustomers(keyword)
                .Select(c => new CustomerViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email
                });
            return Json(customers);
        }

        // Tonen van details van een klant
        public IActionResult Details(int id)
        {
            CustomerManager cm = new CustomerManager();
            Customer c = cm.GetCustomer(id);
            if (c != null)
            {
                CustomerViewModel vm = new CustomerViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                };
                return View(vm);
            }
            else return NotFound();
        }

    }
}