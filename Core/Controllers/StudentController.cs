using System.Threading.Tasks;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService service)
        {
            studentService = service;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromServices]IStudentService service, Student student)
        {
            service.Create(student);
            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            var service = ActivatorUtilities
                .GetServiceOrCreateInstance<IStudentService>(HttpContext.RequestServices);
            if (id != null)
            {
                var student = await service.Get((int)id);
                if (student != null)
                    return View(student);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            var service = (IStudentService)HttpContext
                .RequestServices
                .GetService(typeof(IStudentService));
            await service.Update(student);
            return RedirectToAction("Get", new { id = student.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                var student = await studentService.Get((int)id);
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
            await studentService.Delete((int)id);
            return RedirectToAction("All");
        }
    }
}