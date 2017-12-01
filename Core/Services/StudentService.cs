using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentsContext db;

        public StudentService(StudentsContext context)
        {
            db = context;
        }

        public async Task<Student> Get(int id)
        {
            var student = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
            return student;
        }

        public void Create(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public async Task Update(Student student)
        {
            db.Students.Update(student);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var student = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await db.Students.ToListAsync();
        }
    }
}
