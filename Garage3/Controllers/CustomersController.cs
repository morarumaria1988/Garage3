using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Models.Entities;
using Garage3.Models.Persistence;
using Garage3.Models.ViewModels;

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
            var cs = await _context.Customers
                .OrderBy(c => c.FirstName.Length >= 2 ? c.FirstName.Substring(2) : c.FirstName)
                .Include(c => c.Vehicles)
                .ToListAsync();

            // Sort upper and lowercase letters
            cs.Sort((left, right) => left.FirstName[0].CompareTo(right.FirstName[0]));

            var models = cs.Select( c => new ShowCustomerViewModel { 
                    PersonalNumber = c.PersonalNumber,
                    FullName = c.FullName,
                    Vcount = c.Vehicles.Count
                });

            return View(models);
        }

        public async Task<IActionResult> Filter(string? firstName)
        {
            var query = String.IsNullOrWhiteSpace(firstName) ?
                _context.Customers :
                _context.Customers
                    .Where(c => c.FirstName.Contains(firstName));

            var customers = await query
                .OrderBy(c => c.FirstName.Length >= 2 ? c.FirstName.Substring(2) : c.FirstName)
                    .Include(c => c.Vehicles)
                .ToListAsync();

            // Sort upper and lowercase letters
            customers.Sort((left, right) => left.FirstName[0].CompareTo(right.FirstName[0]));

            var models = customers.Select(c => new ShowCustomerViewModel {
                PersonalNumber = c.PersonalNumber,
                FullName = c.FullName,
                Vcount = c.Vehicles.Count
            }).ToList();

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

                DateTime birthdate = ExtractBirthdateFromPersonalNumber(customer.PersonalNumber);

                int age = CalculateAge(birthdate);

                // Validate age to restrict registration for individuals under 18
                if (age < 18)
                {
                   
                    ModelState.AddModelError(string.Empty, "You must be 18 years or older to register.");
                    return View(customer);
                }

                // Proceed with registration if age is 18 or older
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        private DateTime ExtractBirthdateFromPersonalNumber(string personalNumber)
        {
            if (personalNumber.Length < 10)
            {
                throw new ArgumentException("Invalid personal number format.");
            }

            
            string dateString = personalNumber.Substring(0, 6);

            if (DateTime.TryParseExact(dateString, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime birthdate))
            {
                return birthdate;
            }
            else
            {
                throw new ArgumentException("Unable to parse birthdate from personal number.");
            }
        }

        private int CalculateAge(DateTime birthdate)
        {
            
            int age = DateTime.Today.Year - birthdate.Year;

            // Adjust age if birthday hasnt occured this year
            if (birthdate.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            return age;
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
