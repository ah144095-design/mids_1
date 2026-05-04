using System;

namespace EmployeePerformanceSystem.Models
{
    public class Person
    {
        private string _name;
        private int _age;

        public Person(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public string Name => _name;
        public int Age => _age;

        public string CalculateResult(double score)
        {
            if (score >= 90) return "Outstanding";
            if (score >= 75) return "Excellent";
            if (score >= 60) return "Satisfactory";
            if (score >= 40) return "Needs Improvement";
            return "Unsatisfactory";
        }
    }
}
