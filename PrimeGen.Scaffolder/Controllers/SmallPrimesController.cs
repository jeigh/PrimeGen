using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeGen.DataAccess;
using PrimeGen.TransferObjects;

namespace PrimeGen.Scaffolder.Controllers
{
    public class SmallPrimesController : Controller
    {
        private readonly PrimeGenContext _context;

        public SmallPrimesController(PrimeGenContext context)
        {
            _context = context;
        }

        // GET: SmallPrimes
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: SmallPrimes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.SmallPrimes == null)
            {
                return NotFound();
            }

            var smallPrime = await _context.SmallPrimes
                .FirstOrDefaultAsync(m => m.SmallPrimeId == id);
            if (smallPrime == null)
            {
                return NotFound();
            }

            return View(smallPrime);
        }

        // GET: SmallPrimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SmallPrimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SmallPrimeId,PrimeValue")] SmallPrime smallPrime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(smallPrime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(smallPrime);
        }

        // GET: SmallPrimes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.SmallPrimes == null)
            {
                return NotFound();
            }

            var smallPrime = await _context.SmallPrimes.FindAsync(id);
            if (smallPrime == null)
            {
                return NotFound();
            }
            return View(smallPrime);
        }

        // POST: SmallPrimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SmallPrimeId,PrimeValue")] SmallPrime smallPrime)
        {
            if (id != smallPrime.SmallPrimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(smallPrime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SmallPrimeExists(smallPrime.SmallPrimeId))
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
            return View(smallPrime);
        }

        // GET: SmallPrimes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.SmallPrimes == null)
            {
                return NotFound();
            }

            var smallPrime = await _context.SmallPrimes
                .FirstOrDefaultAsync(m => m.SmallPrimeId == id);
            if (smallPrime == null)
            {
                return NotFound();
            }

            return View(smallPrime);
        }

        // POST: SmallPrimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.SmallPrimes == null)
            {
                return Problem("Entity set 'PrimeGenContext.SmallPrime'  is null.");
            }
            var smallPrime = await _context.SmallPrimes.FindAsync(id);
            if (smallPrime != null)
            {
                _context.SmallPrimes.Remove(smallPrime);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SmallPrimeExists(long id)
        {
          return (_context.SmallPrimes?.Any(e => e.SmallPrimeId == id)).GetValueOrDefault();
        }
    }
}
