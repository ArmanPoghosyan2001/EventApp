using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventApp
{
    public partial class Form1 : Form
    {
        private readonly string required = "Required";
        private StudentService _studentService;
        public Form1()
        {
            _studentService = new StudentService();
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(firstNameBox.Text) || string.IsNullOrEmpty(lastNameBox.Text))
            {
                MessageBox.Show("Please enter firstname and lastname");
            }
            else
            {
                _studentService.Add(new Student { Name = firstNameBox.Text, LastName = lastNameBox.Text });

                List<Student> students = _studentService.GetAll();
                UpdateGrid(studentGridView, students);

            }
        }
        private void UpdateGrid(DataGridView dataGrid, List<Student> students)
        {
            dataGrid.DataSource = null;
            dataGrid.Update();
            dataGrid.DataSource = students;
            dataGrid.Update();
            if (students.Count == 0)
            {
                dataGrid.ClearSelection();
            }
        }
        private void StudentGridView_Selected(object sender, EventArgs e)
        {
            for (int i = 0; i < studentGridView.Columns.Count; i++)
            {
                if (studentGridView.CurrentCell == studentGridView[i, studentGridView.Rows.Count - 1])
                {
                    studentGridView.ClearSelection();
                }
            }
            if (studentGridView.CurrentCell != null)
            {
                firstNameBox.Text = studentGridView.CurrentRow.Cells[1].Value.ToString();
                lastNameBox.Text = studentGridView.CurrentRow.Cells[2].Value.ToString();
            }
            else
            {
                firstNameBox.Text = string.Empty;
                lastNameBox.Text = string.Empty;
            }
        }

        private void FirstNameVal(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(firstNameBox.Text))
            {
                firstNameValidLbl.Text = required;
                firstNameValidLbl.ForeColor = Color.Red;
            }
            else
            {
                firstNameValidLbl.Text = string.Empty;
            }
        }
        private void LastNameVal(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastNameBox.Text))
            {
                lastNameValidLbl.Text = required;
                lastNameValidLbl.ForeColor = Color.Red;
            }
            else
            {
                lastNameValidLbl.Text = string.Empty;
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            Student student = new Student
            {
                Id = (Guid)studentGridView.CurrentRow.Cells[0].Value,
                Name = firstNameBox.Text,
                LastName = lastNameBox.Text
            };
            _studentService.Update(student);
            List<Student> students = _studentService.GetAll();
            UpdateGrid(studentGridView, students);
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            Student student = new Student
            {
                Id = (Guid)studentGridView.CurrentRow.Cells[0].Value,
                Name = firstNameBox.Text,
                LastName = lastNameBox.Text
            };
            _studentService.Remove(student);

            List<Student> students = _studentService.GetAll();
            UpdateGrid(studentGridView, students);
        }
    }
}
