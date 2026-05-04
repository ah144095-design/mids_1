using System;
using System.Drawing;
using System.Windows.Forms;

namespace EmployeePerformanceSystem
{
    public class Form2 : Form
    {
        private DataGridView dgvEmployees;
        private Button btnClose;

        public Form2()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Employee Performance Report";
            this.Size = new Size(800, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvEmployees = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false
            };

            dgvEmployees.Columns.Add("Name", "Name");
            dgvEmployees.Columns.Add("Age", "Age");
            dgvEmployees.Columns.Add("EmployeeID", "Employee ID");
            dgvEmployees.Columns.Add("Department", "Department");
            dgvEmployees.Columns.Add("Role", "Role");
            dgvEmployees.Columns.Add("Performance", "Performance Result");

            this.Controls.Add(dgvEmployees);

            Panel bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 50 };
            btnClose = new Button { Text = "Close", Location = new Point(700, 10), Width = 75 };
            btnClose.Click += (s, e) => this.Close();
            bottomPanel.Controls.Add(btnClose);
            this.Controls.Add(bottomPanel);

            this.Load += Form2_Load;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dgvEmployees.Rows.Clear();
            foreach (var emp in EmployeeStore.Employees)
            {
                dgvEmployees.Rows.Add(
                    emp.Name,
                    emp.Age,
                    emp.EmployeeId,
                    emp.Department,
                    emp.Role,
                    emp.EvaluatePerformance()
                );
            }
        }
    }
}
