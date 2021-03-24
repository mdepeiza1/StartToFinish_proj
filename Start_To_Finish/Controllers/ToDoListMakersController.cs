using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Start_To_Finish.Data;
using Start_To_Finish.Models;

namespace Start_To_Finish.Controllers
{
    [Authorize(Roles = "ToDoListMaker")]
    public class ToDoListMakersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoListMakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoListMakers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ToDoListMakers.Include(t => t.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ToDoListMakers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoListMaker = await _context.ToDoListMakers
                .Include(t => t.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoListMaker == null)
            {
                return NotFound();
            }

            return View(toDoListMaker);
        }

        // GET: ToDoListMakers/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ToDoListMakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IdentityUserId")] ToDoListMaker toDoListMaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoListMaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", toDoListMaker.IdentityUserId);
            return View(toDoListMaker);
        }

        // GET: ToDoListMakers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoListMaker = await _context.ToDoListMakers.FindAsync(id);
            if (toDoListMaker == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", toDoListMaker.IdentityUserId);
            return View(toDoListMaker);
        }

        // POST: ToDoListMakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,IdentityUserId")] ToDoListMaker toDoListMaker)
        {
            if (id != toDoListMaker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoListMaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListMakerExists(toDoListMaker.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", toDoListMaker.IdentityUserId);
            return View(toDoListMaker);
        }

        // GET: ToDoListMakers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoListMaker = await _context.ToDoListMakers
                .Include(t => t.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoListMaker == null)
            {
                return NotFound();
            }

            return View(toDoListMaker);
        }

        // POST: ToDoListMakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var toDoListMaker = await _context.ToDoListMakers.FindAsync(id);
            _context.ToDoListMakers.Remove(toDoListMaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListMakerExists(string id)
        {
            return _context.ToDoListMakers.Any(e => e.Id == id);
        }
    }
}
