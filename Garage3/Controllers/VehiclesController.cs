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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;

namespace Garage3.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly GarageMVCContext _context;

        public VehiclesController(GarageMVCContext context)
        {
            _context = context;
      
        }

        // Helper 
        private async Task LoadViewData() {

            string[] vTypeOptions = await _context.VTypes.Select(v => v.Name).ToArrayAsync();
            ViewData["VTypeOptions"] = vTypeOptions.Append("Other");
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {

           var list = await _context.Vehicles.Select( v => new ShowVehicleViewModel { 
                RegistrationNumber = v.RegistrationNumber,
                OwnerFullName = v.Member.FullName,
                Color = v.Color,
                Make = v.Make,
                TypeName = v.TypeName,
                NumberOfWheels = v.NumberOfWheels,
                IsParked = (v.ArrivalTime != null)

            }).ToListAsync();

           var parkedCount = await _context.Vehicles.Where(v => v.ArrivalTime != null).CountAsync();
            ViewData["ParkedCount"] = parkedCount;

            return View(list);
        }

    // GET: Vehicles/Details/5
    public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public async Task<IActionResult> Create()
        {
            await LoadViewData();
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationNumber,PersonalNumber,VType,VTypeCustom, Color,Make,NumberOfWheels,ArrivalTime")] CreateVehicleViewModel vehicleViewModel)

        {

            var vtyp = new VehicleType();
            vtyp.Name = (vehicleViewModel.VType == "Other") ? vehicleViewModel.VTypeCustom : vehicleViewModel.VType;

            Vehicle v = new Vehicle();
            v.ArrivalTime = vehicleViewModel.ArrivalTime;
            v.RegistrationNumber = vehicleViewModel.RegistrationNumber;
            v.PersonalNumber = vehicleViewModel.PersonalNumber;
            v.NumberOfWheels = vehicleViewModel.NumberOfWheels;
            v.Color = vehicleViewModel.Color;
            v.Make = vehicleViewModel.Make;
            v.TypeName = vtyp.Name;


            if (ModelState.IsValid)
            {
                if (_context.Customers.FirstOrDefault(c => c.PersonalNumber == vehicleViewModel.PersonalNumber) == null)
                {

                    ModelState.AddModelError(string.Empty, "This user personal number dont exist in our database");

                }

                else
                {
                    if (!_context.VTypes.Contains(vtyp))
                    {
                        _context.Add(vtyp);
                    }

                    var stored = _context.Vehicles.FirstOrDefault(sv => sv.RegistrationNumber == v.RegistrationNumber);
                    if (stored == null)
                    {
                        _context.Add(v);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "This vehcile registration number already exists");
                    }
                }
            }
           await LoadViewData();
            return View();
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RegistrationNumber,Color,Make,NumberOfWheels,ArrivalTime")] Vehicle vehicle)
        {
            if (id != vehicle.RegistrationNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.RegistrationNumber))
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
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicles/Park/5
        public async Task<IActionResult> Park(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.ArrivalTime = DateTime.Now;
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            // return View(vehicle);
        }

        // GET: Vehicles/CheckOut/5
        public async Task<IActionResult> CheckOut(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            Receipt receipt = new Receipt();
            receipt.ArrivalTime = vehicle.ArrivalTime ?? DateTime.Now;
            receipt.DepartureTime = DateTime.Now;
            receipt.RegistrationNumber = vehicle.RegistrationNumber;
            receipt.Price = 10.00; //TODO
            _context.Add(receipt);
            vehicle.ArrivalTime = null;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehicles/ShowReceipts/5
        public async Task<IActionResult> ShowReceipts(string id)
        {
            return RedirectToAction("Index", "Receipts", new {id = id});
        }
        private bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.RegistrationNumber == id);
        }
    }
}
