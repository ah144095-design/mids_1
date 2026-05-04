using System;

namespace EmployeePerformanceSystem.Models
{
    public class Staff : Employee
    {
        private int _tasksCompleted;
        private double _attendancePercent;

        public Staff(string name, int age, string employeeId, double salary, string department, int tasksCompleted, double attendancePercent)
            : base(name, age, employeeId, salary, department)
        {
            TasksCompleted = tasksCompleted;
            AttendancePercent = attendancePercent;
        }

        public int TasksCompleted
        {
            get => _tasksCompleted;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Tasks completed cannot be negative.");
                _tasksCompleted = value;
            }
        }

        public double AttendancePercent
        {
            get => _attendancePercent;
            set
            {
                _attendancePercent = Math.Max(0, Math.Min(100, value));
            }
        }

        public override string Role => "Staff";

        public override string EvaluatePerformance()
        {
            double score = Math.Min(TasksCompleted / 50.0 * 100, 100) * 0.6 + AttendancePercent * 0.4;
            return $"Score: {score:F1} | {CalculateResult(score)}";
        }
    }
}
