using System;

namespace EmployeePerformanceSystem.Models
{
    public class Employee : Person
    {
        private string _employeeId;
        private double _salary;
        private string _department;

        public Employee(string name, int age, string employeeId, double salary, string department) 
            : base(name, age)
        {
            EmployeeId = employeeId;
            Salary = salary;
            Department = department;
        }

        public string EmployeeId
        {
            get => _employeeId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Employee ID cannot be empty.");
                _employeeId = value;
            }
        }

        public double Salary
        {
            get => _salary;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Salary must be non-negative.");
                _salary = value;
            }
        }

        public string Department
        {
            get => _department;
            set => _department = string.IsNullOrWhiteSpace(value) ? "General" : value;
        }

        public virtual string Role => "Employee";

        public virtual string EvaluatePerformance()
        {
            return "Performance not evaluated.";
        }
    }
}
