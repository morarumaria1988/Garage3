﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Models.Entities;
using Garage3.Models.Persistence;
using Garage3.Models.ViewModels;
using Bogus.DataSets;

namespace Garage3.Controllers
{
    public class VehiclesForCustomerController : Controller
    {
        private readonly GarageMVCContext _context;

        public VehiclesForCustomerController(GarageMVCContext context)
        {
            _context = context;
        }

        // GET: VehiclesForCustomer
        public async Task<IActionResult> Index(string id)
        {
            var garageMVCContext = _context.Vehicles.Where(v => v.PersonalNumber == id).Include(v => v.VType);
            var vehicles = await garageMVCContext.ToListAsync();

            List<VehiclesForCustomerViewModel.VehicleViewModel> result = new List<VehiclesForCustomerViewModel.VehicleViewModel>();
            foreach (Models.Entities.Vehicle v in vehicles) {

               var vVM = new VehiclesForCustomerViewModel.VehicleViewModel();
                vVM.PersonalNumber = v.PersonalNumber;
                vVM.NumberOfWheels = v.NumberOfWheels;
                vVM.RegistrationNumber = v.RegistrationNumber;
                vVM.Color = v.Color;
                vVM.VType = v.VType.Name;
                vVM.Make = v.Make;
                vVM.NumberOfWheels = v.NumberOfWheels;

                result.Add(vVM);
            }
          
            var customer = await _context.Customers.FirstOrDefaultAsync(v => v.PersonalNumber == id);
           
            var cVM = new VehiclesForCustomerViewModel.CustomerViewModel();
            cVM.PersonalNumber = customer.PersonalNumber;
            cVM.FullName = customer.FullName;

            var vm = new VehiclesForCustomerViewModel();
            vm.customer = cVM;
            vm.vehicleViewModels = result;

            return View(vm);
        }

        // GET: VehiclesForCustomer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Member)
                .Include(v => v.VType)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: VehiclesForCustomer/Edit/5
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
            ViewData["PersonalNumber"] = new SelectList(_context.Customers, "PersonalNumber", "PersonalNumber", vehicle.PersonalNumber);
            ViewData["TypeName"] = new SelectList(_context.VTypes, "Name", "Name", vehicle.TypeName);
            return View(vehicle);
        }

        // POST: VehiclesForCustomer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RegistrationNumber,PersonalNumber,TypeName,Color,Make,NumberOfWheels,ArrivalTime")] Models.Entities.Vehicle vehicle)
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
                return RedirectToAction(nameof(Index), new { id = vehicle.PersonalNumber });
            }
            ViewData["PersonalNumber"] = new SelectList(_context.Customers, "PersonalNumber", "PersonalNumber", vehicle.PersonalNumber);
            ViewData["TypeName"] = new SelectList(_context.VTypes, "Name", "Name", vehicle.TypeName);
            return View(vehicle);
        }

        // GET: VehiclesForCustomer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Member)
                .Include(v => v.VType)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: VehiclesForCustomer/Delete/5
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

        private bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.RegistrationNumber == id);
        }
    }
}
