using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public IActionResult Home()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var toDoListMaker = _context.ToDoListMakers.Where(t => t.IdentityUserId == userId).FirstOrDefault();

            if (toDoListMaker == null)
            {
                return RedirectToAction(nameof(Create));
            }
            else
            {
                List<Note> notesToDo = new List<Note>();
                Note placeholderToDo = new Note();
                placeholderToDo.Title = "Add Content Here";
                bool placeholderToDoBool;
                Note placeholderInProgress = new Note();
                placeholderInProgress.Title = "Add Content Here";
                bool placeholderInProgressBool;
                Note placeholderComplete = new Note();
                placeholderComplete.Title = "Add Content Here";
                bool placeholderCompleteBool;
                //if (toDoListMaker.Notes != null)
                //{
                //if (toDoListMaker.Notes != null)
                if(_context.Notes.Count() != 0)
                {
                    //for (int i = 0; i < _context.Notes.Count(); i++)
                    //{
                    //    if (toDoListMaker.Notes[i].isToDo && (toDoListMaker.Notes[i].ToDoListMakerId == toDoListMaker.Id))
                    //    {
                    //        notesToDo.Add(toDoListMaker.Notes[i]);
                    //    }
                    //}
                    List<Note> notesToDo1 = _context.Notes.Where(n => n.isToDo == true 
                    && n.ToDoListMakerId == toDoListMaker.Id).ToList();
                    if (notesToDo1.Count() == 0)
                    {
                        placeholderToDo.isToDo = true;
                        List<Note> notes = new List<Note>();
                        notes.Add(placeholderToDo);
                        placeholderToDoBool = true;
                        ViewBag.NotesToDoBool = placeholderToDoBool;
                        ViewBag.NotesToDo = notes;
                    }
                    else
                    {
                        placeholderToDoBool = false;
                        ViewBag.NotesToDoBool = placeholderToDoBool;
                        ViewBag.NotesToDo = notesToDo1;
                    }
                }
                else
                {
                    placeholderToDo.isToDo = true; 
                    List<Note> notes = new List<Note>();
                    notes.Add(placeholderToDo);
                    placeholderToDoBool = true;
                    ViewBag.NotesToDoBool = placeholderToDoBool;
                    ViewBag.NotesToDo = notes;
                }

                List<Note> notesInProgress = new List<Note>();
                if (_context.Notes.Count() != 0)
                {
                    //for (int i = 0; i < toDoListMaker.Notes.Count(); i++)
                    //{
                    //    if (toDoListMaker.Notes[i].isInProgress && (toDoListMaker.Notes[i].ToDoListMakerId == toDoListMaker.Id))
                    //    {
                    //        notesInProgress.Add(toDoListMaker.Notes[i]);
                    //    }
                    //}

                    List<Note> notesInProgress1 = _context.Notes.Where(n => n.isInProgress == true
                    && n.ToDoListMakerId == toDoListMaker.Id).ToList();
                    if (notesInProgress1.Count() == 0)
                    {
                        placeholderInProgress.isInProgress = true;
                        List<Note> notes = new List<Note>();
                        notes.Add(placeholderInProgress);
                        placeholderInProgressBool = true;
                        ViewBag.NotesInProgressBool = placeholderInProgressBool;
                        ViewBag.NotesInProgress = notes;
                    }
                    else
                    {
                        placeholderInProgressBool = false;
                        ViewBag.NotesInProgressBool = placeholderInProgressBool;
                        ViewBag.NotesInProgress = notesInProgress1;
                    }
                }
                else
                {
                    placeholderInProgress.isInProgress = true;
                    List<Note> notes = new List<Note>();
                    notes.Add(placeholderInProgress);
                    placeholderInProgressBool = true;
                    ViewBag.NotesInProgressBool = placeholderInProgressBool;
                    ViewBag.NotesInProgress = notes;
                }

                List<Note> notesComplete = new List<Note>();
                if (_context.Notes.Count() != 0)
                {
                    //for (int i = 0; i < toDoListMaker.Notes.Count(); i++)
                    //{
                    //    if (toDoListMaker.Notes[i].isComplete && (toDoListMaker.Notes[i].ToDoListMakerId == toDoListMaker.Id))
                    //    {
                    //        notesComplete.Add(toDoListMaker.Notes[i]);
                    //    }
                    //}

                    List<Note> notesComplete1 = _context.Notes.Where(n => n.isComplete == true
                   && n.ToDoListMakerId == toDoListMaker.Id).ToList();
                    if (notesComplete1.Count() == 0)
                    {
                        placeholderComplete.isComplete = true;
                        List<Note> notes = new List<Note>();
                        notes.Add(placeholderComplete);
                        placeholderCompleteBool = true;
                        ViewBag.NotesCompleteBool = placeholderCompleteBool;
                        ViewBag.NotesComplete = notes;
                    }
                    else
                    {
                        placeholderCompleteBool = false;
                        ViewBag.NotesCompleteBool = placeholderCompleteBool;
                        ViewBag.NotesComplete = notesToDo;
                    }
                }
                else
                {
                    placeholderComplete.isComplete = true;
                    List<Note> notes = new List<Note>();
                    notes.Add(placeholderComplete);
                    placeholderCompleteBool = true;
                    ViewBag.NotesCompleteBool = placeholderCompleteBool;
                    ViewBag.NotesComplete = notes;
                }

                //}
                //else
                //{
                //  ViewBag.ToDoListMaker.NotesToDo = null;
                //ViewBag.ToDoListMaker.NotesInProgress = null;
                //ViewBag.ToDoListMaker.NotesComplete = null;
                //}
            }
            return View();
        }

        // GET: ToDoListMakers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ToDoListMakers.Include(t => t.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ToDoListMakers/Details/5
        public async Task<IActionResult> Details(int? id)
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
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                toDoListMaker.IdentityUserId = userId;
                _context.Add(toDoListMaker);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Home));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", toDoListMaker.IdentityUserId);
            return View(toDoListMaker);
        }

        // GET: ToDoListMakers/Create
        public IActionResult CreateToDo()
        {
            return View();
        }

        // POST: ToDoListMakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateToDo(Note newToDoNote)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var toDoListMaker = _context.ToDoListMakers.Where(t => t.IdentityUserId == userId).FirstOrDefault();
                newToDoNote.ToDoListMakerId = toDoListMaker.Id;
                newToDoNote.isToDo = true;
                if(toDoListMaker.Notes == null)
                {
                    toDoListMaker.Notes = new List<Note>();
                }
                toDoListMaker.Notes.Add(newToDoNote);
                _context.Add(newToDoNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Home));
            }
            return View(newToDoNote);
        }
        // GET: ToDoListMakers/Create
        public IActionResult CreateInProgress()
        {
            return View();
        }

        // POST: ToDoListMakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInProgress(Note newInProgressNote)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var toDoListMaker = _context.ToDoListMakers.Where(t => t.IdentityUserId == userId).FirstOrDefault();
                newInProgressNote.ToDoListMakerId = toDoListMaker.Id;
                newInProgressNote.isInProgress = true;
                if (toDoListMaker.Notes == null)
                {
                    toDoListMaker.Notes = new List<Note>();
                }
                toDoListMaker.Notes.Add(newInProgressNote);
                _context.Add(newInProgressNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Home));
            }
            return View(newInProgressNote);
        }
        // GET: ToDoListMakers/Create
        public IActionResult CreateComplete()
        {
            return View();
        }

        // POST: ToDoListMakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComplete(Note newCompleteNote)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var toDoListMaker = _context.ToDoListMakers.Where(t => t.IdentityUserId == userId).FirstOrDefault();
                newCompleteNote.ToDoListMakerId = toDoListMaker.Id;
                newCompleteNote.isComplete = true;
                if (toDoListMaker.Notes == null)
                {
                    toDoListMaker.Notes = new List<Note>();
                }
                toDoListMaker.Notes.Add(newCompleteNote);
                _context.Add(newCompleteNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Home));
            }
            return View(newCompleteNote);
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
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,IdentityUserId")] ToDoListMaker toDoListMaker)
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




        // GET: ToDoListMakers/Edit/5
        public async Task<IActionResult> EditNote(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noteToEdit = await _context.Notes.FindAsync(id);
            if (noteToEdit == null)
            {
                return NotFound();
            }
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", toDoListMaker.IdentityUserId);
            return View(noteToEdit);
        }

        // POST: ToDoListMakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNote(int? id, [Bind("Id,Title,NoteInfo,YoutubeInfo,GoogleMapsInfo,SpotifyInfo,isToDo," +
            "isInProgress,isComplete,ToDoListMakerId")] Note noteToEdit)
        {
            if (id != noteToEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noteToEdit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(noteToEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Home));
            }
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", toDoListMaker.IdentityUserId);
            return View(noteToEdit);
        }






        // GET: ToDoListMakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        private bool ToDoListMakerExists(int? id)
        {
            return _context.ToDoListMakers.Any(e => e.Id == id);
        }
        private bool NoteExists(int? id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
