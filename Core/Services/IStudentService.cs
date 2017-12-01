using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services
{
    public interface IStudentService
    {
        Task<Student> Get(int id);
        void Create(Student student);
        Task Update(Student student);
        Task Delete(int id);
        Task<IEnumerable<Student>> GetAll();
    }
}
