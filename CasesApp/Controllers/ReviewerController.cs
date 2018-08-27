using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasesApp.Controllers
{
    public class ReviewerController : Controller
    {
        [Authorize(Roles = "Reviewer")]
        public IActionResult Index()
        {
            return View();
        }
    }
}