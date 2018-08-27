﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasesApp.Controllers
{
    public class WorkerController : Controller
    {
        [Authorize(Roles = "Worker")]
        public IActionResult Index()
        {
            return View();
        }
    }
}