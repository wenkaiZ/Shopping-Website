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
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly MvcProductContext _context;

        public UsersController(MvcProductContext context)
        {
            _context = context;
        }

        // GET: Users. Used to generate UI to check the users in database
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        //Used to return all users in database
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.User.ToList();
        }


        [Route("[action]")]
        // GET: Users/Create.  Used to generate UI to create new user by admin
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //Fix the bug of POST!!!!
        //[ValidateAntiForgeryToken]
        [Route("[action]")]

        public async Task<StatusCodeResult> Create([FromBody] User user)
        {
            if (user == null)
                return new Microsoft.AspNetCore.Mvc.BadRequestResult();
            else
            {
                if (_context.User.ToArray().Length == 0)
                    user.ID = 1;
                else
                    user.ID = _context.User.ToArray()[_context.User.ToArray().Length - 1].ID + 1;
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return new StatusCodeResult(201);
            }
        }
    }
}
