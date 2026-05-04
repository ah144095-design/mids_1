using System;

namespace EmployeePerformanceSystem.Models
{
    public class Manager : Employee
    {
        private int _teamSize;
        private int _projectsDelivered;

        public Manager(string name, int age, string employeeId, double salary, string department, int teamSize, int projectsDelivered)
            : base(name, age, employeeId, salary, department)
        {
            TeamSize = teamSize;
            ProjectsDelivered = projectsDelivered;
        }

        public int TeamSize
        {
            get => _teamSize;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Team size must be at least 1.");
                _teamSize = value;
            }
        }

        public int ProjectsDelivered
        {
            get => _projectsDelivered;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Projects delivered cannot be negative.");
                _projectsDelivered = value;
            }
        }

        public override string Role => "Manager";

        public override string EvaluatePerformance()
        {
            double score = Math.Min((ProjectsDelivered / (double)TeamSize) * 100, 100);
            return $"Score: {score:F1} | {CalculateResult(score)}";
        }
    }
}
