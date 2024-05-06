using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Models.Entities;
using Garage3.Models.Persistence;
using Garage3.Models;

namespace Garage3.Controllers
{
    public class CustomersController : Controller
    {
        private readonly GarageMVCContext _context;

        public CustomersController(GarageMVCContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
                .Include(c => c.Vehicles)
                .OrderBy(c => c.FirstName.Length >= 2 ? c.FirstName.Substring(2) : c.FirstName)
                .ToListAsync();

            var models = customers.Select(c => new CustomerIndexViewModel
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PersonalNumber = c.PersonalNumber,
                VehicleCount = c.Vehicles.Count()
            });

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string? firstName)
        {
            var customers = firstName is not null ?
                await _context.Customers
                    .Where(c => c.FirstName.Contains(firstName))
                    .Include(c => c.Vehicles)
                    .OrderBy(c => c.FirstName.Length >= 2 ? c.FirstName.Substring(2) : c.FirstName)
                    .ToListAsync() :
                await _context.Customers
                    .Include(c => c.Vehicles)
                    .OrderBy(c => c.FirstName.Length >= 2 ? c.FirstName.Substring(2) : c.FirstName)
                    .ToListAsync();

            var models = customers.Select(c => new CustomerIndexViewModel
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PersonalNumber = c.PersonalNumber,
                VehicleCount = c.Vehicles.Count()
            });

            return View(nameof(Index), models);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.PersonalNumber == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalNumber,FirstName,LastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonalNumber,FirstName,LastName")] Customer customer)
        {
            if (id != customer.PersonalNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.PersonalNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.PersonalNumber == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.PersonalNumber == id);
        }
    }
}
