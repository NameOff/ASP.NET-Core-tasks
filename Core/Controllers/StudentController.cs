using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class StudentController : Controller
    {
        [HttpPost]
        public IActionResult Create(Student student)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Students()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}