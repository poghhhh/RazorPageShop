﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Data;
using SignalRAssignment.Models;

namespace SignalRAssignment.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly SignalRAssignment.Data.ApplicationDbContext _context;

        public IndexModel(SignalRAssignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string UsName = HttpContext.Session.GetString("UsName");
            ViewData["UsName"] = UsName;
            if (UsName == null)
            {
                return RedirectToPage("/Home/Index");
            }
            else
            {
                if (HttpContext.Session.GetInt32("IsAdmin") == 0)
                {
                    return RedirectToPage("/Home/Index");
                }
            }
            Account = await _context.Accounts.ToListAsync();
            return Page();
        }
    }
}
