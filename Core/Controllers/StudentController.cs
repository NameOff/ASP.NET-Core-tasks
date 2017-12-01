using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService service;

        public StudentController(IStudentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            service.Create(student);
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View(await service.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            if (id != null)
            {
                var student = await service.Get((int) id);
                if (student != null)
                    return View(student);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            await service.Update(student);
            return RedirectToAction("Get", new { id = student.Id });
        }
            
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var student = await service.Get((int)id);
                if (student != null)
                    return View(student);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            await service.Delete((int) id);
            return RedirectToAction("All");
        }
    }
}