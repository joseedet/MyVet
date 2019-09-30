﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyVet.Web.Data;
using MyVet.Web.Data.Entities;

namespace MyVet.Web.Controllers
{
    public class PetTypesController : Controller
    {
        private readonly DataContext _dataContext;

        public PetTypesController(DataContext context)
        {
            _dataContext = context;
        }

        // GET: PetTypes
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.PetTypes.ToListAsync());
        }

        // GET: PetTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petType = await _dataContext.PetTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petType == null)
            {
                return NotFound();
            }

            return View(petType);
        }

        // GET: PetTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PetType petType)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(petType);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petType);
        }

        // GET: PetTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petType = await _dataContext.PetTypes.FindAsync(id);
            if (petType == null)
            {
                return NotFound();
            }
            return View(petType);
        }

        // POST: PetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PetType petType)
        {
            if (id != petType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(petType);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetTypeExists(petType.Id))
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
            return View(petType);
        }

        // GET: PetTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petType = await _dataContext.PetTypes
                .Include(pt => pt.Pets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petType == null)
            {
                return NotFound();
            }
          
            if(petType.Pets.Count >0 )
            {
                //TODO:Validation Summary.
                ModelState.AddModelError(string.Empty,
                    "The pet can't be deleted because it has related records.");
                return RedirectToAction(nameof(Index));
            }
            //TODO:Falta try catch
            _dataContext.PetTypes.Remove(petType);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View(petType);
        }

       
        private bool PetTypeExists(int id)
        {
            return _dataContext.PetTypes.Any(e => e.Id == id);
        }
    }
}
