using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using CMS.UI.Models;
using CMS.UI.Display;

namespace CMS.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Section02: Classes and Objects
            Console.WriteLine("----- Section02: Classes and Objects -----");
            Student student1 = new Student();
            Staff staff1 = new Staff();

            // Assignment for Section02
            Console.WriteLine("-- Assignment for Section02 --");
            Course computerSciece = new Course();

            // Section03: Class Fields
            Console.WriteLine("----- Section03: Class Fields -----");
            student1.firstName = "James";
            Console.WriteLine(student1.firstName);

            Student student2 = new()
            {
                firstName = "John",
                lastName = "Doe",
                studentId = 123
            };

            // Static field
            Console.WriteLine(Student.MaxBooksAllowed);

            // Value type vs Reference type
            int a = 10;
            Console.WriteLine(a);
            int b = a;
            b = 20;
            Console.WriteLine(a);
            Console.WriteLine(b);

            student1.firstName = "John";
            Console.WriteLine(student1.firstName);
            student2 = student1;
            student2.firstName = "Jane";
            Console.WriteLine(student1.firstName);
            Console.WriteLine(student2.firstName);

            // Assignment for Section03
            Console.WriteLine("-- Assignment for Section03 --");
            computerSciece.courseId = 1;
            computerSciece.courseName = "Computer Science";

            Console.WriteLine(computerSciece.courseId);
            Console.WriteLine(computerSciece.courseName);

            // Section04: Class Methods
            Console.WriteLine("----- Section04: Class Methods -----");
            Student student3 = new()
            {
                firstName = "Alice",
                lastName = "Smith",
                studentId = 456
            };
            Console.WriteLine(student3.GetId());
            // Console.WriteLine(IStudent.GetFullName());

            // Constructor and destructor
            Student student4 = new();
            Student student5 = new(789, "Bob", "Johnson");

            // Properties
            Staff staff2 = new Staff();
            Console.WriteLine(staff2.FirstName);

            // Types of passing parameters
            decimal electiveFees = 5000;
            decimal roughFees = 5000;
            decimal finalFees = 5000;

            Console.WriteLine($"Before Method Call: Elective Fees: {electiveFees}, Rough Fees: {roughFees}, Final Fees: {finalFees}");

            // staff2.CalculateFees(electiveFees, ref roughFees, out finalFees);

            Console.WriteLine($"After Method Call: Elective Fees: {electiveFees}, Rough Fees: {roughFees}, Final Fees: {finalFees}");

            // Method Overloading
            staff2.UpdateInfo("Michael");
            // staff2.UpdateInfo("Michael", "Brown");
            Console.WriteLine($"Staff Name: {staff2.FirstName} {staff2.LastName}");

            // Static display
            Display.Show("Hello!");

            // Section05: Interfaces
            Console.WriteLine("----- Section05: Interfaces -----");
            ICourse csCourse = new CSCourse();
            ICourse eleCourse = new ElectronicsCourse();

            Console.WriteLine(csCourse.GetTotalElectives());

            ICourse.DefaultElectives = 10;
            Console.WriteLine(ICourse.DefaultElectives);

            // Assignment for Section05
            Console.WriteLine("-- Assignment for Section05 --");
            IStudent student6 = new Student()
            {
                FirstName = "Laura",
                LastName = "Wilson"
            };

            Console.WriteLine(student6.GetFullName());
            Console.WriteLine(IStudent.WhoAmI());


            // Section06: Inheritance
            Console.WriteLine("----- Section06: Inheritance -----");
            Student student7 = new Student();
            student7.GetFullName();

            Student student8 = new Student(10000, "Tom", "Harris");
            Console.WriteLine(student8.GetFullName());

            // Casting in inheritance
            Person person1 = new Student(20000, "Sara", "Connor");
            Console.WriteLine(person1.GetFullName());
            // List<string> hobbies = person1.Hobbies; // Not accessible
            // List<string> hobbies = ((Student)person1).Hobbies; // Accessible with casting
            List<string> hobbies = (person1 as Student)?.Hobbies; // Safer casting
            Console.WriteLine($"Hobbies Count: {hobbies?.Count}");

            if (person1 is Student)
            {
                Student student9 = person1 as Student;
                Console.WriteLine(student9.GetFullName());
            }

            // Assignment for Section06
            Console.WriteLine("-- Assignment for Section06 --");
            Person person2 = new Staff("Mary", "Smith");
            if (person2 is Staff)
            {
                Staff staff3 = person2 as Staff;
                Console.WriteLine(staff3?.GetFullName());
            }

            // Section07: Polymorphism
            Console.WriteLine("----- Section07: Polymorphism -----");
            Course course1 = new CSCourse();
            course1.AddSubject(new CourseSubject() { id = 1, subjectName = "Data Structures" });

            // Assignment for Section07
            Console.WriteLine("-- Assignment for Section07 --");
            ElectronicsCourse electronicsCourse = new ElectronicsCourse();
            CourseSubject electronicsSubject = new CourseSubject() { id = 401, subjectName = "Basics of electrical Science" };
            electronicsCourse.AddSubject(electronicsSubject);
            electronicsCourse.RemoveSubject(electronicsSubject);

        }
    }
}
