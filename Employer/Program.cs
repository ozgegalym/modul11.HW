using Employer;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int numberOfEmployees;

        Console.Write("Введите количество сотрудников: ");

        // Use a while loop to handle input errors
        while (!int.TryParse(Console.ReadLine(), out numberOfEmployees))
        {
            Console.WriteLine("Ошибка ввода. Пожалуйста, введите целое число.");
            Console.Write("Введите количество сотрудников: ");
        }

        IEmployee[] employees = new Employee[numberOfEmployees];

        for (int i = 0; i < numberOfEmployees; i++)
        {
            employees[i] = ReadEmployeeData();
        }

        Console.WriteLine("\nИнформация о всех сотрудниках:");
        PrintEmployees(employees);

        Console.Write("\nВведите должность для фильтрации: ");
        string positionFilter = Console.ReadLine();
        PrintEmployeesByPosition(employees, positionFilter);

        PrintManagersAboveAverageClerkSalary(employees);

        Console.Write("\nВведите дату приема на работу для фильтрации (гггг-ММ-дд): ");
        DateTime hireDateFilter;

        // Use a while loop to handle input errors for the date
        while (!DateTime.TryParse(Console.ReadLine(), out hireDateFilter))
        {
            Console.WriteLine("Ошибка ввода. Пожалуйста, введите дату в формате гггг-ММ-дд.");
            Console.Write("Введите дату приема на работу для фильтрации: ");
        }

        PrintEmployeesHiredLater(employees, hireDateFilter);
        Console.Write("\nВведите пол для фильтрации (M/F, оставьте пустым для всех): ");
        string genderFilter = Console.ReadLine();
        PrintEmployeesByGender(employees, genderFilter);
    }

    static IEmployee ReadEmployeeData()
    {
        Console.WriteLine("\nEnter employee data:");

        Console.Write("Имя: ");
        string name = Console.ReadLine();

        Console.Write("Фамилия: ");
        string surname = Console.ReadLine();

        Console.Write("Должность: ");
        string position = Console.ReadLine();

        Console.Write("Зарплата: ");
        double salary = double.Parse(Console.ReadLine());


        Console.Write("Дата приема на работу (гггг-ММ-дд): ");
        DateTime hireDate;

     
        while (!DateTime.TryParse(Console.ReadLine(), out hireDate))
        {
            Console.WriteLine("Ошибка ввода. Пожалуйста, введите дату в формате гггг-ММ-дд.");
            Console.Write("Введите дату приема на работу для фильтрации: ");
        }

        Console.Write("Пол (М/Ж): ");
        string gender = Console.ReadLine();

        return new Employee
        {
            Name = name,
            Surname = surname,
            Position = position,
            Salary = salary,
            HireDate = hireDate,
            Gender = gender
        };
    }

    static void PrintEmployees(IEmployee[] employees)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine(employee);
        }
    }

    static void PrintEmployeesByPosition(IEmployee[] employees, string position)
    {
        var filteredEmployees = employees.Where(e => e.Position == position).ToArray();

        Console.WriteLine($"\nСотрудники с должностью '{position}':");
        PrintEmployees(filteredEmployees);
    }

    static void PrintManagersAboveAverageClerkSalary(IEmployee[] employees)
    {
        var clerkAverageSalary = employees
            .Where(e => e.Position == "Clerk")
            .Average(e => e.Salary);

        var managersAboveAverageSalary = employees
            .Where(e => e.Position == "Manager" && e.Salary > clerkAverageSalary)
            .OrderBy(e => e.Surname)
            .ToArray();

        Console.WriteLine("\nМенеджеры с зарплатой выше средней зарплаты клерка:");
        PrintEmployees(managersAboveAverageSalary);
    }

    static void PrintEmployeesHiredLater(IEmployee[] employees, DateTime hireDate)
    {
        var employeesHiredLater = employees
            .Where(e => e.HireDate > hireDate)
            .OrderBy(e => e.Surname)
            .ToArray();

        Console.WriteLine($"\nСотрудники, принятые на работу позднее {hireDate.ToShortDateString()}:");
        PrintEmployees(employeesHiredLater);
    }

    static void PrintEmployeesByGender(IEmployee[] employees, string genderFilter)
    {
        if (string.IsNullOrWhiteSpace(genderFilter))
        {
            Console.WriteLine("\nВсе работники:");
            PrintEmployees(employees);
        }
        else
        {
            var filteredEmployees = employees.Where(e => e.Gender == genderFilter).ToArray();

            Console.WriteLine($"\nСотрудники с полом '{genderFilter}':");
            PrintEmployees(filteredEmployees);
        }
    }
}
