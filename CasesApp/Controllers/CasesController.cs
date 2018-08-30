using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasesApp.Models;
using Microsoft.AspNetCore.Authorization;
using CasesApp.Data;
using CasesApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CasesApp.Controllers
{
    public class CasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CaseService _caseService;
        private readonly UserManager<IdentityUser> _userManager;


        public CasesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;            
            _caseService = new CaseService(_context);
            _userManager = userManager;
        }

        // GET: Cases
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.Reviewer))
                {
                    return View(_caseService.GetCasesToReview());
                }
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.Worker))
                {
                    return View(_caseService.GetCasesToWork());
                }
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.Approver))
                {
                    return View(_caseService.GetCasesToApprove());
                }
            }
            return View(await _context.Case.ToListAsync());
        }

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Case
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // GET: Cases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Details,Worker,CreateDate")] Case @case)
        {
            if (ModelState.IsValid)
            {
                @case.WorkerID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;           
                _caseService.Add(@case);
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }


        // GET: Cases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Case.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userRole = _userManager.GetRolesAsync(user);

            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.Reviewer))
                {
                    ViewBag.Role = Constants.Roles.Reviewer;
                }
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.Worker))
                {
                    ViewBag.Role = Constants.Roles.Worker;
                }
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.Approver))
                {
                    ViewBag.Role = Constants.Roles.Approver;
                }
            }

            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Details,WorkerEmail,ReviewerEmail,ApproverEmail,Status")] Case @case)
        {
            if (id != @case.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _caseService.Edit(@case);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(@case.ID))
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
            return View(@case);
        }

        // GET: Cases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Case
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @case = await _context.Case.FindAsync(id);
            _context.Case.Remove(@case);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(int id)
        {
            return _context.Case.Any(e => e.ID == id);
        }
    }
}
