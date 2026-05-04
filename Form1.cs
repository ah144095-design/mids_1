using System;
using System.Drawing;
using System.Windows.Forms;
using EmployeePerformanceSystem.Models;

namespace EmployeePerformanceSystem
{
    public class Form1 : Form
    {
        private TextBox txtName, txtAge, txtEmployeeId, txtSalary, txtDepartment, txtRoleSpecific, txtAttendance, txtTeamSize;
        private ComboBox cmbRole;
        private Label lblRoleSpecific, lblAttendance, lblResult, lblTeamSize;
        private Button btnAdd, btnViewAll;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Smart Employee Performance Evaluation System";
            this.Size = new Size(450, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            int labelX = 20, inputX = 180, y = 20, spacing = 35;

            // Name
            AddLabel("Name:", labelX, y);
            txtName = AddTextBox(inputX, y); y += spacing;

            // Age
            AddLabel("Age:", labelX, y);
            txtAge = AddTextBox(inputX, y); y += spacing;

            // Employee ID
            AddLabel("Employee ID:", labelX, y);
            txtEmployeeId = AddTextBox(inputX, y); y += spacing;

            // Salary
            AddLabel("Salary:", labelX, y);
            txtSalary = AddTextBox(inputX, y); y += spacing;

            // Department
            AddLabel("Department:", labelX, y);
            txtDepartment = AddTextBox(inputX, y); y += spacing;

            // Role
            AddLabel("Role:", labelX, y);
            cmbRole = new ComboBox { Location = new Point(inputX, y), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new string[] { "Manager", "Staff" });
            cmbRole.SelectedIndexChanged += CmbRole_SelectedIndexChanged;
            this.Controls.Add(cmbRole); y += spacing;

            // Team Size (Manager only)
            lblTeamSize = AddLabel("Team Size:", labelX, y);
            txtTeamSize = AddTextBox(inputX, y); y += spacing;

            // Role Specific Label & Textbox
            lblRoleSpecific = AddLabel("Projects Delivered:", labelX, y);
            txtRoleSpecific = AddTextBox(inputX, y); y += spacing;

            // Attendance (Staff only)
            lblAttendance = AddLabel("Attendance %:", labelX, y);
            txtAttendance = AddTextBox(inputX, y);
            lblAttendance.Visible = false;
            txtAttendance.Visible = false;
            y += spacing;

            // Result Label
            lblResult = new Label { Location = new Point(labelX, y), Size = new Size(400, 30), Font = new Font(this.Font, FontStyle.Bold), ForeColor = Color.DarkBlue };
            this.Controls.Add(lblResult); y += 40;

            // Buttons
            btnAdd = new Button { Text = "Add Employee", Location = new Point(labelX, y), Width = 150, Height = 35 };
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);

            btnViewAll = new Button { Text = "View All Employees", Location = new Point(labelX + 170, y), Width = 150, Height = 35 };
            btnViewAll.Click += BtnViewAll_Click;
            this.Controls.Add(btnViewAll);

            cmbRole.SelectedIndex = 0; // Default to Manager
        }

        private Label AddLabel(string text, int x, int y)
        {
            Label lbl = new Label { Text = text, Location = new Point(x, y), AutoSize = true };
            this.Controls.Add(lbl);
            return lbl;
        }

        private TextBox AddTextBox(int x, int y)
        {
            TextBox txt = new TextBox { Location = new Point(x, y), Width = 200 };
            this.Controls.Add(txt);
            return txt;
        }

        private void CmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem.ToString() == "Manager")
            {
                lblRoleSpecific.Text = "Projects Delivered:";
                lblTeamSize.Visible = true;
                txtTeamSize.Visible = true;
                lblAttendance.Visible = false;
                txtAttendance.Visible = false;
            }
            else
            {
                lblRoleSpecific.Text = "Tasks Completed:";
                lblTeamSize.Visible = false;
                txtTeamSize.Visible = false;
                lblAttendance.Visible = true;
                txtAttendance.Visible = true;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtAge.Text) ||
                    string.IsNullOrWhiteSpace(txtEmployeeId.Text) || string.IsNullOrWhiteSpace(txtSalary.Text))
                {
                    MessageBox.Show("Please fill in all basic fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtAge.Text, out int age)) { MessageBox.Show("Invalid Age."); return; }
                if (!double.TryParse(txtSalary.Text, out double salary)) { MessageBox.Show("Invalid Salary."); return; }
                if (!int.TryParse(txtRoleSpecific.Text, out int roleSpecificVal)) { MessageBox.Show("Invalid value for " + lblRoleSpecific.Text); return; }

                Employee emp;
                if (cmbRole.SelectedItem.ToString() == "Manager")
                {
                    if (!int.TryParse(txtTeamSize.Text, out int teamSize)) { MessageBox.Show("Invalid Team Size."); return; }
                    emp = new Manager(txtName.Text, age, txtEmployeeId.Text, salary, txtDepartment.Text, teamSize, roleSpecificVal);
                }
                else
                {
                    if (!double.TryParse(txtAttendance.Text, out double attendance)) { MessageBox.Show("Invalid Attendance."); return; }
                    emp = new Staff(txtName.Text, age, txtEmployeeId.Text, salary, txtDepartment.Text, roleSpecificVal, attendance);
                }

                EmployeeStore.Employees.Add(emp);
                lblResult.Text = "Result: " + emp.EvaluatePerformance();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtAge.Clear();
            txtEmployeeId.Clear();
            txtSalary.Clear();
            txtDepartment.Clear();
            txtRoleSpecific.Clear();
            txtAttendance.Clear();
            txtTeamSize.Clear();
        }

        private void BtnViewAll_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }
    }
}
