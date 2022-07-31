using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using To_do_list.Data;
using To_do_list.Models;

namespace To_do_list.Controllers
{
    [Authorize]
    public class ToDoListsController : Controller
    {
        private readonly ToDoListDbContext _context;

        public ToDoListsController(ToDoListDbContext context)
        {
            _context = context;
        }

        // GET: ToDoLists
        public async Task<IActionResult> Index()
        {
              return _context.Tasks != null ? 
                          View(await _context.Tasks.ToListAsync()) :
                          Problem("Entity set 'ToDoListDbContext.Tasks'  is null.");
        }

        // GET: ToDoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var toDoList = await _context.Tasks
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // GET: ToDoLists/Create
        public IActionResult Create()
        {
            return View(new ToDoList());//pass 1st instance of ToDoList model
        }

        // POST: ToDoLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,TaskName,Description,CreatedDate,Deadline,Status")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                toDoList.CreatedDate = DateTime.Now;
                toDoList.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(toDoList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var toDoList = await _context.Tasks.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,TaskName,Description,CreatedDate,Deadline,Status")] ToDoList toDoList)
        {
            if (id != toDoList.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListExists(toDoList.TaskId))
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
            return View(toDoList);
        }

    

        // POST: ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'ToDoListDbContext.Tasks'  is null.");
            }
            var toDoList = await _context.Tasks.FindAsync(id);
            if (toDoList != null)
            {
                _context.Tasks.Remove(toDoList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListExists(int id)
        {
          return (_context.Tasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
