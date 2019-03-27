using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CloudWebStore.Models;

namespace CloudWebStore.Controllers
{
    public class PaymentInfoController : Controller
    {
        private readonly MvcProductContext _context;

        public PaymentInfoController(MvcProductContext context)
        {
            _context = context;
        }

        // GET: PaymentInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentInfo.ToListAsync());
        }

        // GET: PaymentInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentInfo == null)
            {
                return NotFound();
            }

            return View(paymentInfo);
        }

        // GET: PaymentInfo/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IEnumerable<PaymentInfo> Get()
        {
            return _context.PaymentInfo.ToList();
        }
        // POST: PaymentInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentInfo paymentInfo)
        {

            if (_context.PaymentInfo.ToArray().Length == 0)
                paymentInfo.Id = 1;
            else
                paymentInfo.Id = _context.PaymentInfo.ToArray()[_context.PaymentInfo.ToArray().Length - 1].Id + 1;
            _context.Add(paymentInfo);
            await _context.SaveChangesAsync();
            return new StatusCodeResult(201);

        }

        // GET: PaymentInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo.FindAsync(id);
            if (paymentInfo == null)
            {
                return NotFound();
            }
            return View(paymentInfo);
        }

        // POST: PaymentInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,CardNum,PassWord")] PaymentInfo paymentInfo)
        {
            if (id != paymentInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentInfoExists(paymentInfo.Id))
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
            return View(paymentInfo);
        }

        // GET: PaymentInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentInfo == null)
            {
                return NotFound();
            }

            return View(paymentInfo);
        }

        // POST: PaymentInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentInfo = await _context.PaymentInfo.FindAsync(id);
            _context.PaymentInfo.Remove(paymentInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentInfoExists(int id)
        {
            return _context.PaymentInfo.Any(e => e.Id == id);
        }
    }
}
