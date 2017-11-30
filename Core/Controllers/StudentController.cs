using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentsContext db;

        public StudentController(StudentsContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View(await db.Students.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
                return NotFound();
            var student = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            db.Students.Update(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Get", new { id = student.Id });
        }
            
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var student = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
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
            var student = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("All");
        }
    }
}