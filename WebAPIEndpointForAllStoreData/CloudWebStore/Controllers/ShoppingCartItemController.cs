using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CloudWebStore.Models;
using Newtonsoft.Json;

namespace CloudWebStore.Controllers
{
    public class ShoppingCartItemController : Controller
    {
        private readonly MvcProductContext _context;

        public ShoppingCartItemController(MvcProductContext context)
        {
            _context = context;
        }

        // GET: ShoppingCartItem
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShoppingCartItem.ToListAsync());
        }

        [HttpGet]
        public IEnumerable<ShoppingCartItem> Get()
        {
            return _context.ShoppingCartItem.ToList();
        }

        // GET: ShoppingCartItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCartItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<StatusCodeResult> Create([FromBody] ShoppingCartItem shoppingCartItem)
        {
            if (shoppingCartItem == null)
                return new Microsoft.AspNetCore.Mvc.BadRequestResult();
            else
            {
                if (_context.ShoppingCartItem.ToArray().Length == 0)
                    shoppingCartItem.ID = 1;
                else
                    shoppingCartItem.ID = _context.ShoppingCartItem.ToArray()[_context.ShoppingCartItem.ToArray().Length - 1].ID + 1;
                _context.ShoppingCartItem.Add(shoppingCartItem);
                await _context.SaveChangesAsync();
                return new StatusCodeResult(201);
            }
        }

        // GET: ShoppingCartItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItem.FindAsync(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductName,UserId")] ShoppingCartItem shoppingCartItem)
        {
            if (id != shoppingCartItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartItemExists(shoppingCartItem.ID))
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
            return View(shoppingCartItem);
        }
        [HttpPost, ActionName("Delete")]
        // GET: ShoppingCartItem/Delete/5
        public async Task<IActionResult> Delete(string username)
        {
            var u = username;
            if (u == null)
            {
                
                Console.WriteLine("this is : not found, username is : "+u);
                return NotFound();
            }


            foreach(var s in _context.ShoppingCartItem.ToList())
            {
                if (s.UserId.Equals(u))
                {
                    _context.ShoppingCartItem.Remove(s);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("this is :" + s.UserId);
                }

            }

            return RedirectToAction(nameof(Index));
        }

        // POST: ShoppingCartItem/Delete/5
        [HttpPost, ActionName("DeleteOne")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingCartItem = await _context.ShoppingCartItem.FindAsync(id);
            _context.ShoppingCartItem.Remove(shoppingCartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartItemExists(int id)
        {
            return _context.ShoppingCartItem.Any(e => e.ID == id);
        }
    }
}
