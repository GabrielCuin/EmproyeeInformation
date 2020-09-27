using EmployeeInformation.Entities;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();
            Console.Write("Enter salary: ");
            double salaryRef = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            List<Employee> list = new List<Employee>();
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        string name = line[0];
                        string email = line[1];
                        double salary = double.Parse(line[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, salary));
                    }

                    var emails = list.Where(e => e.Salary > salaryRef).OrderBy(e => e.Email).Select(e => e.Email);

                    var sum = list.Where(e => e.Name[0] == 'M').Sum(e => e.Salary);

                    Console.WriteLine("Email of people whose salary is more than " + salaryRef.ToString("F2", CultureInfo.InvariantCulture));
                    foreach (var email in emails)
                    {
                        Console.WriteLine(email);
                    }

                    Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sum.ToString("F2", CultureInfo.InvariantCulture));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
