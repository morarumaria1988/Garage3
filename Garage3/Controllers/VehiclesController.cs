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
    public class VehiclesController : Controller
    {
        private readonly GarageMVCContext _context;

        public VehiclesController(GarageMVCContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
         
            return View(await _context.Vehicles.ToListAsync());
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
            string[] vTypeOptions = await this._context.VTypes.Select(v => v.Name).ToArrayAsync();
            ViewData["VTypeOptions"] = vTypeOptions.Append("Other");
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
                if (!_context.VTypes.Contains(vtyp))
                {
                    _context.Add(vtyp);
                }
                _context.Add(v);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                      
            return View(v);
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
            return RedirectToAction("Index", "Receipts", id);
        }
        private bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.RegistrationNumber == id);
        }
    }
}
