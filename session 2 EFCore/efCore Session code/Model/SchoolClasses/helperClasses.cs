using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efCore.Model.SchoolClasses
{
    #region classForFluentApi
    public class Person
    {
        public Guid Uid { get; set; }                // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Age { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TemporaryData
    {
        public int Id { get; set; }
        public string Info { get; set; }
    }

    // For composite key
    public class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
    }
    #endregion
    #region classForOneToOne
    /*public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StudentAddress Address { get; set; }
    }

    public class StudentAddress
    {
        public int StudentAddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public int AddressOfStudentId { get; set; }
        public Student Student { get; set; }
    }*/
    #endregion
    #region classForManyToMany
    /*public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        //public IList<StudentCourse> StudentCourses { get; set; }

    }

    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }

        //public IList<StudentCourse> StudentCourses { get; set; }

    }
        public class StudentCourse
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
    */
    #endregion
}
