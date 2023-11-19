using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employer
{
    public interface IEmployee
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Position { get; set; }
        double Salary { get; set; }
        DateTime HireDate { get; set; }
        string Gender { get; set; }
    }

    public class Employee : IEmployee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}, {Position}, Salary: {Salary}, Hire Date: {HireDate.ToShortDateString()}, Gender: {Gender}";
        }
    }
}
