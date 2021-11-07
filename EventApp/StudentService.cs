using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp
{
    public class StudentService
    {
        private List<Student> students = new List<Student>();
        public void Add(Student student)
        {
            students.Add(student);
        }

        public List<Student> GetAll()
        {
            return students;
        }

        public void Update(Student updatedStudent)
        {
            var student = students.FirstOrDefault(x => x.Id == updatedStudent.Id);

            student.Name = updatedStudent.Name;
            student.LastName = updatedStudent.LastName;
        }
        public void Remove(Student removedStudent)
        {
            var student = students.FirstOrDefault(x => x.Id == removedStudent.Id);

            students.Remove(student);
        }
    }
}
